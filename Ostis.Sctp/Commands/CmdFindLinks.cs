using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdFindLinks : ACommand
    {

        public CmdFindLinks(LinkContent content)
            : base(0x0a,0)
        {
           
            UInt32 argsize = 0;
          
            UInt32 contlenght = (uint)content.BytesStream.Length;
            Argument<UInt32> _argcontlenght = new Argument<UInt32>(contlenght);
            base._arguments.Add(_argcontlenght);

            Argument<LinkContent> _argcontent = new Argument<LinkContent>(content);
            base._arguments.Add(_argcontent);

            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgumentsSize = argsize;
        }

    }
}
