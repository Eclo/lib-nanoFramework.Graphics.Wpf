//
// Copyright (c) 2019 The nanoFramework project contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Presentation;
using System;
using System.Collections;
using System.Diagnostics;

namespace nanoFramework.UI
{
    #region WindowCollection class

    /// <summary>
    /// WindowCollection can be used to interate over all the windows that have been
    /// opened in the current application.
    /// </summary>
    //CONSIDER: Should this be a sealed class?
    public sealed class WindowCollection : ICollection
    {
        //------------------------------------------------------
        //
        //   Public Methods
        //
        //------------------------------------------------------
        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowCollection()
        {
            _list = new ArrayList();
            _list.Capacity = 1;
        }

        internal WindowCollection(int count)
        {
            //Debug.Assert(count >= 0, "count must not be less than zero");
            _list = new ArrayList();
            _list.Capacity = 1;
        }

        #endregion Public Methods

        //------------------------------------------------------
        //
        //   Operator overload
        //
        //------------------------------------------------------
        #region Operator overload
        /// <summary>
        /// Overloaded [] operator to access the WindowCollection list
        /// </summary>
        public Window this[int index]
        {
            get
            {
                return _list[index] as Window;
            }
        }

        #endregion Operator overload

        //------------------------------------------------------
        //
        //   IEnumerable implementation
        //
        //------------------------------------------------------
        #region IEnumerable implementation
        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion IEnumerable implementation

        //--------------------------------------------------------
        //
        //   ICollection implementation (derives from IEnumerable)
        //
        //--------------------------------------------------------
        #region ICollection implementation
        /// <summary>
        /// CopyTo
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        void ICollection.CopyTo(Array array, int index)
        {
            _list.CopyTo(array, index);
        }

        /// <summary>
        /// CopyTo
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Window[] array, int index)
        {
            _list.CopyTo(array, index);
        }

        /// <summary>
        /// Count property
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// IsSynchronized
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return _list.IsSynchronized;
            }
        }

        /// <summary>
        /// SyncRoot
        /// </summary>
        public Object SyncRoot
        {
            get
            {
                return _list.SyncRoot;
            }
        }

        #endregion ICollection implementation

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------
        #region Internal Methods
        internal WindowCollection Clone()
        {
            WindowCollection clone;
            lock (_list.SyncRoot)
            {
                clone = new WindowCollection(_list.Count);
                for (int i = 0; i < _list.Count; i++)
                {
                    clone._list.Add(_list[i]);
                }
            }

            return clone;
        }

        internal void Remove(Window win)
        {
            _list.Remove(win);
        }

        internal int Add(Window win)
        {
            return _list.Add(win);
        }

        internal bool HasItem(Window win)
        {
            lock (_list.SyncRoot)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    if (_list[i] == win)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion Internal Methods

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------
        #region Private Fields
        private ArrayList _list;
        #endregion Private Fields
    }

    #endregion WindowCollection class
}


