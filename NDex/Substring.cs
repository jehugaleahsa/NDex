using System;
using System.Collections;
using System.Collections.Generic;

namespace NDex
{
    /// <summary>
    /// Provides methods for creating instances of Substring.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Wraps the given string such that the entirety of the string is visible.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        /// <returns>A Substring wrapping the given string.</returns>
        public static Substring ToSubstring(this string value)
        {
            return new Substring(value);
        }

        /// <summary>
        /// Wraps the given string such that the first character starts at the given offset and contains the remaining characters.
        /// </summary>
        /// <param name="value">The string to wrap.</param>
        /// <param name="offset">The starting index into the string to create the view.</param>
        /// <returns>A Substring wrapping the given string.</returns>
        public static Substring ToSubstring(this string value, int offset)
        {
            return new Substring(value, offset);
        }

        /// <summary>
        /// Wraps the given string such that the first character starts at the given offset and contains the number of characters
        /// specified by the count.
        /// </summary>
        /// <param name="value">The string to wrap.</param>
        /// <param name="offset">The starting index into the string to create the view.</param>
        /// <param name="count">The number of characters to include in the view.</param>
        /// <returns>A Substring wrapping the given string.</returns>
        public static Substring ToSubstring(this string value, int offset, int count)
        {
            return new Substring(value, offset, count);
        }

        /// <summary>
        /// Restores a Substring that's been converted to an IReadOnlySublist back to a Substring.
        /// </summary>
        /// <param name="sublist">The sublist representation of the substring.</param>
        /// <returns>A Substring representing the original substring.</returns>
        public static Substring ToSubstring(this IReadOnlySublist<StringAdapter, char> sublist)
        {
            if (sublist == null)
            {
                throw new ArgumentNullException("sublist");
            }
            return new Substring(sublist.List.Value, sublist.Offset, sublist.Count);
        }
    }

    /// <summary>
    /// Creates a view into a string starting at an offset and containing the designated number of characters.
    /// </summary>
    public sealed class Substring : IReadOnlySublist<StringAdapter, char>
    {
        private readonly Sublist<StringAdapter, char> _list;

        /// <summary>
        /// Initializes a new instance of a Substring representing a splice containing the entire string.
        /// </summary>
        /// <param name="value">The string to wrap</param>
        /// <exception cref="System.ArgumentNullException">The string is null.</exception>
        public Substring(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this._list = new Sublist<StringAdapter,char>(new StringAdapter(value), 0, value.Length);
        }

        /// <summary>
        /// Initializes a new instance of a Substring representing a splice starting at the given offset and containing
        /// the remaining items.
        /// </summary>
        /// <param name="value">The string to wrap.</param>
        /// <param name="offset">The index into the string to treat as the start of the splice.</param>
        /// <exception cref="System.ArgumentNullException">The string is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the string.</exception>
        public Substring(string value, int offset)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            _list = new Sublist<StringAdapter,char>(new StringAdapter(value), offset);
        }

        /// <summary>
        /// Initializes a new instance of a Substring representing a splice starting at the given offset and containing
        /// count characters.
        /// </summary>
        /// <param name="value">The string to wrap.</param>
        /// <param name="offset">The index into the list to treat as the start of the splice.</param>
        /// <param name="count">The number of items to include in the splice.</param>
        /// <exception cref="System.ArgumentNullException">The string is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bound of the string.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The count is negative.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The count is greater than the remaining items in the string.</exception>
        public Substring(string value, int offset, int count)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            _list = new Sublist<StringAdapter,char>(new StringAdapter(value), offset, count);
        }

        private Substring(Sublist<StringAdapter, char> sublist)
        {
            this._list = sublist;
        }

        /// <summary>
        /// Creates a new Substring that acts as a splice into the Substring, starting at the given offset.
        /// </summary>
        /// <param name="offset">The offset into the Substring to start the new splice.</param>
        /// <returns>A new Substring starting at the given offset into the Sublist, consisting of the remaining items.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the Substring.</exception>
        public Substring Nest(int offset)
        {
            return new Substring(_list.Nest(offset));
        }

        IReadOnlySublist<StringAdapter, char> IReadOnlySublist<StringAdapter, char>.Nest(int offset)
        {
            return Nest(offset);
        }

        /// <summary>
        /// Creates a new Substring that acts as a splice into the Substring, starting at the given offset, spanning
        /// the number of characters specified by the count.
        /// </summary>
        /// <param name="offset">The offset into the Substring to start the new splice.</param>
        /// <param name="count">The number of characters to include in the splice.</param>
        /// <returns>
        /// A new Substring starting at the given offset into the Substring, spanning the number of characters specified by the count.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the Substring.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The count is negative -or-  beyond the bounds of the StringString.</exception>
        public Substring Nest(int offset, int count)
        {
            return new Substring(_list.Nest(offset, count));
        }

        IReadOnlySublist<StringAdapter, char> IReadOnlySublist<StringAdapter, char>.Nest(int offset, int count)
        {
            return Nest(offset, count);
        }

        /// <summary>
        /// Attempts to shift the sublist to the right by the given shift.
        /// If the shift is negative, the sublist is shifted to the left.
        /// The sublist will be automatically resized if it is too big.
        /// </summary>
        /// <param name="shift">The amount to shift the sublist to the right.</param>
        /// <param name="isChecked">If checked, an exception will be thrown if the sublist would extend beyond the list.</param>
        /// <returns>True if the sublist remained the size; otherwise, false if the sublist shrank.</returns>
        public Substring Shift(int shift, bool isChecked)
        {
            return new Substring(_list.Shift(shift, isChecked));
        }

        IReadOnlySublist<StringAdapter, char> IReadOnlySublist<StringAdapter, char>.Shift(int shift, bool isChecked)
        {
            return Shift(shift, isChecked);
        }

        /// <summary>
        /// Attempts to resize the sublist so that its count equals the given limit.
        /// If the limit is too large, the count gets as large as it can.
        /// </summary>
        /// <param name="size">The desired length of the sublist.</param>
        /// <param name="isChecked">If checked, an exception will be thrown if the sublist would be too large.</param>
        /// <returns>True if the sublist fit in the list; otherwise, false.</returns>
        public Substring Resize(int size, bool isChecked)
        {
            return new Substring(_list.Resize(size, isChecked));
        }

        IReadOnlySublist<StringAdapter, char> IReadOnlySublist<StringAdapter, char>.Resize(int size, bool isChecked)
        {
            return Resize(size, isChecked);
        }

        StringAdapter IReadOnlySublist<StringAdapter, char>.List
        {
            get { return _list.List; }
        }

        /// <summary>
        /// Gets the underlying string.
        /// </summary>
        public string Value
        {
            get { return _list.List.Value; }
        }

        /// <summary>
        /// Gets or sets the offset into the underlying string.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The value is negative -or- outside the bounds of the underlying string.
        /// </exception>
        public int Offset
        {
            get { return _list.Offset; }
        }

        /// <summary>
        /// Gets or sets the number of items to include in the Sublist.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The value is negative -or- outside the bounds of the underlying string.
        /// </exception>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// Gets the character at the given index.
        /// </summary>
        /// <param name="index">The index into the Substring to get the item.</param>
        /// <returns>The item at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- beyond the bounds of the string.</exception>
        public char this[int index]
        {
            get { return _list[index]; }
        }

        /// <summary>
        /// Gets an enumerator that iterates through the string.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<char> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the string representation of the Substring.
        /// </summary>
        /// <returns>The substring.</returns>
        public override string ToString()
        {
            return _list.List.Value.Substring(_list.Offset, _list.Count);
        }

        /// <summary>
        /// Implicitly creates a new instance of a Substring spanning the entirety of a string.
        /// </summary>
        /// <param name="value">The string to wrap with a Substring.</param>
        /// <returns>A new instance of a Substring.</returns>
        public static implicit operator Substring(string value)
        {
            return new Substring(value);
        }
    }
}
