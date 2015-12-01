using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Tools;
using Ostis.Sctp;
using Ostis.Sctp.Arguments;
namespace Demo
{
  public  class Demo
    {

      public void Run()
      {
          
          byte[] xb = new LinkContent("product").GetBytes();
          string x = LinkContent.ToString(xb);
          Console.WriteLine(x);
          Console.ReadKey();

      }

    }
}
