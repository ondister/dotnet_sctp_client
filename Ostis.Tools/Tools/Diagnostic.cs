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
    ///// <summary>
    ///// Класс с методами для диагностики абстрактной базы знаний
    ///// </summary>
    //public class Diagnostic
    //{
    //    private KnowledgeBase knowledgeBase;
    //    /// <summary>
    //    /// Инициализирует новый класс<see cref="Diagnostic"/>.
    //    /// </summary>
    //    /// <param name="knowledgeBase">Абстрактная база знаний</param>
    //    public Diagnostic(KnowledgeBase knowledgeBase)
    //    {
    //        this.knowledgeBase = knowledgeBase;
    //    }

    //    /// <summary>
    //    /// Выводит в указанный файл системные идентификаторы узлов, у которых отсутствует хотя бы один основной идентификатор
    //    /// </summary>
    //    /// <param name="fileName">Имя файла для результата</param>
    //    /// <remarks>
    //    /// Как правило, при разработке баз знаний всем узлам назначают основные идентификаторы и их отсутствие может означать, что либо узел не описан, либо это ошибочное название узла, то есть узел дубликат.
    //    /// <para>Например есть узел test_node, а в другом scs файле он записан с ошибкой, например test_nade</para>
    //    /// </remarks>
    //    public void GetNodesWithoutMainIdtf(String fileName)
    //    {
    //        using (System.IO.StreamWriter file =
    //        new System.IO.StreamWriter(fileName))
    //        {
    //            //ищем адреса всех дуг, в которые входит идентификатор
    //            var template = new ConstructionTemplate(knowledgeBase.Commands.GetNodeAddress("nrel_system_identifier"), ElementType.AccessArc_a, ElementType.CommonArc_a);
    //            var cmdIterateArcs = new IterateElementsCommand(template);
    //            var rspIterateArcs = (IterateElementsResponse)knowledgeBase.ExecuteCommand(cmdIterateArcs);

    //            foreach (var construction in rspIterateArcs.Constructions)
    //            {
    //                //ищем узел, из которого отходит дуга
    //                var cmdGetNode = new GetArcElementsCommand(construction[2]);
    //                var responseGetNode = (GetArcElementsResponse)knowledgeBase.ExecuteCommand(cmdGetNode);
    //                //искомый узел будет responseGetNode.BeginElementAddress, а ссылка  responseGetNode.EndElementAddress
    //                var cmdGetLinkContent = new GetLinkContentCommand(responseGetNode.EndElementAddress);
    //                var rspGetLinkContent = (GetLinkContentResponse)knowledgeBase.ExecuteCommand(cmdGetLinkContent);
    //                //теперь смотрим, есть ли у него хотя бы один основной идентификатор 
    //                //для этого смотрим адрес идентификатора
    //                var itertemplate = new ConstructionTemplate(responseGetNode.BeginElementAddress, ElementType.CommonArc_a, ElementType.Link_a, ElementType.AccessArc_a, knowledgeBase.Commands.GetNodeAddress("nrel_main_idtf"));
    //                var cmdIterate = new IterateElementsCommand(itertemplate);
    //                var rspIterate = (IterateElementsResponse)knowledgeBase.ExecuteCommand(cmdIterate);
    //                //и если нет, то записываем системный идентификатор в файл
    //                if (rspIterate.Constructions.Count == 0)
    //                {
    //                    file.WriteLine(LinkContent.ToString(rspGetLinkContent.LinkContent));
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Выводит в указанный файл идентиффикаторы узлов к которым нет входящих дуг.
    //    /// </summary>
    //    /// <param name="fileName">Имя файла для результата</param>
    //    /// <remarks>
    //    /// В семантической сети количество узлов, к которым нет входящих дуг должно быть минимально. Это узлы ключевых понятий, их можно по пальцам посчитать.
    //    /// <para>И если к узлу нет входящих дуг, то он или верхнего уровня, или узел дубликат, идентификатор, которого написан с ошибкой</para>
    //    /// </remarks>
    //    public void GetNodesWithoutInputArcs(String fileName)
    //    {
    //        using (System.IO.StreamWriter file =
    //        new System.IO.StreamWriter(fileName))
    //        {
    //            //ищем адреса всех дуг, в которые входит идентификатор
    //            var template = new ConstructionTemplate(knowledgeBase.Commands.GetNodeAddress("nrel_system_identifier"), ElementType.AccessArc_a, ElementType.CommonArc_a);
    //            var cmdIterateArcs = new IterateElementsCommand(template);
    //            var rspIterateArcs = (IterateElementsResponse)knowledgeBase.ExecuteCommand(cmdIterateArcs);

    //            foreach (var construction in rspIterateArcs.Constructions)
    //            {
    //                //ищем узел, из которого отходит дуга
    //                var cmdGetNode = new GetArcElementsCommand(construction[2]);
    //                var responseGetNode = (GetArcElementsResponse)knowledgeBase.ExecuteCommand(cmdGetNode);
    //                //искомый узел будет responseGetNode.BeginElementAddress, а ссылка  responseGetNode.EndElementAddress
    //                var cmdGetLinkContent = new GetLinkContentCommand(responseGetNode.EndElementAddress);
    //                var rspGetLinkContent = (GetLinkContentResponse)knowledgeBase.ExecuteCommand(cmdGetLinkContent);
    //                //и итерируем по дугам общего вида
    //                var itertemplateComm = new ConstructionTemplate(ElementType.Node_a, ElementType.CommonArc_a, responseGetNode.BeginElementAddress);
    //                var cmdIterateComm = new IterateElementsCommand(itertemplateComm);
    //                var rspIterateComm = (IterateElementsResponse)knowledgeBase.ExecuteCommand(cmdIterateComm);
    //                //и итерируем по дугам принадлежности
    //                var itertemplateAcc = new ConstructionTemplate(ElementType.Node_a, ElementType.AccessArc_a, responseGetNode.BeginElementAddress);
    //                var cmdIterateAcc = new IterateElementsCommand(itertemplateAcc);
    //                var rspIterateAcc = (IterateElementsResponse)knowledgeBase.ExecuteCommand(cmdIterateAcc);

    //                if (rspIterateComm.Constructions.Count + rspIterateAcc.Constructions.Count == 0)
    //                {
    //                    file.WriteLine(LinkContent.ToString(rspGetLinkContent.LinkContent));
    //                }


    //            }
    //        }
    //    }


    //}
}
