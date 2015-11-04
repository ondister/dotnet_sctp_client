using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;
using Ostis.Sctp.Commands;
using Ostis.Sctp.Responses;
using Ostis.Sctp;

namespace Ostis.Sctp.Tools
{
    public class Arc:ElementBase
    {

        private ElementBase beginElement;

        public ElementBase BeginElement
        {
            get { return beginElement; }
        }
        private ElementBase endElement;
         
        public ElementBase EndElement
        {
            get { return endElement; }

        }

        public Arc(ElementType type,ElementBase beginElement, ElementBase endElement)
            : base(type)
        {
            this.beginElement = beginElement;
            this.endElement = endElement;
        }

        internal static Arc Load(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
              Arc arc = null;
              bool isExist = knowledgeBase.Commands.IsElementExist(scAddress);
              
              if (isExist == true)
              {
                  ElementType type=knowledgeBase.Commands.GetElementType(scAddress);
                  bool isArc = type.HasAnyType(ElementType.ArcMask_c);
                  if (isArc==true)
                  {
                    
#warning здесь возможна бесконечная цикла если элементы дуги тоже дуги бесконечно
                      var cmdGetArcElements = new GetArcElementsCommand(scAddress);
                      var rspgetArcElements = (GetArcElementsResponse)knowledgeBase.ExecuteCommand(cmdGetArcElements);
                      ElementBase beginElement= ElementBase.LoadElement(knowledgeBase,rspgetArcElements.BeginElementAddress);
                      ElementBase endElement = ElementBase.LoadElement(knowledgeBase, rspgetArcElements.EndElementAddress);

                      arc = new Arc(type, beginElement, endElement);
                      arc.Address = scAddress;
                      arc.State = ElementState.Synchronized;
                  }
              }

            return arc;
        }

       
       
        internal override bool Save(KnowledgeBase knowledgeBase)
        {

            bool isSaved = false;
            if (base.State.HasAnyState(ElementState.New))
            {
                this.CreateNew(knowledgeBase);
                base.State = base.State.RemoveState(ElementState.New);
            }
          
            if (base.State.HasAnyState(ElementState.Deleted))
            {
                this.Delete(knowledgeBase);
                base.State = base.State.RemoveState(ElementState.Deleted);
            }
            base.State = base.State.AddState(ElementState.Synchronized);
            return isSaved;
        }

        private void CreateNew(KnowledgeBase knowledgeBase)
        {
            //добавлять в соответствующие коллекции элементов перед использованием в виде вершин дуг
           
            base.Address = knowledgeBase.Commands.CreateArc(this.Type, this.beginElement.Address, this.endElement.Address);
        }

        private bool Delete(KnowledgeBase knowledgeBase)
        {
            return knowledgeBase.Commands.DeleteElement(base.Address);
        }
    }
}
