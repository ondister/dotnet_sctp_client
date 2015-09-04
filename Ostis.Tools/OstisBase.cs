using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ostis.Sctp;           // общее пространство имен, обязательно для подключения
using Ostis.Sctp.Arguments; // пространство имен аргументов команд
using Ostis.Sctp.Commands;  // пространство имен команд, отправляемых серверу
using Ostis.Sctp.Responses;
using System.Threading; // пространство имен ответов сервера

namespace Ostis.Tools
{
   internal sealed class OstisBase
    {
       private readonly SctpClient sctpClient;
       public OstisBase()
       {
        string serverAddress=   ConfigurationManager.AppSettings["ServerIP"];
        int serverPort = Int32.Parse( ConfigurationManager.AppSettings["ServerPort"]);
        sctpClient = new SctpClient(serverAddress, serverPort);
               try
            {
                sctpClient.Connect();

                Console.WriteLine("Socket connected to " + sctpClient.ServerEndPoint);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
       }

       /// <summary>
       /// Проверяет узлы в базе знаний на наличие хотя бы одного основного идентификатора. 
       /// Как правило, при разработке баз знаний всем узлам назначают основные идентификаторы и их отсутствие позволяет выявить ошибки.
       /// </summary>
   public void CheckMainIdtf()
       {
           if (sctpClient.IsConnected)
           {
               //ищем адрес идентификатора узел
               var commandFindByID = new FindElementCommand(new Identifier("nrel_system_identifier"));
               var responseFindByID = (FindElementResponse)sctpClient.Send(commandFindByID);
               Console.WriteLine(responseFindByID.Header.ReturnCode);
               if (responseFindByID.Header.ReturnCode==ReturnCode.Successfull)
               {
                
                   //ищем адреса всех дуг, в которые входит идентификатор
                   var template = new ConstructionTemplate(responseFindByID.FoundAddress, ElementType.AccessArc, ElementType.CommonArc);
                   var commandIterate = new IterateElementsCommand(template);
                   var responseIterate = (IterateElementsResponse)sctpClient.Send(commandIterate);
                   Console.WriteLine(responseIterate.Constructions.Count);

                   foreach (var construction in responseIterate.Constructions)
                   {
                       //ищем узел, из которого отходит дуга
                       var commandGetNode = new GetArcElementsCommand(construction[2]);
                       var responseGetNode = (GetArcElementsResponse)sctpClient.Send(commandGetNode);
                       //искомый узел будет responseGetNode.BeginElementAddress, а ссылка  responseGetNode.EndElementAddress
                       var commandGetLinkContent = new GetLinkContentCommand(responseGetNode.EndElementAddress);
                       var respondeGetLinkContent = (GetLinkContentResponse) sctpClient.Send(commandGetLinkContent);
                       //теперь смотрим, есть ли у него хотя бы один основной идентификатор 
                       //для этого смотрим адрес идентификатора
                       var cmdFindBydId = new  FindElementCommand(new Identifier("nrel_main_idtf"));                       
                       var rspFindById= (FindElementResponse)sctpClient.Send(cmdFindBydId);
           
                       //и итерируем 
                       var itertemplate=new ConstructionTemplate(responseGetNode.BeginElementAddress,ElementType.CommonArc,ElementType.Link,ElementType.AccessArc,rspFindById.FoundAddress);
                       var cmdIterate=new IterateElementsCommand(itertemplate);
                       var rspIterate= (IterateElementsResponse)sctpClient.Send(cmdIterate);
                       if (rspIterate.Constructions.Count==0)
                       {
                       Console.WriteLine(LinkContent.ToString(respondeGetLinkContent.LinkContent));
                       }
                   }
               }
           }
           Console.WriteLine("Найдены все узлы, не имеющие основного идентификатора");
       }

   /// <summary>
   /// Позволяет искать узлы к которым нет входящих дуг.
   /// Это позволяет искать ошибки из-за неправильного написания системных идентификаторов.
   /// </summary>
   public void FindUpperNodes()
   {
       if (sctpClient.IsConnected)
       {
          //ищем адрес идентификатора узел
               var commandFindByID = new FindElementCommand(new Identifier("nrel_system_identifier"));
               var responseFindByID = (FindElementResponse)sctpClient.Send(commandFindByID);
               Console.WriteLine(responseFindByID.Header.ReturnCode);
               if (responseFindByID.Header.ReturnCode == ReturnCode.Successfull)
               {

                   //ищем адреса всех дуг, в которые входит идентификатор
                   var template = new ConstructionTemplate(responseFindByID.FoundAddress, ElementType.AccessArc, ElementType.CommonArc);
                   var commandIterate = new IterateElementsCommand(template);
                   var responseIterate = (IterateElementsResponse)sctpClient.Send(commandIterate);
                   Console.WriteLine(responseIterate.Constructions.Count);
                   var constr = responseIterate.Constructions;

                   foreach (var construction in constr)
                   {
                       int cnt = construction.Count;
                       //ищем узел, из которого отходит дуга
                       var commandGetNode = new GetArcElementsCommand(construction[2]);
                       var responseGetNode = (GetArcElementsResponse)sctpClient.Send(commandGetNode);
                       //искомый узел будет responseGetNode.BeginElementAddress, а ссылка  responseGetNode.EndElementAddress
                       var commandGetLinkContent = new GetLinkContentCommand(responseGetNode.EndElementAddress);
                       var respondeGetLinkContent = (GetLinkContentResponse)sctpClient.Send(commandGetLinkContent);
                       //и итерируем по дугам общего вида
                       var itertemplateComm = new ConstructionTemplate(ElementType.Node,ElementType.CommonArc,responseGetNode.BeginElementAddress);
                       var cmdIterateComm = new IterateElementsCommand(itertemplateComm);
                       var rspIterateComm = (IterateElementsResponse)sctpClient.Send(cmdIterateComm);

                       //и итерируем по дугам принадлежности
                       var itertemplateAcc= new ConstructionTemplate(ElementType.Node, ElementType.AccessArc, responseGetNode.BeginElementAddress);
                       var cmdIterateAcc = new IterateElementsCommand(itertemplateAcc);
                       var rspIterateAcc = (IterateElementsResponse)sctpClient.Send(cmdIterateAcc);

                       if (rspIterateComm.Constructions.Count + rspIterateAcc.Constructions.Count == 0 )
                       {
                           Console.WriteLine(LinkContent.ToString(respondeGetLinkContent.LinkContent));
                       }
                   }
               }
       
       
       
       }
       Console.WriteLine("Найдены все узлы, не имеющие входящих дуг");
   }

   public void Test()
   {
       var cmd1 = new CreateSubscriptionCommand(EventsType.DeleteInArc, new ScAddress(2, 3));
       var bytes1= cmd1.GetBytes();
       var cmd2 = new CheckElementCommand(new ScAddress(2, 3));
       var bytes2 = cmd2.GetBytes();
      
   }

   

   }
}
