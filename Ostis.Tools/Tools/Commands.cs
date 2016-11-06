using System;
using System.Collections.Generic;
using Ostis.Sctp.Arguments;
using Ostis.Sctp.Commands;
using Ostis.Sctp.Responses;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс с методами выполнения команд в базе знаний.
    /// </summary>
    public class Commands
    {
        private readonly KnowledgeBase knowledgeBase;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="knowledgeBase">база знаний</param>
        public Commands(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }

        #region Nodes

        /// <summary>
        /// Генерирация уникального системного идентификатора для узла на основе адреса узла и префикса.
        /// </summary>
        /// <param name="nodeAddress">SC-адрес узла</param>
        /// <param name="preffix">префикс для узла</param>
        /// <returns>уникальный идентификатор для узла</returns>
        public Identifier GenerateUniqueSysIdentifier(ScAddress nodeAddress, string preffix)
        {
            Identifier identifier = Identifier.Invalid;
            Identifier probablyIdentifier = string.Format("{0}_{1}_{2}", preffix, UnixDateTime.FromDateTime(DateTime.Now).GetHashCode(), nodeAddress.GetHashCode());
            
            var content = new LinkContent(probablyIdentifier.Value);
            var command = new FindLinksCommand(content);
            var response = (FindLinksResponse) knowledgeBase.ExecuteCommand(command);

            if (response.Addresses.Count == 0)
            {
                identifier = probablyIdentifier;
            }
            return identifier;
        }
        
        /// <summary>
        /// Задание системного идентификатора для узла.
        /// </summary>
        /// <param name="nodeAddress">SC-адрес узла</param>
        /// <param name="nodeIdentifier">системный идентификатор узла</param>
        /// <returns><b>true</b>, если системный идентификатор был установлен</returns>
        public bool SetSysIdentifier(ScAddress nodeAddress, Identifier nodeIdentifier)
        {
            bool isSuccesful = false;
            if (knowledgeBase.IsAvaible)
            {
                var command = new SetSystemIdCommand(nodeAddress, nodeIdentifier);
                var response = (SetSystemIdResponse) knowledgeBase.ExecuteCommand(command);
                isSuccesful = response.IsSuccesfull;
            }
            return isSuccesful;
        }
        
        /// <summary>
        /// Создает новый узел заданного типа
        /// </summary>
        /// <param name="nodeType">Тип создаваемого узла</param>
        /// <returns>SC-адрес созданного узла</returns>
        public ScAddress CreateNode(ElementType nodeType)
        {
            ScAddress nodeAddress = ScAddress.Invalid;
            if (knowledgeBase.IsAvaible)
            {
                var command = new CreateNodeCommand(nodeType);
                var response = (CreateNodeResponse)knowledgeBase.ExecuteCommand(command);
                nodeAddress = response.CreatedNodeAddress;
            }
            return nodeAddress;
        }

        /// <summary>
        /// Создание нового узла с заданным системным идентификаторомю
        /// </summary>
        /// <param name="nodeType">тип узла</param>
        /// <param name="sysIdentifier">системный идентификатор</param>
        /// <returns>SC-адрес</returns>
        public ScAddress CreateNode(ElementType nodeType, Identifier sysIdentifier)
        {
            ScAddress nodeAddress = GetNodeAddress(sysIdentifier);
            if (knowledgeBase.IsAvaible)
            {
                if (nodeAddress == ScAddress.Invalid)
                {
                    var command = new CreateNodeCommand(nodeType);
                    var response = (CreateNodeResponse) knowledgeBase.ExecuteCommand(command);
                    if (response.Header.ReturnCode == ReturnCode.Successfull)
                    {
                        if (SetSysIdentifier(response.CreatedNodeAddress, sysIdentifier))
                        {
                            nodeAddress = response.CreatedNodeAddress;
                        }
                    }
                }
            }
            return nodeAddress;
        }

        /// <summary>
        /// Получение адреса произвольного узла базы знаний.
        /// </summary>
        /// <param name="identifier">идентификатор</param>
        /// <returns>SC-адрес узла <see cref="ScAddress"/></returns>
        public ScAddress GetNodeAddress(Identifier identifier)
        {
            ScAddress address = ScAddress.Invalid;
            if (knowledgeBase.IsAvaible)
            {
                var command = new FindElementCommand(identifier);
                var response = (FindElementResponse) knowledgeBase.ExecuteCommand(command);
                address = response.FoundAddress;
            }
            return address;
        }

        /// <summary>
        /// Получение произвольного идентификатора. узла
        /// </summary>
        /// <param name="scAddress">SC-адрес узла</param>
        /// <param name="identifierType">тип идентификатора</param>
        /// <returns>идентификатор</returns>
        public Identifier GetNodeIdentifier(ScAddress scAddress, Identifier identifierType)
        {
            Identifier identifier = Identifier.Invalid;
            if (knowledgeBase.IsAvaible)
            {
                var template = new ConstructionTemplate(scAddress, ElementType.ConstantCommonArc_c, ElementType.Link_a, ElementType.PositiveConstantPermanentAccessArc_c, GetNodeAddress(identifierType));
                var command = new IterateElementsCommand(template);
                var response = (IterateElementsResponse)knowledgeBase.ExecuteCommand(command);
                if (response.Constructions.Count == 1)
                {
                    ScAddress link = response.Constructions[0][2];
                    var commandGetLink = new GetLinkContentCommand(link);
                    var responseGetLink = (GetLinkContentResponse)knowledgeBase.ExecuteCommand(commandGetLink);
                    if (responseGetLink.Header.ReturnCode == ReturnCode.Successfull)
                    {
                        identifier = LinkContent.ToString(responseGetLink.LinkContent);
                    }
                }
            }
            return identifier;
        }

        /// <summary>
        /// Получение системного идентификатора узла по известному адресу.
        /// </summary>
        /// <param name="scAddress">SC-адрес узда</param>
        /// <returns>идентификатор</returns>
        public Identifier GetNodeSysIdentifier(ScAddress scAddress)
        {
#warning nrel_system_identifier - magic string, заменить на нормальную константу.
            return GetNodeIdentifier(scAddress, new Identifier("nrel_system_identifier"));
        }

        #endregion

        #region Links

        /// <summary>
        /// Получение содержимого ссылки по известному адресу.
        /// </summary>
        /// <param name="scAddress">SC-адрес ссылки</param>
        /// <returns>содержимое ссылки</returns>
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
        /// Создание новой ссылки.
        /// </summary>
        /// <returns>SC-адрес созданной ссылки</returns>
        public ScAddress CreateLink()
        {
            ScAddress linkAddress = ScAddress.Invalid;
            if (knowledgeBase.IsAvaible)
            {
                var coomand = new CreateLinkCommand();
                var response = (CreateLinkResponse)knowledgeBase.ExecuteCommand(coomand);
                linkAddress = response.CreatedLinkAddress;
            }
            return linkAddress;
        }

        /// <summary>
        /// Получение ссылки по содержимому.
        /// </summary>
        /// <param name="linkContent">содержимое</param>
        /// <returns>список SC-адресов</returns>
        public List<ScAddress> GetLinksByContent(LinkContent linkContent)
        {
            var result = new List<ScAddress>();
            if (knowledgeBase.IsAvaible)
            {
                var command = new FindLinksCommand(linkContent);
                var response = (FindLinksResponse)knowledgeBase.ExecuteCommand(command);
                result = response.Addresses;
            }
            return result;
        }

        /// <summary>
        /// Задание содержимого для ссылки.
        /// </summary>
        /// <param name="linkAddress">SC-адрес ссылки</param>
        /// <param name="content">содержимое для ссылки</param>
        /// <returns><b>true</b>, если контент задан</returns>
        public bool SetLinkContent(ScAddress linkAddress, LinkContent content)
        {
            bool isSet = false;
            if (knowledgeBase.IsAvaible)
            {
                var command = new SetLinkContentCommand(linkAddress, content);
                var response = (SetLinkContentResponse) knowledgeBase.ExecuteCommand(command);
                isSet = response.ContentIsSet;
            }
            return isSet;
        }

        #endregion

        #region Arcs

        /// <summary>
        /// Создание дуги между двумя элементами.
        /// </summary>
        /// <param name="arcType">тип дуги</param>
        /// <param name="beginElement">SC-адрес начального элемента дуги</param>
        /// <param name="endElement">SC-адрес конечного элемента дуги</param>
        /// <returns>SC-адрес созданной дуги</returns>
        public ScAddress CreateArc(ElementType arcType, ScAddress beginElement, ScAddress endElement)
        {
            ScAddress arcAddress = ScAddress.Invalid;
            if (knowledgeBase.IsAvaible)
            {
                var command = new CreateArcCommand(arcType, beginElement, endElement);
                var response = (CreateArcResponse) knowledgeBase.ExecuteCommand(command);
                arcAddress = response.CreatedArcAddress;
            }
            return arcAddress;
        }

        #endregion

        #region Elements

        /// <summary>
        /// Получение типа элемента по известному адресу.
        /// </summary>
        /// <param name="scAddress">SC-адрес элемента</param>
        /// <returns>тип элемента</returns>
        public ElementType GetElementType(ScAddress scAddress)
        {
            var type = ElementType.Unknown;
            if (knowledgeBase.IsAvaible)
            {
                var command = new GetElementTypeCommand(scAddress);
                var response = (GetElementTypeResponse)knowledgeBase.ExecuteCommand(command);
                type = response.ElementType;
            }
            return type;
        }

        /// <summary>
        /// Проверка наличия элемента.
        /// </summary>
        /// <param name="scAddress">SC-адрес элемента</param>
        /// <returns><b>true</b>, если существует, иначе - <b>false</b></returns>
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

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="scAddress">SC-адрес элемента</param>
        /// <returns><b>true</b>, если был удалён, иначе - <b>false</b></returns>
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

        /// <summary>
        /// Получить коллекцию конструкций, соответствующих указанной конструкции-шаблону.
        /// </summary>
        /// <param name="template">Конструкция-шаблон.</param>
        /// <returns>Список конструкций (списков элементов).</returns>
        public List<List<ScAddress>> IterateElements(ConstructionTemplate template)
        {
            if (!knowledgeBase.IsAvaible) return new List<List<ScAddress>>();
            var cmd = new IterateElementsCommand(template);
            var rsp = (IterateElementsResponse) knowledgeBase.ExecuteCommand(cmd);
            return rsp.Constructions;
        }

        #endregion
    }
}
