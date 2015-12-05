using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostis.Sctp.Tools
{
#warning Чтобы работать с ElementState, как с набором флагов (что крайне не рекомендуется), стоит пометить это перечисление атрибутом [Flags], в противном случае класс ElementStateExtensionMethods должен быть предан огню и мечу.
    /// <summary>
    /// Методы расширения для <see cref="ElementState"/>
    /// </summary>
    public static class ElementStateExtensionMethods
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="elementStates"></param>
       /// <param name="elementType"></param>
       /// <returns></returns>
        public static Boolean IsType(this ElementState elementStates, ElementState elementState)
        {
            bool isset = false;
            if (elementState != 0)
            {
                isset = (elementStates & elementState) == elementState;
            }
            return isset;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="elementStates"></param>
       /// <param name="testElementStates"></param>
       /// <returns></returns>
        public static Boolean HasAnyState(this ElementState elementStates, ElementState testElementStates)
        {
            return ((elementStates & testElementStates) != 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementStates"></param>
        /// <param name="addElementStates"></param>
        /// <returns></returns>
        public static ElementState AddState(this ElementState elementStates, ElementState addElementStates)
        {
            return elementStates | addElementStates;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementStates"></param>
        /// <param name="removeElementStates"></param>
        /// <returns></returns>
        public static ElementState RemoveState(this ElementState elementStates, ElementState removeElementStates)
        {
            return elementStates & ~removeElementStates;
        }
       
    }

}
