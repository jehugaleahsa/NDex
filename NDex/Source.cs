﻿using System;
using System.Collections;
using System.Collections.Generic;
using NDex.Properties;

namespace NDex
{
    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TDestination">The type of the items in the destination sublist.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the CopyTo operation.</typeparam>
    public abstract class Source<TDestination, TResult>
    {
        /// <summary>
        /// Initializes a new instance of an Source.
        /// </summary>
        protected Source()
        {
        }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        public IExpandableSublist<TDestinationList, TDestination> AddTo<TDestinationList>(IExpandableSublist<TDestinationList, TDestination> destination)
            where TDestinationList : IList<TDestination>
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return SafeAddTo(destination);
        }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected abstract IExpandableSublist<TDestinationList, TDestination> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TDestination> destination)
            where TDestinationList : IList<TDestination>;

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A result object holding indexes into the lists where the operation stopped.</returns>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        public TResult CopyTo<TDestinationList>(IMutableSublist<TDestinationList, TDestination> destination)
            where TDestinationList : IList<TDestination>
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return SafeCopyTo(destination);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected abstract TResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TDestination> destination)
            where TDestinationList : IList<TDestination>;

        /// <summary>
        /// Gets an enumerator over a collection holding the results of applying the operation.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerable<TDestination> AsEnumerable()
        {
            return AsSublist();
        }

        /// <summary>
        /// Gets a Sublist wrapping the results of applying the operation.
        /// </summary>
        /// <returns>The Sublist.</returns>
        public IExpandableSublist<List<TDestination>, TDestination> AsSublist()
        {
            List<TDestination> destination = new List<TDestination>();
            return SafeAddTo(destination.ToSublist());
        }
    }
}
