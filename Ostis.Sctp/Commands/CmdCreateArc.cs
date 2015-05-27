using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdCreateArc : ACommand
    {

        public CmdCreateArc(ElementType arctype,ScAddress beginaddress, ScAddress endaddress)
            : base(0x06,0)
        {
           
            UInt32 argsize = 0;

            Argument<ElementType> _argtypearc = new Argument<ElementType>(arctype);
            base._arguments.Add(_argtypearc);
           
            Argument<ScAddress> _argstaddress = new Argument<ScAddress>(beginaddress);
            base._arguments.Add(_argstaddress);

            Argument<ScAddress> _argendaddress = new Argument<ScAddress>(endaddress);
            base._arguments.Add(_argendaddress);

            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgumentsSize = argsize;
        }


    }
}
