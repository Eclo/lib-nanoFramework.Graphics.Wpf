//
// Copyright (c) 2019 The nanoFramework project contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.
//

using System.Runtime.CompilerServices;

namespace nanoFramework.Touch
{
    /// <summary>
    /// 
    /// </summary>
    public class TouchPanel
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                EnableInternal(value);
                _enabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cCalibrationPoints"></param>
        /// <param name="screenXBuffer"></param>
        /// <param name="screenYBuffer"></param>
        /// <param name="uncalXBuffer"></param>
        /// <param name="uncalYBuffer"></param>
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetCalibration(int cCalibrationPoints,
                short[] screenXBuffer,
                short[] screenYBuffer,
                short[] uncalXBuffer,
                short[] uncalYBuffer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void GetCalibrationPointCount(ref int count);

        /// <summary>
        /// 
        /// </summary>
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void StartCalibration();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void GetCalibrationPoint(int index, ref int x, ref int y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enable"></param>
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern void EnableInternal(bool enable);

        private bool _enabled = false;
    }
}


