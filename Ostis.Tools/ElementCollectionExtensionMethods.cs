using System.Collections.Generic;
using System.Linq;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Прочие методыдля работы с коллекцией элементов.
    /// </summary>
    public static class ElementCollectionExtensionMethods
    {
        /// <summary>
        /// Поиск узла по идентификатору.
        /// </summary>
        /// <param name="nodeCollection">коллекция</param>
        /// <param name="systemIdentifier">идентификатор</param>
        /// <returns>список найденных</returns>
        public static Node GetNodeBySystemIdentifier(this ElementCollection<Node> nodeCollection, Identifier systemIdentifier)
        {
            Node foundNode = nodeCollection.FirstOrDefault(n => n.SystemIdentifier == systemIdentifier);
            if (foundNode == null)
            {
                foundNode = nodeCollection[nodeCollection.KnowledgeBase.Commands.GetNodeAddress(systemIdentifier)];
            }
            return foundNode;
        }

        /// <summary>
        /// Поиск ссылок по содержанию.
        /// </summary>
        /// <param name="linkCollection">коллекция</param>
        /// <param name="linkContent"содержимое></param>
        /// <returns>список найденных</returns>
        public static ElementCollection<Link> GetLinksByContent(this ElementCollection<Link> linkCollection, LinkContent linkContent)
        {
            List<ScAddress> linkAddresses = linkCollection.KnowledgeBase.Commands.GetLinksByContent(linkContent);
            var foundedCollection = new ElementCollection<Link>(linkCollection.KnowledgeBase);
            foreach (var address in linkAddresses)
            {
                foundedCollection.Add(linkCollection[address]);
            }
            return foundedCollection;
        }
    }
}
