﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    internal static class TestHelper
    {
        public static IExpandableSublist<List<int>, int> Wrap(List<int> list)
        {
            int count = list.Count;
            var wrapper = new List<int>();
            Sublist.Generate(100, i => i).AddTo(wrapper.ToSublist());
            wrapper.ToSublist().AddTo(list.ToSublist(0, 0)); // add to front
            wrapper.ToSublist().AddTo(list.ToSublist()); // add to back
            var sublist = list.ToSublist(100, count);
            return sublist;
        }

        public static IReadOnlySublist<List<int>, int> WrapReadOnly(List<int> list)
        {
            return Wrap(list);
        }

        public static IExpandableSublist<List<int>, int> Populate(IReadOnlySublist<List<int>, int> sublist)
        {
            return new Sublist<List<int>, int>(sublist.List, sublist.Offset, sublist.Count);
        }

        public static void CheckHeaderAndFooter(IReadOnlySublist<List<int>, int> list)
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
