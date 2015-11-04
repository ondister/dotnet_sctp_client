using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
   public static class ElementCollectionExtensionMethods
    {

       public static Node GetNodeBySystemIdentifier(this ElementCollection<Node> nodeCollection, Identifier systemIdentifier)
       {
          
           Node foundNode = nodeCollection.FirstOrDefault(n => n.SystemIdentifier == systemIdentifier);
           if (foundNode == null)
           {
               foundNode = nodeCollection[nodeCollection.KnowledgeBase.Commands.GetNodeAddress(systemIdentifier)];
           }

           return foundNode;
       }

       public static ElementCollection<Link> GetLinksByContent(this ElementCollection<Link> linkCollection, LinkContent linkContent)
       {
           List<ScAddress> linkAddresses = linkCollection.KnowledgeBase.Commands.GetLinksByContent(linkContent);
           ElementCollection<Link> foundedCollection = new ElementCollection<Link>(linkCollection.KnowledgeBase);
           foreach (var address in linkAddresses)
           {
               foundedCollection.Add(linkCollection[address]);
           }

           return foundedCollection;
       }

       
    }
}
