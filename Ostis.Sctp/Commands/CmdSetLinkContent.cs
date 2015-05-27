using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdSetLinkContent : ACommand
    {

        public CmdSetLinkContent(ScAddress linkaddress, LinkContent content)
            : base(0x0b,0)
        {
           
            UInt32 argsize = 0;
            Argument<ScAddress> _arglinkadr = new Argument<ScAddress>(linkaddress);
            base.Arguments.Add(_arglinkadr);

            UInt32 contlenght = (uint)content.BytesStream.Length;
            Argument<UInt32> _argcontlenght = new Argument<UInt32>(contlenght);
            base.Arguments.Add(_argcontlenght);

            Argument<LinkContent> _argcontent = new Argument<LinkContent>(content);
            base.Arguments.Add(_argcontent);

            foreach (IArgument arg in base.Arguments)
            {
                argsize += arg.Length;
            }
            base.Header.ArgumentsSize = argsize;
        }

    }
}
