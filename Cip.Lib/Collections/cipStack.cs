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
    
    /// <summary>Stack.</summary>
    /// <remarks>FILO - first in, last out.</remarks>
    public class CipStack<T> : IEnumerable<T>
    {
        #region Private Fields

        /// <summary> Data.</summary>
        private T[] list;
        /// <summary> The number of elements contained in CipStack.</summary>
        private int count;
        /// <summary> The initial number of elements that the CipStack can contain.</summary>
        private int capacity;
        /// <summary> The default number of elements that the CipStack can contain.</summary>
        private readonly int defaultCapacity = 50;
        /// <summary> The top value in the CipStack.</summary>
        private int top;

        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the CipStack
        /// that is empty and has default capacity.
        /// </summary>
        public CipStack()
        {
            capacity = defaultCapacity;
            list = new T[capacity];
            count = 0;
            top = -1;
        }
        /// <summary>
        /// Initializes a new instance of the CipStack
        /// that is empty and has the specified initial capacity or the default initial
        /// capacity, whichever is greater.
        /// </summary>
        /// <param name="capacity">The initial number of elements.</param>
        public CipStack(int capacity)
        {
            if (capacity > this.defaultCapacity)
                this.capacity = capacity;
            else
                this.capacity = defaultCapacity;
            list = new T[this.capacity];
            count = 0;
            top = -1;
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
        /// Inserts an object(T) at the top of CipStack.
        /// </summary>
        /// <param name="value"></param>
        public void Push(T value)
        {
            if (!IsFull)
            {
                top++;
                list[top] = value;
                count++;
            }
        }
        /// <summary>
        /// Returns the object at the top of the CipStack
        /// without removing it.
        /// </summary>
        /// <returns>value</returns>
        public T Peek()
        {
            if (!IsEmpty)
                return list[top];
            else
                return default(T);
        }
        /// <summary>
        /// Removes and returns the object at the top of the CipStack.
        /// </summary>
        /// <returns>value</returns>
        public T Pop()
        {
            if (!IsEmpty)
            {
                T temp = list[top];
                top--;
                count--;
                return temp;
            }
            else
                return default(T);
        }
        /// <summary>
        /// Removes all objects from the CipStack.
        /// </summary>
        public void Clear()
        {
            top = -1;
            count = 0;
        }
        /// <summary>
        /// Copies the CipStack to a new array.
        /// </summary>
        /// <returns>A new array containing copies of the elements of the CipStack.</returns>
        public T[] ToArray()
        {
            if(!IsEmpty)
            {
                T[] array = new T[count];
                for (int i = 0; i < count; i++)
                    array[i] = list[top - i];
                return array;
            }
            else
                return null;
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
            int _top_copy = top;
            while (top > -1)
            {
                yield return list[top--];
            }
            top = _top_copy;
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
                for (int i = 0; i < count; i++)
                    System.Console.WriteLine(list[top - i]);
                System.Console.WriteLine("\n");
            }
        }
    }
}
