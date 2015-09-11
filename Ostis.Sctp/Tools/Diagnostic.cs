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
    public class Diagnostic
    {
        private KnowledgeBase knowledgeBase;
        public Diagnostic(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }

        /// <summary>
        /// Выводит в указанный файл системные идентификаторы узлов, у которых отсутствует хотя бы один основной идентификатор
        /// Как правило, при разработке баз знаний всем узлам назначают основные идентификаторы и их отсутствие позволяет выявить ошибки.
        /// </summary>
        public void GetNodesWithoutMainIdtf(String fileName)
        {
            
                    //ищем адреса всех дуг, в которые входит идентификатор
                    var template = new ConstructionTemplate(knowledgeBase.GetNodeAddress("nrel_system_identifier"), ElementType.AccessArc, ElementType.CommonArc);
                    var cmdIterateArcs = new IterateElementsCommand(template);
                    knowledgeBase.RunAsyncCommand(cmdIterateArcs);
                    var rspIterateArcs = (IterateElementsResponse)knowledgeBase.LastAsyncResponse;

                    foreach (var construction in rspIterateArcs.Constructions)
                    {
                        //ищем узел, из которого отходит дуга
                        var cmdGetNode = new GetArcElementsCommand(construction[2]);
                        knowledgeBase.RunAsyncCommand(cmdGetNode);
                        var responseGetNode = (GetArcElementsResponse)knowledgeBase.LastAsyncResponse;
                        //искомый узел будет responseGetNode.BeginElementAddress, а ссылка  responseGetNode.EndElementAddress
                        var cmdGetLinkContent = new GetLinkContentCommand(responseGetNode.EndElementAddress);
                        knowledgeBase.RunAsyncCommand(cmdGetLinkContent);
                        var rspGetLinkContent = (GetLinkContentResponse)knowledgeBase.LastAsyncResponse;
                        //теперь смотрим, есть ли у него хотя бы один основной идентификатор 
                        //для этого смотрим адрес идентификатора
                        var itertemplate = new ConstructionTemplate(responseGetNode.BeginElementAddress, ElementType.CommonArc, ElementType.Link, ElementType.AccessArc, knowledgeBase.GetNodeAddress("nrel_main_idtf"));
                        var cmdIterate = new IterateElementsCommand(itertemplate);
                        knowledgeBase.RunAsyncCommand(cmdIterate);
                        var rspIterate = (IterateElementsResponse)knowledgeBase.LastAsyncResponse;
                        int i=rspIterate.Constructions.Count;
                        //и если нет, то записываем системный идентификатор в файл
                        if (rspIterate.Constructions.Count == 0)
                        {
                         //LinkContent.ToString(rspGetLinkContent.LinkContent);
                        }
                    }
        }

        /// <summary>
        /// Выводит в указанный файл идентиффикаторы узлов к которым нет входящих дуг.
        /// Это позволяет искать ошибки из-за неправильного написания системных идентификаторов.
        /// </summary>
        public void GetNodesWithoutInputArcs(String FileName)
        {

        }

    }
}
