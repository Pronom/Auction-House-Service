using AuctionHouseDataAnalyser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace AuctionDataServiceRetreiver
{
    public partial class AuctionDataService : ServiceBase
    {
        
        
        public AuctionDataService()
        {
            InitializeComponent();
          

        }

        protected override void OnStart(string[] args)
        {
            manageAnalyse();

            Timer timer1 = new Timer(600000);
            timer1.Elapsed += new ElapsedEventHandler(startTrigger);
            timer1.Start();


        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
        }

        private void startTrigger(object sender, EventArgs e)
        {
            manageAnalyse();
        }

        private void manageAnalyse()
        {
            startAnalyse();
            GC.Collect();
        }

        private void startAnalyse()
        {
            Analyser auctionDataAnalyser = new Analyser();
        }

    }
}
