using Nancy;
using Nancy.Hosting.Self;
using System;
using System.IO;

namespace TNTScaleService
{
    public class WebService : NancyModule
    {
        public WebService()
            {
            Get["/"] = _ =>
            {
                Zebra zebra = new Zebra();
                return "printed";
            };
        }
    }
}
