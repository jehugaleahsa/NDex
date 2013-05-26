using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDex
{
    internal sealed class ComparisonWrapper<T> : Comparer<T>
    {
        private readonly Func<T, T, int> _comparison;

        public static IComparer<T> GetComparer(Func<T, T, int> comparison)
        {
            IComparer<T> source = comparison.Target as IComparer<T>;
            return source ?? new ComparisonWrapper<T>(comparison);
        }

        private ComparisonWrapper(Func<T, T, int> comparison)
        {
            _comparison = comparison;
        }

        public override int Compare(T x, T y)
        {
            return _comparison(x, y);
        }
    }
}
