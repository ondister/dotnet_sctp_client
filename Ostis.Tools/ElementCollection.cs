using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
   public  class ElementCollection<T>:ICollection<T>
        where T : ElementBase
    {
        internal ElementCollection(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
            objectsCache = new Dictionary<ScAddress, T>();
            
        }





        private readonly KnowledgeBase knowledgeBase;

        internal  KnowledgeBase KnowledgeBase
        {
            get { return knowledgeBase; }
        } 

        private readonly Dictionary<ScAddress, T> objectsCache;

       


        #region Public-интерфейс

            
       public T this[ScAddress address]
       {
           get
           {
               T foudObject;
               if (!objectsCache.TryGetValue(address, out foudObject))
               {
                   ElementLoader loader;
                   this.elementLoaders.TryGetValue(typeof(T), out loader);
                   foudObject = (T)loader(knowledgeBase, address);
                   this.Add(foudObject);
               }
               
                   return foudObject;

           }
       }


        //public T this[ScAddress address]
        //{
        //    get
        //    {
        //        List<T> objectsList;
        //        T foundObject;
        //        if (!objectsCache.TryGetValue(address, out objectsList))
        //        {
        //            objectsCache[address] = objectsList = new List<T>();
        //            // requestObjectFromOstisServer
        //            // storeRequestedInCache
        //            // returnLoadedIfFound
        //            // ifMissedReturnNullOrThrewException
        //        }
        //        else
        //        {
        //            return objectsList.First(o => o.State != ElementState.Deleted) /*FirstOrDefault*/;
        //        }
        //    }
        //    set
        //    {
        //        List<T> objectsList;
        //        if (!objectsCache.TryGetValue(address, out objectsList))
        //        {
        //            requestFromKnowledgeBase
        //        }
        //        objectsList.ForEach(if sync or edited => deleted, if new => simple remove from list);
        //        objectsList.Add(value);
        //        value.State = if sync => edited, etc.
        //    }
        //}

        #endregion

       

        #region Методы загрузки элементов

        private delegate ElementBase ElementLoader(KnowledgeBase knowledgeBase, ScAddress address);
        private Dictionary<Type, ElementLoader> elementLoaders = new Dictionary<Type, ElementLoader>
        {
            { typeof(Arc), loadArc },
            { typeof(Link), loadLink },
            { typeof(Node), loadNode },
        };

        private static Arc loadArc(KnowledgeBase knowledgeBase, ScAddress address)
        {

            return Arc.Load(knowledgeBase, address);
        }

        private static Link loadLink(KnowledgeBase knowledgeBase, ScAddress address)
        {
            return Link.Load(knowledgeBase, address);
        }

        private static Node loadNode(KnowledgeBase knowledgeBase, ScAddress address)
        {
            return Node.Load(knowledgeBase, address);
        }

        #endregion

        #region ICollection
        public void Add(T item)
        {
           
item.Save(this.knowledgeBase);//сразу добавляем и в базу знаний
            
            if(!objectsCache.ContainsKey(item.Address))
            {
                objectsCache.Add(item.Address, item);
            }
        }

        public void Clear()
        {
            objectsCache.Clear();
        }

        public bool Contains(T item)
        {
            return objectsCache.ContainsKey(item.Address);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            objectsCache.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return objectsCache.Count; }
        }

        public bool IsReadOnly
        {
            
            get { return false; }
        }

        public bool Remove(T item)
        {
            item.State = ElementState.Deleted;
            item.Save(this.knowledgeBase);
            return true ;
        }

        public IEnumerator<T> GetEnumerator()
        {
          return  objectsCache.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (ElementBase element in objectsCache.Values)
            {
                yield return element;
            }

        }
        #endregion

        public void SaveChanged()
        {
            foreach (var element in this)
            {
                if (element.State != ElementState.Synchronized)
                {
                    element.Save(knowledgeBase);
                }
            }
            this.Clear();
        }
    }
}

