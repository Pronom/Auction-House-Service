using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

using StatisticsTool;
using Data_Access_Layer;

namespace AuctionHouseDataAnalyser
{
    public class Analyser : Statistics
    {
        #region Fields
        private AuctionHouseJsonFile _auctionHouseJsonFile;
        private Server _server;
        private Item _itm;
        private ItemShares _itmShares;
        private Aucs _aucs;
        MarketValue _market;
        private string _serverName;
        private string _lastModified;

        private DateTime _fixedTime;

        #endregion

        #region Constructor
        public Analyser()
        {
            initProcess();
            getServer();

            if (!_server.lastModified.Contains(_lastModified))
            {
                _fixedTime = DateTime.Now;
                _auctionHouseJsonFile.serverNames.Remove(_server);

                _server.lastModified.Add(_lastModified);
                globalDataAnalyze();
                finishingDataAnalyze();
                globalMarketDataAnalyze();
                finishingMarketDataAnalyze();
                _server.marketValues.Add(_market);

                _auctionHouseJsonFile.serverNames.Add(_server);

                endingProcess(_auctionHouseJsonFile);
            }
        }

        #endregion

        #region Methods
        #region InitProcess()
        private void initProcess()
        {
            Context context = new Context();
            _auctionHouseJsonFile = context.AuctionHouseJsonFile;

            _lastModified = context.Files.lastModified;
            _aucs = context.Aucs;

            _serverName = context.Server;

        }

        private void getServer()
        {
            if (_auctionHouseJsonFile.serverNames.Where(p => p.serverName == _serverName).ToList().Count == 0)
            {
                _server = new Server();
                _server.serverName = _serverName;
            }
            else
            {
                _server = _auctionHouseJsonFile.serverNames.Where(p => p.serverName == _serverName).FirstOrDefault();
            }
        }

        #endregion

        #region Items Analyze

        private void globalDataAnalyze()
        {
            _aucs.Auctions.Select(p => p).ToList().ForEach(delegate (Auction item)
              {
                  if (_server.items.Where(p => p.item == item.item).ToList().Count == 0)
                  {
                      _itm = new Item();
                      _itm.item = item.item;
                  }
                  else
                  {
                      _itm = _server.items.Where(p => p.item == item.item).FirstOrDefault();
                      _server.items.Remove(_itm);
                  }



                  if (_itm.itemShares.Where(p => p.date == _fixedTime).ToList().Count == 0)
                  {
                      _itmShares = new ItemShares();
                      _itmShares.date = _fixedTime;
                  }
                  else
                  {
                      _itmShares = _itm.itemShares.Where(p => p.date == _fixedTime).FirstOrDefault();
                      _itm.itemShares.Remove(_itmShares);
                  }

                  _itmShares.globalQuantity += item.quantity;
                  _itmShares.globalSum += item.buyout;
                  _itmShares.globalAuctionOccurences++;

                  _itm.itemShares.Add(_itmShares);
                  _server.items.Add(_itm);
              });

        }

        private void finishingDataAnalyze()
        {


            _server.items.ForEach(delegate (Item item)
            {
                _itmShares = item.itemShares.Where(p => p.date == _fixedTime).FirstOrDefault();
                if (_itmShares != null)
                {
                    item.itemShares.Remove(_itmShares);


                    _itmShares.globalAverage = getAverage(_itmShares.globalSum, _itmShares.globalQuantity);

                    foreach (Auction auct in _aucs.Auctions)
                    {
                        if (auct.item == item.item)
                        {
                            if (auct.buyout <= _itmShares.globalAverage)
                            {
                                _itmShares.belowOrEqualAverageQuantity += auct.quantity;
                                _itmShares.belowOrEqualAverageSum += auct.buyout;
                                _itmShares.belowOrEqualAverageAuctionOccurences++;
                            }
                            else if (auct.buyout > _itmShares.globalAverage)
                            {
                                _itmShares.aboveAverageQuantity += auct.quantity;
                                _itmShares.aboveAverageSum += auct.buyout;
                                _itmShares.aboveAuctionOccurences++;
                            }
                        }
                    }

                    _itmShares.belowOrEqualAverageAverage = getAverage(_itmShares.belowOrEqualAverageSum, _itmShares.belowOrEqualAverageQuantity);
                    _itmShares.aboveAverageAverage = getAverage(_itmShares.aboveAverageSum, _itmShares.aboveAverageQuantity);

                    item.itemShares.Add(_itmShares);
                }

            });


        }

        #endregion

        #region Market Analyze
        private void globalMarketDataAnalyze()
        {
            _market = new MarketValue();
            _market.date = _fixedTime;


            _server.items.ForEach(delegate (Item item)
            {
                foreach (ItemShares itemShares in item.itemShares)
                {
                    if (itemShares.date == _fixedTime)
                    {
                        _market.globalQuantity += itemShares.globalQuantity;
                        _market.globalSum += itemShares.globalSum;

                        _market.globalAuctionOccurences += itemShares.globalAuctionOccurences;
                    }
                }
            });


            _market.globalAverage = getAverage(_market.globalSum, _market.globalQuantity);

        }

        private void finishingMarketDataAnalyze()
        {
            _server.items.ForEach(delegate (Item item)
            {
                foreach (ItemShares itemShares in item.itemShares)
                {
                    if (itemShares.date == _fixedTime)
                    {
                        if (itemShares.globalSum > _market.globalAverage)
                        {
                            _market.aboveAverageQuantity += itemShares.aboveAverageQuantity;
                            _market.aboveAverageSum += itemShares.aboveAverageSum;
                            _market.aboveAuctionOccurences += itemShares.globalAuctionOccurences;
                        }
                        else if (itemShares.globalSum <= _market.globalAverage)
                        {
                            _market.belowOrEqualAverageQuantity += itemShares.belowOrEqualAverageQuantity;
                            _market.aboveAverageSum += itemShares.belowOrEqualAverageSum;
                            _market.belowOrEqualAverageAuctionOccurences += itemShares.globalAuctionOccurences;
                        }
                    }
                }
            });

            _market.aboveAverageAverage = getAverage(_market.aboveAverageSum, _market.aboveAverageQuantity);
            _market.belowOrEqualAverageAverage = getAverage(_market.belowOrEqualAverageSum, _market.belowOrEqualAverageQuantity);
        }

        #endregion
        #region EndingProcess
        private void endingProcess(AuctionHouseJsonFile auctionHouseJsonFile)
        {
            Context context = new Context();

            context.saveAnalyze(auctionHouseJsonFile);

        }

        #endregion
        #endregion

    }

}
