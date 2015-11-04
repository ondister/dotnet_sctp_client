using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
   public class Node:ElementBase
    {
        private Identifier systemIdentifier;
        private string prefix;
        public Identifier SystemIdentifier
        {
            get { return systemIdentifier; }
            set
            {
                systemIdentifier = value;
                base.PropertyChanged();
            }
        }

       
         public Node(ElementType type,Identifier systemIdentifier)
             : base(type)
         {
             this.systemIdentifier = systemIdentifier;
         }
         public Node(ElementType type, string prefix)
             : base(type)
         {
             this.prefix = prefix;
             this.SystemIdentifier = Identifier.Unique;
            base.State= base.State.AddState(ElementState.Edited);
          
         }

        
       internal static Node Load(KnowledgeBase knowledgeBase, ScAddress scAddress)
       {
           Node node = new Node(ElementType.Unknown,Identifier.Invalid);
           bool isExist = knowledgeBase.Commands.IsElementExist(scAddress);
           if (isExist == true)
           {
               node.Address = scAddress;
               node.SystemIdentifier = knowledgeBase.Commands.GetNodeSysIdentifier(scAddress);
               node.Type = knowledgeBase.Commands.GetElementType(scAddress);
               node.State = ElementState.Synchronized;
               node.OnPropertyChanged += node_OnPropertyChanged;
           }
           return node;
       }

       static void node_OnPropertyChanged(ElementBase sender)
       {
           sender.State = sender.State.RemoveState(ElementState.Synchronized);
           sender.State = sender.State.AddState(ElementState.Edited);
       }

      

      internal override bool Save(KnowledgeBase knowledgeBase)
       {

           bool isSaved = false;
           if (base.State.HasAnyState(ElementState.New))
           {
               this.CreateNew(knowledgeBase);
               base.State = base.State.RemoveState(ElementState.New);
           }
           if (base.State.HasAnyState(ElementState.Edited))
           {
               this.Modify(knowledgeBase);
               base.State = base.State.RemoveState(ElementState.Edited);
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
           base.Address= knowledgeBase.Commands.CreateNode(base.Type);
       }

       private bool Modify(KnowledgeBase knowledgeBase)
       {
           if (this.systemIdentifier == Identifier.Unique) {this.SystemIdentifier= knowledgeBase.Commands.GenerateUniqueSysIdentifier(base.Address,this.prefix); }
           return knowledgeBase.Commands.SetSysIdentifier(base.Address, this.SystemIdentifier);
       }


       private bool Delete(KnowledgeBase knowledgeBase)
       {
           return knowledgeBase.Commands.DeleteElement(base.Address);
       }
    }
}
