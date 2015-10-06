using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;
using Ostis.Sctp.Commands;
using Ostis.Sctp.Responses;
using System.IO;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс с методами для диагностики абстрактной базы знаний
    /// </summary>
    public class Commands
    {
        private KnowledgeBase knowledgeBase;
        /// <summary>
        /// Инициализирует новый класс<see cref="Diagnostic"/>.
        /// </summary>
        /// <param name="knowledgeBase">Абстрактная база знаний</param>
        public Commands(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }

        #region Commands


        public Identifier FindUniqueSysIdentifier(ScAddress nodeAddress, string preffix)
        {
            Identifier sysIdtf = Identifier.Unknown;
            StringBuilder stringBilder = new StringBuilder();

            Identifier probablySysIdtf = stringBilder.AppendFormat(preffix, "_", nodeAddress.GetHashCode()).ToString();
            LinkContent content = new LinkContent(probablySysIdtf.Value);
            var cmdFindLink = new FindLinksCommand(content);
            var rspFindLink = (FindLinksResponse)knowledgeBase.ExecuteCommand(cmdFindLink);

            if (rspFindLink.Addresses.Count() == 0)
            {
                sysIdtf = probablySysIdtf;
            }


            return sysIdtf;
        }

        public bool SetSysIdentifier(ScAddress nodeAddress, Identifier nodeSysIdtf)
        {
            bool isSuccesful = false;
            if (knowledgeBase.IsAvaible)
            {
                var cmdSetIdtf = new SetSystemIdCommand(nodeAddress, nodeSysIdtf);
                var rspSetIdtf = (SetSystemIdResponse)knowledgeBase.ExecuteCommand(cmdSetIdtf);
                isSuccesful = rspSetIdtf.IsSuccesfull;
            }
            return isSuccesful;

        }

        public ScAddress CreateNode(ElementType nodeType)
        {
            ScAddress nodeAddress = ScAddress.Unknown;

            if (knowledgeBase.IsAvaible)
            {
                var cmdCreateNode = new CreateNodeCommand(nodeType);
                var rspCreateNode = (CreateNodeResponse)knowledgeBase.ExecuteCommand(cmdCreateNode);

                nodeAddress = rspCreateNode.CreatedNodeAddress;

            }

            return nodeAddress;
        }

        /// <summary>
        /// Создает новый узел с заданным системным идентификатором
        /// </summary>
        /// <param name="nodeType">Тип узла</param>
        /// <param name="sysIdentifier">Системный идентификатор</param>
        /// <returns></returns>
        public ScAddress CreateNode(ElementType nodeType, Identifier sysIdentifier)
        {
            ScAddress nodeAddress = ScAddress.Unknown;

            if (knowledgeBase.IsAvaible)
            {
                var cmdCreateNode = new CreateNodeCommand(nodeType);
                var rspCreateNode = (CreateNodeResponse)knowledgeBase.ExecuteCommand(cmdCreateNode);

                if (rspCreateNode.Header.ReturnCode == ReturnCode.Successfull)
                {

                    if (this.SetSysIdentifier(rspCreateNode.CreatedNodeAddress, sysIdentifier) == true)
                    {
                        nodeAddress = rspCreateNode.CreatedNodeAddress;
                    }
                }

            }

            return nodeAddress;
        }

        /// <summary>
        /// Создает новый узел с заданным системным идентификатором
        /// </summary>
        /// <param name="nodeType">Тип узла</param>
        /// <param name="identifierString">Системный идентификатор в виде строки</param>
        /// <returns></returns>
        public ScAddress CreateNode(ElementType nodeType, string identifierString)
        {
            return this.CreateNode(nodeType, new Identifier(identifierString));
        }


        /// <summary>
        /// Возвращает адрес произвольного узла базы знаний
        /// </summary>
        /// <param name="identifierString">Строка идентификатора</param>
        /// <returns>Адрес узла <see cref="ScAddress"/></returns>
        public ScAddress GetNodeAddress(string identifierString)
        {
            return GetNodeAddress(new Identifier(identifierString));
        }

        /// <summary>
        /// Возвращает адрес произвольного узла базы знаний
        /// </summary>
        /// <param name="identifier">Идентификатор</param>
        /// <returns>Адрес узла <see cref="ScAddress"/></returns>
        public ScAddress GetNodeAddress(Identifier identifier)
        {
            ScAddress address = ScAddress.Unknown;
            if (knowledgeBase.IsAvaible)
            {
                var command = new FindElementCommand(identifier);
                var response = (FindElementResponse)knowledgeBase.ExecuteCommand(command);
                address = response.FoundAddress;
            }

            return address;
        }

        /// <summary>
        /// Получает произвольный идентификатор узла
        /// </summary>
        /// <param name="scAddress">Адрес узла</param>
        /// <param name="identifierType">Идентификатор для поиска</param>
        /// <returns></returns>
        public Identifier GetNodeIdentifier(ScAddress scAddress, Identifier identifierType)
        {
            Identifier idtf = Identifier.Unknown;
            if (knowledgeBase.IsAvaible)
            {
                ConstructionTemplate template = new ConstructionTemplate(scAddress, ElementType.ConstantCommonArc_c, ElementType.Link_a, ElementType.PositiveConstantPermanentAccessArc_c, GetNodeAddress(identifierType));
                var command = new IterateElementsCommand(template);
                var response = (IterateElementsResponse)knowledgeBase.ExecuteCommand(command);
                if (response.Constructions.Count == 1)
                {
                    ScAddress link = response.Constructions[0][2];
                    var commandGetLink = new GetLinkContentCommand(link);
                    var responseGetLink = (GetLinkContentResponse)knowledgeBase.ExecuteCommand(commandGetLink);
                    if (responseGetLink.Header.ReturnCode == ReturnCode.Successfull)
                    {
                        idtf = LinkContent.ToString(responseGetLink.LinkContent);
                    }
                }
            }

            return idtf;
        }

        /// <summary>
        /// Получает системный идентификатор узла по известному адресу
        /// </summary>
        /// <param name="scAddress">Адрес узда</param>
        /// <returns></returns>
        public Identifier GetNodeSysIdentifier(ScAddress scAddress)
        {
            return GetNodeIdentifier(scAddress, new Identifier("nrel_system_identifier"));
        }

        /// <summary>
        /// Получает тип элемента по известному адресу
        /// </summary>
        /// <param name="scAddress">Адрес элемента</param>
        /// <returns></returns>
        public ElementType GetElementType(ScAddress scAddress)
        {
            ElementType type = ElementType.Unknown;
            if (knowledgeBase.IsAvaible)
            {
                var command = new GetElementTypeCommand(scAddress);
                var response = (GetElementTypeResponse)knowledgeBase.ExecuteCommand(command);
                type = response.ElementType;
            }
            return type;
        }

        /// <summary>
        /// Получает контент ссылки по известному адресу ссылки
        /// </summary>
        /// <param name="scAddress">Адрес ссылки</param>
        /// <returns></returns>
        public LinkContent GetLinkContent(ScAddress scAddress)
        {
            LinkContent content = LinkContent.Unknown;
            if (knowledgeBase.IsAvaible)
            {
                var command = new GetLinkContentCommand(scAddress);
                var response = (GetLinkContentResponse)knowledgeBase.ExecuteCommand(command);
                content = new LinkContent(response.LinkContent);
            }
            return content;
        }


        #endregion


    }
}
