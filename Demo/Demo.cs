using System;

using Ostis.Sctp.Arguments;

namespace Demo
{
    public class Demo
    {
        public void Run()
        {
            Console.WriteLine(LinkContent.ToString(new LinkContent("product").GetBytes()));
        }
    }
}
