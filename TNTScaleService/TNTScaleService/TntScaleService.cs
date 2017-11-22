﻿using System.ServiceProcess;
using System.IO;
using ScaleInterface;
using System.Configuration;
using System;
using System.Net;
using Newtonsoft.Json;

namespace TNTScaleService
{
    public partial class TnTScaleService : ServiceBase
    {
        int ScaleCheckTime = Convert.ToInt32(ConfigurationManager.AppSettings["ScaleCheckFrequencyMilliseconds"]);
        string Endpoint = ConfigurationManager.AppSettings["WebServiceUrl"];
        string ScaleLogFile = ConfigurationManager.AppSettings["LogFileOutput"];
        string HttpLogFile = ConfigurationManager.AppSettings["HttpResponseLog"];
        string debugmode = ConfigurationManager.AppSettings["DebugMode"];
        decimal? lastweight = 0;

        public TnTScaleService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = ScaleCheckTime;  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }
        
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {

            decimal? weight;
            bool? isStable;

            USBScale s = new USBScale();
            s.Connect();

            if (s.IsConnected)
            {
                s.GetWeight(out weight, out isStable);
                s.DebugScaleData();
                s.Disconnect();

                if (debugmode == "true")
                {
                    File.AppendAllText(ScaleLogFile, weight.ToString() + " LBS" + System.Environment.NewLine);
                }
                else
                {

                }

                if (weight > 0.0m)
                {
                    SendMessage(weight);
                }
                else
                {
                    //Do nothing if there is no change.
                }
                lastweight = weight;
            }
            else
            {
                if (debugmode == "true")
                {
                    File.AppendAllText(ScaleLogFile, "No scale connected" + Environment.NewLine);
                }
                else
                {

                }
            }
        }

        public string SendMessage(decimal? weightpassed)
        {

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
{
    return true; // **** Always accept
};

            try
            {
                if (debugmode == "true")
                    {
                     File.AppendAllText(HttpLogFile, "woot" + Environment.NewLine);
                    }
               else
                    {

                    }

                string webAddr = Endpoint;

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    messageobject mobject = new messageobject();
                    mobject.computername = Environment.MachineName.ToLower() + ".trollandtoad.local";
                    mobject.weight = weightpassed.ToString();

                    var json = JsonConvert.SerializeObject(mobject);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }
            }
            catch (WebException ex)
            {
                return "Failure Exception";
            }
            return "Successful";
        }
    }
}