using System;
using System.Collections.Generic;

namespace DionysosFX.Collections
{
    public interface ICollection<T>
    {
        int Count { get; }
        void Add(T item);
        void AddRange(IEnumerable<T> items);
        T[] ToArray();
        void Clear();
        bool Remove(T item);
        void RemoveAt(int index);
        void RemoveRange(int index, int count);
        int RemoveAll(System.Predicate<T> predicate);
        bool Any();
        IEnumerable<T> Where(Func<T, bool> predicate);        
    }
}
