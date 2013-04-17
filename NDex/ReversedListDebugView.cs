using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDex
{
    internal class ReversedListDebugView<TList, T>
        where TList : IList<T>
    {
        private readonly ReversedList<TList, T> _list;

        public ReversedListDebugView(ReversedList<TList, T> list)
        {
            _list = list;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] List
        {
            get
            {
                T[] items = new T[_list.Count];
                _list.CopyTo(items, 0);
                return items;
            }
        }
    }
}
