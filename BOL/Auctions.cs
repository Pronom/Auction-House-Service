using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Auction
    {
        public long auc { get; set; }
        public long item { get; set; }
        public string owner { get; set; }
        public string ownerRealm { get; set; }
        public long bid { get; set; }
        public long buyout { get; set; }
        public int quantity { get; set; }
        public string timeLeft { get; set; }
        public int rand { get; set; }
        public long seed { get; set; }
        public int context { get; set; }      
    }

    public class Aucs
    {
        public List<Auction> Auctions { get; set; }

        public Aucs()
        {
            Auctions = new List<Auction>();
        }
    }

    public class AuctionFiles
    {
        public List<Files> Files { get; set; }

        public AuctionFiles()
        {
            Files = new List<Files>();
        }
    }

    public class Files
    {
        public string url { get; set; }
        public string lastModified { get; set; }
    }

    public class AuctionHouseJsonFile
    {
        public List<Server> serverNames { get; set; }

        public AuctionHouseJsonFile()
        {
            serverNames = new List<Server>();
        }

    }
    public class Server
    {
        public string serverName { get; set; }
        public List<Item> items { get; set; }
        public List<string> lastModified { get; set; }
        public List<MarketValue> marketValues { get; set; }

        public Server()
        {
            items = new List<Item>();
            lastModified = new List<string>();
            marketValues = new List<MarketValue>();
        }

    }

    public class Item
    {
        public long item { get; set; }
        public List<ItemShares> itemShares { get; set; }

        public Item()
        {
            itemShares = new List<ItemShares>();
        }
    }

    public class ItemShares
    {
        public DateTime date { get; set; }

        public long globalQuantity { get; set; }
        public long globalSum { get; set; }
        public decimal globalAverage { get; set; }

        public int globalAuctionOccurences { get; set; }

        public long belowOrEqualAverageQuantity { get; set; }
        public long belowOrEqualAverageSum { get; set; }
        public decimal belowOrEqualAverageAverage { get; set; }

        public int belowOrEqualAverageAuctionOccurences { get; set; }

        public long aboveAverageQuantity { get; set; }
        public long aboveAverageSum { get; set; }
        public decimal aboveAverageAverage { get; set; }

        public int aboveAuctionOccurences { get; set; }
    }

    public class MarketValue
    {
        public DateTime date { get; set; }

        public long globalQuantity { get; set; }
        public long globalSum { get; set; }
        public decimal globalAverage { get; set; }
        public int globalAuctionOccurences { get; set; }

        public long belowOrEqualAverageQuantity { get; set; }
        public long belowOrEqualAverageSum { get; set; }
        public decimal belowOrEqualAverageAverage { get; set; }
        public int belowOrEqualAverageAuctionOccurences { get; set; }

        public long aboveAverageQuantity { get; set; }
        public long aboveAverageSum { get; set; }
        public decimal aboveAverageAverage { get; set; }
        public int aboveAuctionOccurences { get; set; }
    }


}
