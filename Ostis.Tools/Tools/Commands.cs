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
    /// Класс с методами выполнение команд в базе знаний
    /// </summary>
    public class Commands
    {
        private KnowledgeBase knowledgeBase;

        /// <summary>
        /// Инициализирует новый класс<see cref="Commands"/>.
        /// </summary>
        /// <param name="knowledgeBase">Абстрактная база знаний</param>
        public Commands(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }

        #region Nodes
        /// <summary>
        /// Генерирует уникальный системный идентификатор для узла на основе адреса узла и префикса
        /// </summary>
        /// <param name="nodeAddress">Адрес узла</param>
        /// <param name="preffix">Префикс для узла</param>
        /// <returns>Возвращает уникальный идентификатор для узла</returns>
        public Identifier GenerateUniqueSysIdentifier(ScAddress nodeAddress, string preffix)
        {
            Identifier sysIdtf = Identifier.Invalid;
          
            Identifier probablySysIdtf = preffix + "_"+UnixDateTime.FromDateTime(DateTime.Now).GetHashCode().ToString()+ "_"+nodeAddress.GetHashCode().ToString();
            LinkContent content = new LinkContent(probablySysIdtf.Value);
            var cmdFindLink = new FindLinksCommand(content);
            var rspFindLink = (FindLinksResponse)knowledgeBase.ExecuteCommand(cmdFindLink);

            if (rspFindLink.Addresses.Count() == 0)
            {
                sysIdtf = probablySysIdtf;
            }

            return sysIdtf;
        }


        /// <summary>
        /// Задает системный идентификатор для узла
        /// </summary>
        /// <param name="nodeAddress">Адрес узла</param>
        /// <param name="nodeSysIdtf">Системный идентификатор узла</param>
        /// <returns>Возвращает True, если системный идентификатор был установлен</returns>
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


        /// <summary>
        /// Создает новый узел заданного типа
        /// </summary>
        /// <param name="nodeType">Тип создаваемого узла</param>
        /// <returns>Адрес созданного узла</returns>
        public ScAddress CreateNode(ElementType nodeType)
        {
            ScAddress nodeAddress = ScAddress.Invalid;

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
            ScAddress nodeAddress = GetNodeAddress(sysIdentifier);

            if (knowledgeBase.IsAvaible)
            {
                if (nodeAddress == ScAddress.Invalid)
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
           
            }

            return nodeAddress;
        }

        /// <summary>
        /// Возвращает адрес произвольного узла базы знаний
        /// </summary>
        /// <param name="identifier">Идентификатор</param>
        /// <returns>Адрес узла <see cref="ScAddress"/></returns>
        public ScAddress GetNodeAddress(Identifier identifier)
        {
            ScAddress address = ScAddress.Invalid;
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
            Identifier idtf = Identifier.Invalid;
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

        #endregion

        #region Links
        /// <summary>
        /// Получает контент ссылки по известному адресу ссылки
        /// </summary>
        /// <param name="scAddress">Адрес ссылки</param>
        /// <returns></returns>
        public LinkContent GetLinkContent(ScAddress scAddress)
        {
            LinkContent content = LinkContent.Invalid;
            if (knowledgeBase.IsAvaible)
            {
                var command = new GetLinkContentCommand(scAddress);
                var response = (GetLinkContentResponse)knowledgeBase.ExecuteCommand(command);
                content = new LinkContent(response.LinkContent);
            }
            return content;
        }

        /// <summary>
        /// Создает ссылку
        /// </summary>
        /// <returns>Адрес созданной ссылки</returns>
        public ScAddress CreateLink()
        {
            ScAddress linkAddress = ScAddress.Invalid;
            if (knowledgeBase.IsAvaible)
            {
                var cmdCreateLink = new CreateLinkCommand();
                var rspCreateLink = (CreateLinkResponse)knowledgeBase.ExecuteCommand(cmdCreateLink);
                linkAddress = rspCreateLink.CreatedLinkAddress;
            }
            return linkAddress;
        }

        public List<ScAddress> GetLinksByContent(LinkContent linkContent)
        {
            List<ScAddress> listAddresses = new List<ScAddress>();
            if (knowledgeBase.IsAvaible)
            {
                var cmdGetLinks = new FindLinksCommand(linkContent);
                var rspGetLinks = (FindLinksResponse)knowledgeBase.ExecuteCommand(cmdGetLinks);
                listAddresses = rspGetLinks.Addresses;
            }


            return listAddresses;
        }


        /// <summary>
        /// Задает контент для ссылки
        /// </summary>
        /// <param name="linkAddress">Адрес ссылки</param>
        /// <param name="content">Контент для ссылки</param>
        /// <returns>True, если контент задан</returns>
        public bool SetLinkContent(ScAddress linkAddress, LinkContent content)
        {
            bool isSet = false;
            if (knowledgeBase.IsAvaible)
            {
                var cmdSetLinkContent = new SetLinkContentCommand(linkAddress, content);
                var rspSetLinkContent = (SetLinkContentResponse)knowledgeBase.ExecuteCommand(cmdSetLinkContent);
                isSet = rspSetLinkContent.ContentIsSet;
            }
            return isSet;
        }

        #endregion

        #region Arcs
        /// <summary>
        /// Создает дугу между двумя элементами базы знаний
        /// </summary>
        /// <param name="arcType">Тип дуги</param>
        /// <param name="beginElement">Адрес начального элемента дуги</param>
        /// <param name="endElement">Адрес конечного элемента дуги</param>
        /// <returns>Адрес созданной дуги</returns>
        public ScAddress CreateArc(ElementType arcType, ScAddress beginElement, ScAddress endElement)
        {
            ScAddress arcAddress = ScAddress.Invalid;
            if (knowledgeBase.IsAvaible)
            {
                var cmdCreateArc = new CreateArcCommand(arcType, beginElement, endElement);
                var rspcreateArc = (CreateArcResponse)knowledgeBase.ExecuteCommand(cmdCreateArc);
                arcAddress = rspcreateArc.CreatedArcAddress;
            }
            return arcAddress;
        }

      

        #endregion

        #region Elements
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
        /// Проверяет наличие элемента
        /// </summary>
        /// <param name="scAddress">Адрес элемента</param>
        /// <returns></returns>
        public bool IsElementExist(ScAddress scAddress)
        {
            bool isExist = false;
            if (knowledgeBase.IsAvaible)
            {
                var command = new CheckElementCommand(scAddress);
                var response = (CheckElementResponse)knowledgeBase.ExecuteCommand(command);
                isExist = response.ElementExists;
            }
            return isExist;
        }


        public bool DeleteElement(ScAddress scAddress)
        {
            bool isDeleted = false;
            if (knowledgeBase.IsAvaible)
            {
                var command = new DeleteElementCommand(scAddress);
                var response = (DeleteElementResponse)knowledgeBase.ExecuteCommand(command);
                isDeleted = response.IsDeleted;
            }
            return isDeleted;

        }


        #endregion





















    }
}
