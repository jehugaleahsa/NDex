using System;
using System.Collections.Generic;

namespace NDex
{
    /// <summary>
    /// Creates a view into a list starting at an offset and containing a designated number of items,
    /// which does not allow modification of the items.
    /// </summary>
    /// <typeparam name="TList">The type of the underlying list.</typeparam>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public interface IReadOnlySublist<TList, T> : IEnumerable<T>
    {
        /// <summary>
        /// Creates a new Sublist that acts as a splice into the Sublist, starting at the given offset.
        /// </summary>
        /// <param name="offset">The offset into the Sublist to start the new splice.</param>
        /// <returns>A new Sublist starting at the given offset into the Sublist, consisting of the remaining items.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the Sublist.</exception>
        IReadOnlySublist<TList, T> Nest(int offset);

        /// <summary>
        /// Creates a new Sublist that acts as a splice into the Sublist, starting at the given offset, spanning
        /// the number of items specified by the count.
        /// </summary>
        /// <param name="offset">The offset into the Sublist to start the new splice.</param>
        /// <param name="count">The number of items to include in the splice.</param>
        /// <returns>
        /// A new Sublist starting at the given offset into the Sublist, spanning the number of items specified by the count.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the Sublist.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The count is negative -or-  beyond the bounds of the Sublist.
        /// </exception>
        IReadOnlySublist<TList, T> Nest(int offset, int count);

        /// <summary>
        /// Gets the underlying list.
        /// </summary>
        TList List
        {
            get;
        }

        /// <summary>
        /// Gets or sets the offset into the underlying list.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The value is negative -or- outside the bounds of the underlying list.
        /// </exception>
        /// <remarks>If Sublist's count is adjusted automatically to prevent the splice for going beyond the end of the list.</remarks>
        int Offset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of items to include in the Sublist.
        /// </summary>
        int Count
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the item at the given index.
        /// </summary>
        /// <param name="index">The index into the Sublist to get the item.</param>
        /// <returns>The item at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- beyond the bounds of the list.</exception>
        T this[int index]
        {
            get;
        }
    }
}
