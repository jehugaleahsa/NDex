using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDex
{
    internal class ReadOnlyListDebugView<TList, T>
        where TList : IList<T>
    {
        private readonly ReadOnlyList<TList, T> _list;

        public ReadOnlyListDebugView(ReadOnlyList<TList, T> list)
        {
            _list = list;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public TList List
        {
            get
            {
                return _list.List;
            }
        }
    }
}
