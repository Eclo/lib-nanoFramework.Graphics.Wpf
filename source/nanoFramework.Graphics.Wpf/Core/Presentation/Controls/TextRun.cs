//
// Copyright (c) 2019 The nanoFramework project contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Presentation.Media;
using System;
using System.Diagnostics;
using nanoFramework.UI;

namespace nanoFramework.Presentation.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class TextRun
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly string Text;
        /// <summary>
        /// 
        /// </summary>
        public readonly Font Font;
        /// <summary>
        /// 
        /// </summary>
        public readonly Color ForeColor;

        internal bool IsEndOfLine;

        /// <summary>
        /// 
        /// </summary>
        protected int _width;
        /// <summary>
        /// 
        /// </summary>
        protected int _height;

        private TextRun()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="foreColor"></param>
        public TextRun(string text, Font font, Color foreColor)
        {
            if (text == null || text.Length == 0)
            {
                throw new ArgumentNullException("Text must be non-null and non-empty");
            }

            if (font == null)
            {
                throw new ArgumentNullException("font must be non-null");
            }

            this.Text = text;
            this.Font = font;
            this.ForeColor = foreColor;
        }

        /// <summary>
        /// 
        /// </summary>
        public static TextRun EndOfLine
        {
            get
            {
                TextRun eol = new TextRun();
                eol.IsEndOfLine = true;
                return eol;
            }
        }

        private int EmergencyBreak(int width)
        {
            int index = Text.Length;
            int w, h;
            do
            {
                Font.ComputeExtent(Text.Substring(0, --index), out w, out h);
            }

            while (w >= width && index > 1);

            return index;
        }

        internal bool Break(int availableWidth, out TextRun run1, out TextRun run2, bool emergencyBreak)
        {
            //Debug.Assert(availableWidth > 0);
            //Debug.Assert(availableWidth < _width);
            //Debug.Assert(Text.Length > 1);

            int leftBreak = -1;
            int rightBreak = -1;
            int w, h;

            // Try to find a candidate position for breaking
            //
            bool foundBreak = false;
            while (!foundBreak)
            {
                // Try adding a word
                //
                int indexOfNextSpace = Text.IndexOf(' ', leftBreak + 1);

                foundBreak = (indexOfNextSpace == -1);

                if (!foundBreak)
                {
                    Font.ComputeExtent(Text.Substring(0, indexOfNextSpace), out w, out h);
                    foundBreak = (w >= availableWidth);
                    if (w == availableWidth)
                    {
                        leftBreak = indexOfNextSpace;
                    }
                }

                if (foundBreak)
                {
                    if (leftBreak >= 0)
                    {
                        rightBreak = leftBreak + 1;
                    }
                    else if (emergencyBreak)
                    {
                        leftBreak = EmergencyBreak(availableWidth);
                        rightBreak = leftBreak;
                    }
                    else
                    {
                        run1 = run2 = null;
                        return false;
                    }
                }
                else
                {
                    leftBreak = indexOfNextSpace;
                }
            }

            string first = Text.Substring(0, leftBreak).TrimEnd(' ');

            // Split the text run
            //
            run1 = null;
            if (first.Length > 0)
            {
                run1 = new TextRun(first, this.Font, this.ForeColor);
            }

            run2 = null;
            if (rightBreak < Text.Length)
            {
                String run2String = Text.Substring(rightBreak).TrimStart(' ');

                // if run2 is all spaces (length == 0 after trim), we'll leave run2 as null
                if (run2String.Length > 0)
                {
                    run2 = new TextRun(run2String, this.Font, this.ForeColor);
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void GetSize(out int width, out int height)
        {
            if (_width == 0)
            {
                Font.ComputeExtent(Text, out _width, out _height);
            }

            width = _width;
            height = _height;
        }
    }
}


