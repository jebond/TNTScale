
using System;
using System.Threading;
using HidLibrary;
using ScaleInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScaleReader
{
  class Program
  {
    public static void Main(string[] args)
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
        Console.WriteLine("Weight: {0:0.00} LBS", weight);
        Console.ReadLine();
            } else {
        Console.WriteLine("No Scale Connected.");
        Console.ReadLine();
            }
    }
  }
}
