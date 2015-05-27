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
            base.Arguments.Add(_argtypearc);
           
            Argument<ScAddress> _argstaddress = new Argument<ScAddress>(beginaddress);
            base.Arguments.Add(_argstaddress);

            Argument<ScAddress> _argendaddress = new Argument<ScAddress>(endaddress);
            base.Arguments.Add(_argendaddress);

            foreach (IArgument arg in base.Arguments)
            {
                argsize += arg.Length;
            }
            base.Header.ArgumentsSize = argsize;
        }


    }
}
