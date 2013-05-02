using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    internal static class TestHelper
    {
        public static IExpandableSublist<List<int>, int> Wrap(List<int> list)
        {
            int count = list.Count;
            var wrapper = new List<int>();
            Sublist.AddGenerated(wrapper.ToSublist(), 100, i => i);
            Sublist.Add(wrapper.ToSublist(), list.ToSublist(0, 0)); // add to front
            Sublist.Add(wrapper.ToSublist(), list.ToSublist()); // add to back
            var sublist = new Sublist<List<int>, int>(list, 100, count);
            return sublist;
        }

        public static void CheckHeaderAndFooter(IExpandableSublist<List<int>, int> list)
        {
            for (int index = 0, value = 0; index != list.Offset; ++index, ++value)
            {
                Assert.AreEqual(value, list.List[index], "The header has been corrupted.");
            }
            for (int index = list.Offset + list.Count, value = 0; index != list.List.Count; ++index, ++value)
            {
                Assert.AreEqual(value, list.List[index], "The footer has been corrupted.");
            }
        }
    }
}
