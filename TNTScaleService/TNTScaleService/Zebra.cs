using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.SharpZebra;
using Com.SharpZebra.Printing;

namespace TNTScaleService
{
    class Zebra
    {
        public Zebra()
        {
            //PrinterSettings ps = new PrinterSettings();
            //ps.PrinterName = "ZebraZP450-200dpi";
            //ps.Width = 203 * 4;
            //ps.Length = 203 * 6;
            //ps.Darkness = 30;

            //byte[] woop = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            //new SpoolPrinter(ps).Print(woop);
            var enumDevices = UsbPrinterConnector.EnumDevices();

            if (enumDevices.Keys.Count > 0)

            {

                string key = enumDevices.Keys.First();

                UsbPrinterConnector connector = new UsbPrinterConnector(key);

                string command1 = @"^XA^PW464~SD15" +

                @"^FO05,100^A0N,32,32^FB450,1,0,C^FDTest Line^FS ";

                command1 = command1 +

                @"^CN1

                ^PN0        

                ^XZ";

                byte[] buffer1 = ASCIIEncoding.ASCII.GetBytes(command1);

                connector.Send(buffer1, 1, 1);
            }
        }
    }
}
