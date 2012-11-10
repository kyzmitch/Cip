/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable 1591

namespace Cip.Collections
{
    /// <summary>
    /// Queue, FIFO - first in, first out.
    /// </summary>
    public class CipQueue<T>: IEnumerable<T>
    {
        #region Private Fields

        /// <summary> Data.</summary>
        private T[] list;
        /// <summary> The number of elements contained in CipQueue.</summary>
        private int count;
        /// <summary> The initial number of elements that the CipQueue can contain.</summary>
        private int capacity;
        /// <summary> The default number of elements that the CipQueue can contain.</summary>
        private readonly int defaultCapacity = 50;
        /// <summary> The top value in the CipQueue.</summary>
        private int top;
        /// <summary> The low value in the CipQueue.</summary>
        private int low;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CipQueue
        /// that is empty and has default capacity.
        /// </summary>
        public CipQueue()
        {
            capacity = defaultCapacity;
            list = new T[capacity];
            count = 0;
            top = -1;
            low = 0;
        }
        /// <summary>
        /// Initializes a new instance of the CipQueue
        /// that is empty and has the specified initial capacity or the default initial
        /// capacity, whichever is greater.
        /// </summary>
        /// <param name="capacity">The initial number of elements.</param>
        public CipQueue(int capacity)
        {
            if (capacity > this.defaultCapacity)
                this.capacity = capacity;
            else
                this.capacity = defaultCapacity;
            list = new T[this.capacity];
            count = 0;
            top = -1;
            low = 0;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of elements contained in CipStack.
        /// </summary>
        public int Count
        {
            get
            {
                return this.count;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return top == -1;
            }
        }
        private bool IsFull
        {
            get
            {
                return top == capacity - 1;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Removes all objects from the CipStack.
        /// </summary>
        public void Clear()
        {
            top = -1;
            low = 0;
            count = 0;
        }
        /// <summary>
        /// Returns the object at the beginning of the CipQueue 
        /// without removing it.
        /// </summary>
        public T Peek()
        {
            if (!IsEmpty)
                return list[low];
            else
                return default(T);
        }
        /// <summary>
        /// Removes and returns the object at the beginning of the CipQueue.
        /// </summary>
        public T Get()
        {
            if (!IsEmpty)
            {
                T value = list[low++];
                count--;
                return value;
            }
            else
                return default(T);
        }
        /// <summary>
        /// Adds an object to the end of the CipQueue.
        /// </summary>
        public void Put(T value)
        {
            if (!IsFull)
            {
                list[++top] = value;
                count++;
            }
        }

        #endregion Public Methods

        #region Iterators methods
        // Implement GetEnumerator to return IEnumerator<T> to enable
        // foreach iteration of our list. Note that in C# 2.0 
        // you are not required to implement Current and MoveNext.
        // The compiler will create a class that implements IEnumerator<T>.
        /// <summary>
        /// GetEnumerator method for iterator.
        /// </summary>
        /// <returns>Data from collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int _low_copy = low;
            while (low <= top)
            {
                yield return list[low++];
            }
            low = _low_copy;
        }

        // We must implement this method because 
        // IEnumerable<T> inherits IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion Iterators methods

        public void Print()
        {
            if (!IsEmpty)
            {
                for (int i = low; i <= top; i++)
                    System.Console.WriteLine(list[i]);
                System.Console.WriteLine("\n");
            }
        }
    }
}
