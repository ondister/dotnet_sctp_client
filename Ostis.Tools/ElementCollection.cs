using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Коллекция элементов базы знаний.
    /// </summary>
    /// <typeparam name="T">тип элемента</typeparam>
    public class ElementCollection<T> : ICollection<T>
        where T : ElementBase
    {
        /// <summary>
        /// ctor,
        /// </summary>
        /// <param name="knowledgeBase">база знаний</param>
        internal ElementCollection(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
            objectsCache = new Dictionary<ScAddress, T>();
        }

        private readonly KnowledgeBase knowledgeBase;

        internal KnowledgeBase KnowledgeBase
        { get { return knowledgeBase; } }

        private readonly Dictionary<ScAddress, T> objectsCache;
        
        #region Public-интерфейс

        /// <summary>
        /// Элемент с соответствующим SC-адресом.
        /// </summary>
        /// <param name="address">адрес</param>
        /// <returns>элемент по адресу</returns>
        public T this[ScAddress address]
        {
            get
            {
                T element;
                if (!objectsCache.TryGetValue(address, out element))
                {
                    ElementLoader loader;
                    elementLoaders.TryGetValue(typeof (T), out loader);
#warning Здесь следует заменить elementLoaders.TryGetValue(typeof (T), out loader) на elementLoaders[typeof (T)] для избежания трудноотлавливаемых ошибок.
                    element = (T) loader(knowledgeBase, address);
                    Add(element);
                }
                return element;
            }
        }

        /// <summary>
        /// Применение всех изменений.
        /// </summary>
        public void SaveChanged()
        {
            foreach (var element in this)
            {
                if (element.State != ElementState.Synchronized)
                {
                    element.Save(knowledgeBase);
                }
            }
#warning Стоит ли очищать при этом базу? Такая логика подошла бы для реализации интерфейса IDisposible.
            Clear();
        }

        #endregion

        #region Методы загрузки элементов

        private delegate ElementBase ElementLoader(KnowledgeBase knowledgeBase, ScAddress address);

        private static readonly Dictionary<Type, ElementLoader> elementLoaders = new Dictionary<Type, ElementLoader>
        {
            {typeof (Arc), loadArc},
            {typeof (Link), loadLink},
            {typeof (Node), loadNode},
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

        #region Implementation of ICollection<T>

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public void Add(T item)
        {
            item.Save(this.knowledgeBase); //сразу добавляем и в базу знаний
#warning В каком состоянии находится при этом элемент?
            if (!objectsCache.ContainsKey(item.Address))
            {
                objectsCache.Add(item.Address, item);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. </exception>
        public void Clear()
        {
            objectsCache.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        public bool Contains(T item)
        {
            return objectsCache.ContainsKey(item.Address);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            objectsCache.Values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        { get { return objectsCache.Count; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        { get { return false; } }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public bool Remove(T item)
        {
            item.State = ElementState.Deleted;
            item.Save(knowledgeBase);
#warning В каком состоянии находится при этом элемент?
            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return objectsCache.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}