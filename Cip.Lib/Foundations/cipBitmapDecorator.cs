/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) [karevn] from GotDotNet.ru,                                   //
// improvements by Andrew [kyzmitch] Ermoshin.                                 //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace Cip.Foundations
{
    /// <remarks>
    /// Fast access to Bitmap.
    /// By [karevn] from GotDotNet.ru, [kyzmitch] improvements
    /// </remarks>
    public class BitmapDecorator: IDisposable
    {
        private readonly Bitmap _bitmap;
        private readonly bool _isAlpha;
        private readonly int _width;
        private readonly int _height;
        private BitmapData _bmpData;
        private IntPtr _bmpPtr;
        private byte[] _rgbValues;
        private int index;
        private byte b;
        private byte r;
        private byte g;
        private byte a;

        private bool _disposed;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bitmap">Source bitmap.</param>
        public BitmapDecorator(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException();
            _bitmap = bitmap;
            if (_bitmap.PixelFormat == (PixelFormat.Indexed | _bitmap.PixelFormat))
            {
                throw new ArgumentException("Can't work with indexed pixel format");
            }
            _isAlpha = (Bitmap.PixelFormat == (Bitmap.PixelFormat | PixelFormat.Alpha));
            _width = bitmap.Width;
            _height = bitmap.Height;
            Lock();
        }

        #region properties
        /// <summary>
        /// Get source bitmap.
        /// </summary>
        public Bitmap Bitmap
        {
            get { return _bitmap; }
        }
        /// <summary>
        /// Get dispose state, if true, then bitmap unlocked.
        /// </summary>
        public bool IsDisposed
        {
            get { return _disposed; }
        }
        #endregion

        #region methods
        /// <summary>
        /// Locks bitmap and copying pixels to byte array.
        /// </summary>
        private void Lock()
        {
            Rectangle rect = new Rectangle(0, 0, _width, _height);
            this._disposed = false;
            _bmpData = Bitmap.LockBits(rect, ImageLockMode.ReadWrite, Bitmap.PixelFormat);
            _bmpPtr = _bmpData.Scan0;
            int bytes = _width * _height * (_isAlpha ? 4 : 3);
            //int bytes = _width * _height * 3;
            _rgbValues = new byte[bytes];
            Marshal.Copy(_bmpPtr, _rgbValues, 0, _rgbValues.Length);
        }
        /// <summary>
        /// Unlocks bitmap and copying pixels into him.
        /// </summary>
        private void UnLock()
        {
            // Copy the RGB values back to the bitmap
            Marshal.Copy(_rgbValues, 0, _bmpPtr, _rgbValues.Length);
            // Unlock the bits.
            Bitmap.UnlockBits(_bmpData);
            this._disposed = true;
        }
        /// <summary>
        /// Set R, G, B values to pixel.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="r">red.</param>
        /// <param name="g">green.</param>
        /// <param name="b">blue.</param>
        public void SetPixel(int x, int y, int r, int g, int b)
        {
            if (_isAlpha)
            {
                index = ((y * _width + x) * 4);
                _rgbValues[index] = (byte)b;
                _rgbValues[index + 1] = (byte)g;
                _rgbValues[index + 2] = (byte)r;
                _rgbValues[index + 3] = 255;
            }
            else
            {
                index = ((y * _width + x) * 3);
                _rgbValues[index] = (byte)b;
                _rgbValues[index + 1] = (byte)g;
                _rgbValues[index + 2] = (byte)r;
            }
        }
        /// <summary>
        /// Set color struct to pixel.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="color">Color struct.</param>
        public void SetPixel(int x, int y, Color color)
        {
            if (_isAlpha)
            {
                index = ((y * _width + x) * 4);
                _rgbValues[index] = color.B;
                _rgbValues[index + 1] = color.G;
                _rgbValues[index + 2] = color.R;
                _rgbValues[index + 3] = color.A;
            }
            else
            {
                index = ((y * _width + x) * 3);
                _rgbValues[index] = color.B;
                _rgbValues[index + 1] = color.G;
                _rgbValues[index + 2] = color.R;
            }
        }
        public void SetPixel(int x, int y, Cip.Foundations.VectorRgb rgb)
        {
            if (_isAlpha)
            {
                index = ((y * _width + x) * 4);
                _rgbValues[index] = rgb.B;
                _rgbValues[index + 1] = rgb.G;
                _rgbValues[index + 2] = rgb.R;
                _rgbValues[index + 3] = 255;
            }
            else
            {
                index = ((y * _width + x) * 3);
                _rgbValues[index] = rgb.B;
                _rgbValues[index + 1] = rgb.G;
                _rgbValues[index + 2] = rgb.R;
            }
        }
        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            if (_isAlpha)
            {
                index = ((y * _width + x) * 4);
                _rgbValues[index] = b;
                _rgbValues[index + 1] = g;
                _rgbValues[index + 2] = r;
                _rgbValues[index + 3] = 255;
            }
            else
            {
                index = ((y * _width + x) * 3);
                _rgbValues[index] = b;
                _rgbValues[index + 1] = g;
                _rgbValues[index + 2] = r;
            }
        }
        /// <summary>
        /// Set color struct to point.
        /// </summary>
        /// <param name="point">Point struct.</param>
        /// <param name="color">Color struct.</param>
        public void SetPixel(Point point, Color color)
        {
            if (_isAlpha)
            {
                index = ((point.Y * _width + point.X) * 4);
                _rgbValues[index] = color.B;
                _rgbValues[index + 1] = color.G;
                _rgbValues[index + 2] = color.R;
                _rgbValues[index + 3] = color.A;
            }
            else
            {
                index = ((point.Y * _width + point.X) * 3);
                _rgbValues[index] = color.B;
                _rgbValues[index + 1] = color.G;
                _rgbValues[index + 2] = color.R;
            }
        }
        /// <summary>
        /// Gets color from specified coordinates.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <returns>Color struct.</returns>
        public Color GetPixel(int x, int y)
        {
            if (x > _width - 1 || y > _height - 1)
                throw new ArgumentException();
            if (_isAlpha)
            {
                index = ((y * _width + x) * 4);
                b = _rgbValues[index];
                g = _rgbValues[index + 1];
                r = _rgbValues[index + 2];
                a = _rgbValues[index + 3];
                return Color.FromArgb(a, r, g, b);
            }
            else
            {
                index = ((y * _width + x) * 3);
                b = _rgbValues[index];
                g = _rgbValues[index + 1];
                r = _rgbValues[index + 2];
                return Color.FromArgb(r, g, b);
            }
        }
        public Color GetPixel(Point point)
        {
            return GetPixel(point.X, point.Y);
        }
        public VectorRgb GetVectorRgb(int x, int y)
        {
            if (x > _width - 1 || y > _height - 1)
                throw new ArgumentException();
            if (_isAlpha)
            {
                index = ((y * _width + x) * 4);
                b = _rgbValues[index];
                g = _rgbValues[index + 1];
                r = _rgbValues[index + 2];
                return new VectorRgb(r, g, b);
            }
            else
            {
                index = ((y * _width + x) * 3);
                b = _rgbValues[index];
                g = _rgbValues[index + 1];
                r = _rgbValues[index + 2];
                return new VectorRgb(r, g, b);
            }
        }
        

        

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            UnLock();
        }

        #endregion
    }
}
