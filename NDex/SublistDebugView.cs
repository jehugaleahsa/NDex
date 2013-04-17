using System;
using System.Collections.Generic;

namespace NDex
{
    internal class SublistDebugView<TList, T>
        where TList : IList<T>
    {
        private readonly Sublist<TList, T> _sublist;

        public SublistDebugView(Sublist<TList, T> sublist)
        {
            _sublist = sublist;
        }

        public int Offset
        {
            get
            {
                return _sublist.Offset;
            }
        }

        public int Count
        {
            get
            {
                return _sublist.Count;
            }
        }

        public TList List
        {
            get
            {
                return _sublist.List;
            }
        }

        public T[] Splice
        {
            get
            {
                T[] items = new T[_sublist.Count];
                Sublist.Copy<TList, T[], T>(
                    _sublist.List, _sublist.Offset, _sublist.Offset + _sublist.Count,
                    items, 0, items.Length);
                return items;
            }
        }

    }
}
