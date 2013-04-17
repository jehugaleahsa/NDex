using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NDex.Properties;

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
    }
    /// <summary>
    /// Creates a view into a string starting at an offset and containing the designated number of characters.
    /// </summary>
    public sealed class Substring : IReadOnlySublist<StringAdapter, char>
    {
        private readonly StringAdapter _list;
        private int _offset;
        private int _count;

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
            this._list = new StringAdapter(value);
            this._offset = 0;
            this._count = value.Length;
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
            if (offset < 0 || offset > value.Length)
            {
                throw new ArgumentOutOfRangeException("offset", offset, Resources.IndexOutOfRange);
            }
            _list = new StringAdapter(value);
            _offset = offset;
            _count = value.Length - offset;
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
                throw new ArgumentNullException("list");
            }
            if (offset < 0 || offset > value.Length)
            {
                throw new ArgumentOutOfRangeException("offset", offset, Resources.IndexOutOfRange);
            }
            if (count < 0 || count > value.Length - offset)
            {
                throw new ArgumentOutOfRangeException("count", count, Resources.CountOutOfRange);
            }
            _list = new StringAdapter(value);
            _offset = offset;
            _count = count;
        }

        /// <summary>
        /// Creates a new Substring that acts as a splice into the Substring, starting at the given offset.
        /// </summary>
        /// <param name="offset">The offset into the Substring to start the new splice.</param>
        /// <returns>A new Substring starting at the given offset into the Sublist, consisting of the remaining items.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the Substring.</exception>
        public Substring Nest(int offset)
        {
            if (offset < 0 || offset > _count)
            {
                throw new ArgumentOutOfRangeException("offset", offset, Resources.IndexOutOfRange);
            }
            return new Substring(_list.Value, offset + _offset, _count - offset);
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
            if (offset < 0 || offset > _count)
            {
                throw new ArgumentOutOfRangeException("offset", offset, Resources.IndexOutOfRange);
            }
            if (count < 0 || offset + count > _count)
            {
                throw new ArgumentOutOfRangeException("count", count, Resources.CountOutOfRange);
            }
            return new Substring(_list.Value, offset + _offset, count);
        }

        IReadOnlySublist<StringAdapter, char> IReadOnlySublist<StringAdapter, char>.Nest(int offset, int count)
        {
            return Nest(offset, count);
        }

        StringAdapter IReadOnlySublist<StringAdapter, char>.List
        {
            get { return _list; }
        }

        /// <summary>
        /// Gets the underlying string.
        /// </summary>
        public string Value
        {
            get { return _list.Value; }
        }

        /// <summary>
        /// Gets or sets the offset into the underlying string.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The value is negative -or- outside the bounds of the underlying string.
        /// </exception>
        /// <remarks>The Substring's count is adjusted automatically to prevent the splice from going beyond the end of the string.</remarks>
        public int Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                if (value < 0 || value > _list.Count)
                {
                    throw new ArgumentOutOfRangeException("value", value, Resources.IndexOutOfRange);
                }
                _offset = value;
                if (_offset + _count > _list.Count)
                {
                    _count = _list.Count - _offset;
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of items to include in the Sublist.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The value is negative -or- outside the bounds of the underlying string.
        /// </exception>
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (value < 0 || _offset + value > _list.Count)
                {
                    throw new ArgumentOutOfRangeException("value", value, Resources.CountOutOfRange);
                }
                _count = value;
            }
        }

        /// <summary>
        /// Gets the character at the given index.
        /// </summary>
        /// <param name="index">The index into the Substring to get the item.</param>
        /// <returns>The item at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- beyond the bounds of the string.</exception>
        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
                }
                return _list[_offset + index];
            }
        }

        /// <summary>
        /// Gets an enumerator that iterates through the string.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<char> GetEnumerator()
        {
            for (int index = 0; index < _count; ++index)
            {
                yield return _list[index + _offset];
            }
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
            return _list.Value.Substring(_offset, _count);
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
