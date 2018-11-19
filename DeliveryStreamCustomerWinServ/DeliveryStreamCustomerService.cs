using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Configuration;

namespace DeliveryStreamCustomerWinServ
{
    /// <summary>
    /// Partial DeliveryStreamCustomerService class
    /// </summary>
    partial class DeliveryStreamCustomerService : ServiceBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DeliveryStreamCustomerService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OnStart
        /// </summary>
        /// <param name="args">Scalar argument</param>
        protected override void OnStart(string[] args)
        {
            try
            {
               //Debugger.Launch();
                new Thread(new ThreadStart(ThreadProc)).Start();
                new Thread(new ThreadStart(ImageThreadProc)).Start();
                
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// OnStop
        /// </summary>
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        /// <summary>
        /// ThreadProc
        /// </summary>
        public static void ThreadProc()
        { 
            TimeSpan timeOutInt = TimeSpan.FromMinutes((double)Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]));
            while (true)
            {
                WebServices.CallWebService();
                Thread.Sleep(timeOutInt);
            }
        }

        /// <summary>
        /// ImageThreadProc
        /// </summary>
        public static void ImageThreadProc()
        {

            TimeSpan timeOutInt = TimeSpan.FromMinutes((double)Convert.ToInt32(ConfigurationManager.AppSettings["ImageRetrivalInterval"]));
            while (true)
            {
                WebServices.CallImageWebService();
                Thread.Sleep(timeOutInt);
            }
        }
    }
}
