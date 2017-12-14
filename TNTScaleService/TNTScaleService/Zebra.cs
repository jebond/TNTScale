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
        public bool ConnectToPrinter()
        {
            PrinterSettings p = new PrinterSettings();
            p.PrinterName = "ZTC ZP 450-200dpi";
            USBPrinter printer = new USBPrinter(p);
            byte[] woop = { 0x20, 0x20, 0x20 };
            printer.Print(woop);
            return true;
        }
    }
}
