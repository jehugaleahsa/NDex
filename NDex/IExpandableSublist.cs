using System;
using System.Collections.Generic;

namespace NDex
{
    /// <summary>
    /// Creates a view into a list starting at an offset and containing a designated number of items.
    /// </summary>
    /// <typeparam name="TList">The type of the list to wrap.</typeparam>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public interface IExpandableSublist<TList, T> : IMutableSublist<TList, T>
    {
        /// <summary>
        /// Creates a new Sublist that acts as a splice into the Sublist, starting at the given offset.
        /// </summary>
        /// <param name="offset">The offset into the Sublist to start the new splice.</param>
        /// <returns>A new Sublist starting at the given offset into the Sublist, consisting of the remaining items.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the Sublist.</exception>
        new IExpandableSublist<TList, T> Nest(int offset);

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
        new IExpandableSublist<TList, T> Nest(int offset, int count);

        /// <summary>
        /// Attempts to shift the sublist to the right by the given shift.
        /// If the shift is negative, the sublist is shifted to the left.
        /// The sublist will be automatically resized if it is too big.
        /// </summary>
        /// <param name="shift">The amount to shift the sublist to the right.</param>
        /// <param name="isChecked">If checked, an exception will be thrown if the sublist would extend beyond the list.</param>
        /// <returns>True if the sublist remained the size; otherwise, false if the sublist shrank.</returns>
        new IExpandableSublist<TList, T> Shift(int shift, bool isChecked);

        /// <summary>
        /// Attempts to resize the sublist so that its count equals the given limit.
        /// If the limit is too large, the count gets as large as it can.
        /// </summary>
        /// <param name="size">The desired length of the sublist.</param>
        /// <param name="isChecked">If checked, an exception will be thrown if the sublist would be too large.</param>
        /// <returns>True if the sublist fit in the list; otherwise, false.</returns>
        new IExpandableSublist<TList, T> Resize(int size, bool isChecked);

        /// <summary>
        /// Gets the number of items in the sublist.
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// Gets or sets the item at the given index.
        /// </summary>
        /// <param name="index">The index into the Sublist to get the item.</param>
        /// <returns>The item at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- beyond the bounds of the list.</exception>
        new T this[int index] { get; set; }
    }
}
