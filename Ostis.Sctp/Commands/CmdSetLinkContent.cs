using System;
using sctp_client.Arguments;

namespace sctp_client.Commands
{

    internal class CmdSetLinkContent : ACommand
    {

        public CmdSetLinkContent(ScAddress linkaddress, LinkContent content)
            : base(0x0b,0)
        {
           
            UInt32 argsize = 0;
            Argument<ScAddress> _arglinkadr = new Argument<ScAddress>(linkaddress);
            base._arguments.Add(_arglinkadr);

            UInt32 contlenght = (uint)content.BytesStream.Length;
            Argument<UInt32> _argcontlenght = new Argument<UInt32>(contlenght);
            base._arguments.Add(_argcontlenght);

            Argument<LinkContent> _argcontent = new Argument<LinkContent>(content);
            base._arguments.Add(_argcontent);

            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgSize = argsize;
        }

    }
}
