using System.Collections;
using System.Diagnostics;

namespace NDex
{
#if NET45
    internal class TypedListDebugView<TList, T>
        where TList : IList
    {
        private readonly TypedList<TList, T> _list;

        public TypedListDebugView(TypedList<TList, T> list)
        {
            _list = list;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object List
        {
            get
            {
                return (object)_list.List;
            }
        }
    }
#endif
}
