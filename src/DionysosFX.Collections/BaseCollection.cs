using System;
using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Collections
{
    internal class BaseCollection<T> : ICollection<T>
    {
        private List<T> _collection = null;

        public BaseCollection()
        {
            _collection = new List<T>();
        }

        public int Count => _collection.Count;

        public void Add(T item)
        {
            _collection.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            _collection.AddRange(items);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Remove(T item) => _collection.Remove(item);       

        public void RemoveAt(int index) => _collection.RemoveAt(index);

        public void RemoveRange(int index, int count) 
        {
            _collection.RemoveRange(index, count);
        }

        public int RemoveAll(Predicate<T> predicate)
        {            
            _collection.Select
            return _collection.RemoveAll(predicate);
        }


        public T[] ToArray() => _collection.ToArray();

        public bool Any() => _collection.Any();

        public IEnumerable<T> Where(Func<T, bool> predicate) => _collection.Where(predicate);
        
    }
}
