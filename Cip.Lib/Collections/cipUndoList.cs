/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable 1591

namespace Cip.Collections
{
    /// <summary>
    /// Undo list.
    /// </summary>
    /// <typeparam name="T">Type of list.</typeparam>
    public class CipUndoList<T>
    {
        /// <summary>
        /// Array of the list.
        /// </summary>
        private T[] _list;
        /// <summary>
        /// Default range of the array.
        /// </summary>
        private const int _range = 20;
        /// <summary>
        /// Range of the array.
        /// </summary>
        private int _rangeOfList;
        /// <summary>
        /// Current index.
        /// </summary>
        private int _currentIndexOfList;
        /// <summary>
        /// Current count of an elements.
        /// </summary>
        private int _currentCountOfList;

        /// <summary>
        /// Parametrized constructor.
        /// </summary>
        /// <param name="range">Range of the list.</param>
        public CipUndoList(int range)
        {
            _rangeOfList = range;
            _list = new T[_rangeOfList];
            _currentCountOfList = 0;
            _currentIndexOfList = -1;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public CipUndoList()
        {
            _rangeOfList = _range;
            _list = new T[_rangeOfList];
            _currentCountOfList = 0;
            _currentIndexOfList = -1;
        }

        /// <summary>
        /// Get current element.
        /// </summary>
        /// <returns>Reterns current element.</returns>
        public T GetCurrent()
        {
            if (!IsEmpty)
                return this._list[_currentIndexOfList];
            else
                return default(T);
        }
        /// <summary>
        /// Add's new element to list.
        /// </summary>
        /// <param name="value">New element.</param>
        public void Add(T value)
        {
            if ((_currentIndexOfList + 1) < _range)
            {
                _list[++_currentIndexOfList] = value;
                _currentCountOfList = _currentIndexOfList + 1;
            }
            else
            {
                _list[_range - 1] = value;
                _currentCountOfList = _range;
                _currentIndexOfList = _range - 1;
            }
        }
        /// <summary>
        /// Undo operation.
        /// </summary>
        /// <returns>Returns previous element.</returns>
        public T Undo()
        {
            if (_currentIndexOfList == 0)
                return default(T);
            else
                //get's previous element from list
                return _list[--_currentIndexOfList];
        }
        /// <summary>
        /// Redo operation.
        /// </summary>
        /// <returns>Returns next element.</returns>
        public T Redo()
        {
            if ((_currentIndexOfList + 1) == _currentCountOfList)
                return default(T);
            else
            {
                if ((_currentIndexOfList + 1) < _currentCountOfList)
                    return _list[++_currentIndexOfList];
                else
                    return default(T);
            }

        }
        /// <summary>
        /// Default state of the list.
        /// </summary>
        /// <returns>Returns first element.</returns>
        public T Default()
        {
            for (int i = 1; i < _currentCountOfList; i++)
                _list[i] = default(T);
            _currentIndexOfList = 0;
            _currentCountOfList = 1;
            return _list[0];
        }
        public bool ClearCollection()
        {
            if (!this.IsEmpty)
            {
                for (int i = 0; i < _currentCountOfList; i++)
                    _list[i] = default(T);
                _currentIndexOfList = -1;
                _currentCountOfList = 0;
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Index of current element.
        /// </summary>
        public int Index
        {
            get { return this._currentIndexOfList; }
            set { this._currentIndexOfList = value; }
        }
        /// <summary>
        /// Count of elements.
        /// </summary>
        public int Count
        {
            get { return this._currentCountOfList; }
            set { this._currentCountOfList = value; }
        }
        public bool IsEmpty
        {
            get { return _currentCountOfList == 0 ? true : false; }
        }
        public bool IsFull
        {
            get { return _currentCountOfList == _rangeOfList ? true : false; }
        }
    }
}
