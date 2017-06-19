using BOL;
using JSONSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class Context
    {
        private AuctionHouseJsonFile _auctionHouseJsonFile;
        private AuctionFiles _auctionFiles;
        private Files files;
        private Aucs _aucs;

        private string _server = "archimonde";
        private string _key = ""; //HiddenKey 
        private string _locale = "fr_FR";

        private string auctionHouseJsonFileURI = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\auctionsJeu.json";

        public Context()
        {
            string _webAPIUri = @"https://eu.api.battle.net/wow/auction/data/" + Server + "?locale=" + _locale + "&apikey=" + _key;

            JsonSaveAndLoad jsonSaveLoad = new JsonSaveAndLoad();

            _auctionFiles = (AuctionFiles)jsonSaveLoad.deserializeData(_auctionFiles, typeof(AuctionFiles), _webAPIUri, SourceType.FromWeb);
            files = _auctionFiles.Files.FirstOrDefault();
            _aucs = (Aucs)jsonSaveLoad.deserializeData(_aucs, typeof(Aucs), _auctionFiles.Files.Select(p => p.url).FirstOrDefault(), SourceType.FromWeb);

            _auctionHouseJsonFile = (AuctionHouseJsonFile)jsonSaveLoad.deserializeData(_auctionHouseJsonFile, typeof(AuctionHouseJsonFile), auctionHouseJsonFileURI, SourceType.FromFile);

        }

        public AuctionHouseJsonFile AuctionHouseJsonFile
        {
            get
            {
                return _auctionHouseJsonFile;
            }
            set
            {
                _auctionHouseJsonFile = value;
            }
        }

        public Aucs Aucs
        {
            get
            {
                return _aucs;
            }

        }

        public Files Files
        {
            get
            {
                return files;
            }

            set
            {
                files = value;
            }
        }

        public string Server
        {
            get
            {
                return _server;
            }

            set
            {
                _server = value;
            }
        }

        public void saveAnalyze(AuctionHouseJsonFile auctionHouseJsonFile)
        {
            JsonSaveAndLoad json = new JsonSaveAndLoad();

            json.serializeData(typeof(AuctionHouseJsonFile), auctionHouseJsonFile, auctionHouseJsonFileURI, SelectFormatting.None);

        }
    }
}
