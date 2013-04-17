using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NDex.Properties;

namespace NDex
{
    /// <summary>
    /// Presents a string as a read-only list. This class is for internal implementation purposes
    /// and cannot be instantiated.
    /// </summary>
    public sealed class StringAdapter : IList<char>
    {
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of a StringAdapter for the given string.
        /// </summary>
        /// <param name="value">The string value to wrap.</param>
        internal StringAdapter(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the underlying string value.
        /// </summary>
        public string Value
        {
            get { return value; }
        }

        /// <summary>
        /// Finds the index of first occurrence of the given character.
        /// </summary>
        /// <param name="item">The character to find.</param>
        /// <returns>The index of the first occurrence of the given character -or- -1 if it cannot be found.</returns>
        public int IndexOf(char item)
        {
            return value.IndexOf(item);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IList<char>.Insert(int index, char item)
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IList<char>.RemoveAt(int index)
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        /// <summary>
        /// Gets the character at the given index.
        /// </summary>
        /// <param name="index">The index of the character to get.</param>
        /// <returns>The character at the given index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index is outside the bounds of the string.</exception>
        public char this[int index]
        {
            get
            {
                return value[index];
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                throw new NotSupportedException(Resources.EditReadonlyList);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void ICollection<char>.Add(char item)
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void ICollection<char>.Clear()
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        /// <summary>
        /// Determines whether the string contains the given character.
        /// </summary>
        /// <param name="item">The character to check for.</param>
        /// <returns>True if the character exists within the string; otherwise, false.</returns>
        public bool Contains(char item)
        {
            return value.IndexOf(item) != -1;
        }

        /// <summary>
        /// Copies the characters in the string to the given array, starting at the given index.
        /// </summary>
        /// <param name="array">The array to copy the characters to.</param>
        /// <param name="arrayIndex">The index into the array to start copying the characters.</param>
        public void CopyTo(char[] array, int arrayIndex)
        {
            value.CopyTo(0, array, arrayIndex, value.Length);
        }

        /// <summary>
        /// Gets the length of the string.
        /// </summary>
        public int Count
        {
            get { return value.Length; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool ICollection<char>.IsReadOnly
        {
            get { return true; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool ICollection<char>.Remove(char item)
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        /// <summary>
        /// Gets an enumerator over the characters in the string.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<char> GetEnumerator()
        {
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
