using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ostis.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            OstisBase ostisBase = new OstisBase();
            //ostisBase.FindUpperNodes();
            ostisBase.CheckMainIdtf();
            Console.ReadKey();
        }
    }
}
