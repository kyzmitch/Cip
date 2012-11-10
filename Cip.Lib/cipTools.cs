/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.Devices;

using Cip.Foundations;

#pragma warning disable 1591

namespace Cip
{
    /// <summary>
    /// Enum for OS type id.
    /// </summary>
    public enum CipOsType
    { 
        /// <summary>
        /// Windows XP NT 5.1 Type
        /// </summary>
        Windows51 = 0,
        /// <summary>
        /// Windows Vista NT 6.0 Type
        /// </summary>
        Windows60 = 1,
        /// <summary>
        /// Windows 7 NT 6.1 Type
        /// </summary>
        Windows61 = 2
    }
    /// <summary>
    /// Unsafe struct for storing OS Information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Author("Andrew kyzmitch Ermoshin", Version = 1)]
    public struct OSVersionInfo
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint dwOSVersionInfoSize;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwMajorVersion;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwMinorVersion;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwBuildNumber;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public String szCSDVersion;
    }
    /// <summary>
    /// Wrapper.
    /// </summary>
    [Author("Andrew kyzmitch Ermoshin", Version = 1)]
    public static class CipWrapper
    {
        /// <summary>
        /// Gets Operation System version into OSVersionInfo struct.
        /// </summary>
        /// <param name="osvi">Struct for storing OS info.</param>
        /// <returns>true - if success, false - vice versa.</returns>
        [DllImport("kernel32", EntryPoint = "GetVersionEx")]
        public static extern bool GetVersionOS(ref OSVersionInfo osvi);
    }
    /// <summary>
    /// Class which provides OS information.
    /// </summary>
    [Author("Andrew kyzmitch Ermoshin", Version = 1)]
    public class CipOsVersion
    {
        #region Private fields

        /// <summary>
        /// Info field.
        /// </summary>
        private ComputerInfo _computerInfo;
        /// <summary>
        /// Version field.
        /// </summary>
        private OSVersionInfo _osVersion;
        /// <summary>
        /// Os type.
        /// </summary>
        private CipOsType _osType;

        #endregion Private fields

        /// <summary>
        /// Constructor.
        /// </summary>
        public CipOsVersion()
        {
            _computerInfo = new ComputerInfo();
            _osVersion = new OSVersionInfo();
            _osVersion.dwOSVersionInfoSize = (uint)Marshal.SizeOf(_osVersion);
            CipWrapper.GetVersionOS(ref _osVersion);
            if (_osVersion.dwMajorVersion == 5)
            {
                switch (_osVersion.dwMinorVersion)
                { 
                    case 1:
                        _osType = CipOsType.Windows51;
                        break;
                }   
            }
            else
                if(_osVersion.dwMajorVersion == 6)
                    switch (_osVersion.dwMinorVersion)
                    { 
                        case 0:
                            _osType = CipOsType.Windows60;
                            break;
                        case 1:
                            _osType = CipOsType.Windows61;
                            break;
                    }
        }
        /// <summary>
        /// Gets OS type id.
        /// </summary>
        /// <returns>id.</returns>
        public CipOsType GetOsTypeId()
        {
            return _osType;
        }
        /// <summary>
        /// Gets OS full name.
        /// </summary>
        /// <returns>string</returns>
        public string GetOsFullName()
        {
            return _computerInfo.OSFullName;
        }
        /// <summary>
        /// Gets OS version.
        /// </summary>
        /// <returns>string.</returns>
        public string GetOsVersion()
        {
            return _computerInfo.OSVersion;
        }
        /// <summary>
        /// Gets OS platform.
        /// </summary>
        /// <returns>string</returns>
        public string GetOsPlatform()
        {
            return _computerInfo.OSPlatform;
        }
        /// <summary>
        /// Gets build number.
        /// </summary>
        /// <returns>number.</returns>
        public uint GetOsBuildNumber()
        {
            return _osVersion.dwBuildNumber;
        }
        /// <summary>
        /// Gets major version number.
        /// </summary>
        /// <returns>number.</returns>
        public uint GetOsMajorVersion()
        {
            return _osVersion.dwMajorVersion;
        }
        /// <summary>
        /// Gets minor version number.
        /// </summary>
        /// <returns>number.</returns>
        public uint GetOsMinorVersion()
        {
            return _osVersion.dwMinorVersion;
        }
        /// <summary>
        /// Organize information string.
        /// </summary>
        /// <returns>information string.</returns>
        public string GetInfoString()
        {
            string info =
                "OS Full Name: " + _computerInfo.OSFullName + "\n" +
                "OS Version: " + _computerInfo.OSVersion + "\n" +
                "OS Platform: " + _computerInfo.OSPlatform;
            return info;
        }
    }
    /// <summary>
    /// Tools.
    /// </summary>
    [Author("Andrew kyzmitch Ermoshin", Version = 1)]
    public static class CipTools
    {
        /// <summary>
        /// Get Equalized Histogram of Intensity
        /// </summary>
        /// <param name="r">original raster</param>
        /// <returns>array with normalized intensity</returns>
        public static float[] GetHistogramNormalized(Raster raster)
        {
            int width  = raster.Width;
            int height = raster.Height;
            int iNumberOfPixels = width * height;
            int L = 256;
            int[] aHistogram;
            float[] aHistogramNormalized = new float[L];

            aHistogram = GetHistogram(raster);

            //calculates intensity
            for (int k = 0; k < L; k++)
                for (int j = 0; j <= k; j++)
                    aHistogramNormalized[k] += (float)aHistogram[j] / iNumberOfPixels;

            return aHistogramNormalized;
        }
        /// <summary>
        /// Histogram calculating.
        /// </summary>
        /// <param name="r">Raster object.</param>
        /// <returns>Histogram array.</returns>
        public static int[] GetHistogram(Raster raster)
        {
            int width = raster.Width;
            int height = raster.Height;
            int[] aHistogram = new int[256];
            int iIntensity;

            //calculates histogram
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    iIntensity = ColorspaceHelper.RGB2GRAYI(raster[i, j]);
                    aHistogram[iIntensity]++;
                }

            return aHistogram;
        }
        /// <summary>
        /// Paint sight on PictureBox.
        /// </summary>
        /// <param name="picBox">PictureBox for painting.</param>
        /// <param name="color">Color of sight.</param>
        /// <param name="lengthOfLine">Length of line.</param>
        public static void PaintTarget(PictureBox picBox, Color color, int lengthOfLine)
        {
            const int indent = 2;

            int a = indent;
            int b = a + lengthOfLine + 1;
            Rectangle rectangle = picBox.ClientRectangle;
            Bitmap image = (Bitmap)picBox.Image;
            Graphics graphics = Graphics.FromImage(image);
            {
                using (Pen pen = new Pen(color, 2))
                {
                    graphics.DrawLine(pen, a, a, a, b);
                    graphics.DrawLine(pen, a, a, b, a);
                    graphics.DrawLine(pen, rectangle.Width - a, a, rectangle.Width - a, b);
                    graphics.DrawLine(pen, rectangle.Width - a, a, rectangle.Width - b, a);
                    graphics.DrawLine(pen, a, rectangle.Height - a, b, rectangle.Height - a);
                    graphics.DrawLine(pen, a, rectangle.Height - a, a, rectangle.Height - b);
                    graphics.DrawLine(pen, rectangle.Width - a, rectangle.Height - a, rectangle.Width - b, rectangle.Height - a);
                    graphics.DrawLine(pen, rectangle.Width - a, rectangle.Height - a, rectangle.Width - a, rectangle.Height - b);
                }
            }
            picBox.Image = image;
        }
        /// <summary>
        /// Paint Histogram.
        /// </summary>
        /// <param name="r">Raster object.</param>
        /// <param name="picBox">Picture box</param>
        /// <param name="color">Color.</param>
        /// <param name="text">text that painted on picture box</param>
        public static void PaintHistogram(Raster r, PictureBox picBox, Color color, string text)
        {
            Font font = new Font("Arial", 8, FontStyle.Bold);
            Rectangle rectangleOld = picBox.ClientRectangle;
            int intensityLevels = 256;
            //width should be as number
            if (rectangleOld.Width == intensityLevels)
            {
                int width = r.Width;
                int height = r.Height;
                int[] histogramOld;

                //calculates histogram.
                histogramOld = GetHistogram(r);

                //calculates maximum number of pixels that have definite level of intensity.
                int Maximum = CipMath.Maximum(histogramOld);
                //Graphics Old image
                Bitmap image = new Bitmap(256, rectangleOld.Height);
                Graphics graphicsOld = Graphics.FromImage(image);
                {
                    SolidBrush brush = new SolidBrush(Color.White);
                    graphicsOld.FillRectangle(brush, rectangleOld);
                    Pen pen = new Pen(color, 1);
                    double norm;
                    int y2;
                    for (int i = 0; i < intensityLevels; i++)
                    {
                        norm = (double)histogramOld[i] / Maximum;
                        y2 = Convert.ToInt32(norm * rectangleOld.Height);
                        graphicsOld.DrawLine(pen, i, rectangleOld.Height, i, rectangleOld.Height - y2);
                    }
                    //painting text
                    using (LinearGradientBrush brushStr = new LinearGradientBrush(rectangleOld,
                                                                               Color.FromArgb(130, 255, 0, 0),
                                                                               Color.FromArgb(255, 0, 0, 255),
                                                                               LinearGradientMode.BackwardDiagonal))
                    {
                        graphicsOld.DrawString(text, font, brushStr, rectangleOld);
                    }

                }
                picBox.Image = image;
            }
            else
                MessageBox.Show("Width of PictureBox should be 256 or 258");
        }
        /// <summary>
        /// Paint Histogram.
        /// </summary>
        /// <param name="param">ArrayList: Raster, PictureBox, Color, string.</param>
        public static void PaintHistogram(object param)
        {
            ArrayList array = (ArrayList)param;
            Raster r = (Raster)array[0];
            PictureBox picBox = (PictureBox)array[1];
            Color color = (Color)array[2];
            string text = (string)array[3];
            
            Font font = new Font("Arial", 8, FontStyle.Bold);
            Rectangle rectangleOld = picBox.ClientRectangle;
            //width should be as number
            if (rectangleOld.Width == 256)
            {
                int width = r.Width;
                int height = r.Height;
                int Height = rectangleOld.Height;
                int[] histogramOld = new int[256];

                //calculates histogram
                histogramOld = GetHistogram(r);

                //calculates maximum number of pixels that have definite level of intensity.
                int Maximum = CipMath.Maximum(histogramOld);
                //Graphics Old image
                Bitmap image = new Bitmap(256, Height);
                Graphics graphicsOld = Graphics.FromImage(image);
                {
                    SolidBrush brush = new SolidBrush(Color.White);
                    graphicsOld.FillRectangle(brush, rectangleOld);
                    Pen pen = new Pen(color, 1);
                    double norm;
                    int y2;
                    for (int i = 0; i < 256; i++)
                    {
                        norm = (double)histogramOld[i] / Maximum;
                        y2 = Convert.ToInt32(norm * Height);
                        graphicsOld.DrawLine(pen, i, Height, i, Height - y2);
                    }
                    //painting text
                    using (LinearGradientBrush brushStr = new LinearGradientBrush(rectangleOld,
                                                                               Color.FromArgb(130, 255, 0, 0),
                                                                               Color.FromArgb(255, 0, 0, 255),
                                                                               LinearGradientMode.BackwardDiagonal))
                    {
                        graphicsOld.DrawString(text, font, brushStr, rectangleOld);
                    }

                }
                picBox.Image = image;
            }
            else
                MessageBox.Show("Width of PictureBox should be 256 or 258");
        }
        /// <summary>
        /// Paint Histogram (Line).
        /// </summary>
        /// <param name="param">ArrayList: Raster, PictureBox, Color, string.</param>
        public static void PaintHistogramLine(object param)
        {
            ArrayList array = (ArrayList)param;
            Raster r = (Raster)array[0];
            PictureBox picBox = (PictureBox)array[1];
            Color color = (Color)array[2];
            string text = (string)array[3];

            Font font = new Font("Arial", 8, FontStyle.Bold);
            Rectangle rectOld = picBox.ClientRectangle;
            //width should be as number
            if (rectOld.Width == 256)
            {
                int width = r.Width;
                int height = r.Height;
                int rectHeight = rectOld.Height;
                int[] histogramOld;

                //calculates histogram.
                histogramOld = GetHistogram(r);

                //calculates maximum number of pixels that have definite level of intensity.
                int Maximum = CipMath.Maximum(histogramOld);
                //Graphics Old image
                Bitmap image = (Bitmap)picBox.Image;
                Graphics graphicsOld = Graphics.FromImage(image);

                {
                    Pen pen = new Pen(color, 1);
                    Point[] points = new Point[256];
                    double norm;
                    int y2;
                    for (int i = 0; i < 256; i++)
                    {
                        norm = (double)histogramOld[i] / Maximum;
                        y2 = Convert.ToInt32(norm * rectHeight);
                        points[i] = new Point(i, rectHeight - y2);
                    }
                    //graphicsOld.DrawLines(pen, points);
                    graphicsOld.DrawBeziers(pen, points);
                   

                    //painting text
                    using (LinearGradientBrush brushStr = new LinearGradientBrush(rectOld,
                                                                               Color.FromArgb(130, 255, 0, 0),
                                                                               Color.FromArgb(255, 0, 0, 255),
                                                                               LinearGradientMode.BackwardDiagonal))
                    {
                        graphicsOld.DrawString(text, font, brushStr, rectOld);
                    }
                }
                picBox.Image = image;
            }
        }
        /// <summary>
        /// Check for odd.
        /// </summary>
        /// <param name="number">Checked integer number.</param>
        /// <returns>True - odd, false - not odd.</returns>
        public static bool IsOdd(int number)
        {
            double a = number / 2;
            double b = Math.Ceiling(a);
            int mult = Convert.ToInt32(b * 2);
            if (mult == number)
                return false;
            else
                return true;
        }
        
        /// <summary>
        /// Save image.
        /// </summary>
        /// <param name="image">Source image.</param>
        public static void SaveImage(Image image)
        {
            // create dialog for save image
            using (SaveFileDialog savedlg = new SaveFileDialog())
            {
                savedlg.AddExtension = true;
                savedlg.CheckPathExists = true;
                savedlg.Filter = "Windows Bitmap (*.bmp)|*.bmp|" +
                                 "Graphics Interchange Format (*.gif)|*.gif|" +
                                 "JPEG File Interchange Format (*.jpg,*.jpeg)|" +
                                 "*.jpg;*.jpeg;*.jfif|" +
                                 "Portable Network Graphics (*.png)|*.png|" +
                                 "Tagged Imaged File Format (*.tif)|*.tif;*.tiff";
                savedlg.FilterIndex = 3;

                if (savedlg.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format;

                    switch (savedlg.FilterIndex)
                    {
                        case 1:
                            {
                                format = ImageFormat.Bmp;
                                break;
                            }
                        case 2:
                            {
                                format = ImageFormat.Gif;
                                break;
                            }
                        case 3:
                            {
                                format = ImageFormat.Jpeg;
                                break;
                            }
                        case 4:
                            {
                                format = ImageFormat.Png;
                                break;
                            }
                        case 5:
                            {
                                format = ImageFormat.Tiff;
                                break;
                            }
                        default:
                            {
                                format = ImageFormat.Bmp;
                                break;
                            }
                    }
                    try
                    {
                        image.Save(savedlg.FileName, format);
                    }
                    catch
                    {
                        MessageBox.Show("Cannot save file to " + savedlg.FileName,
                                        "Save picture ERROR",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }
        /// <summary>
        /// Open image from file to System.Drawing.Bitmap. 
        /// </summary>
        /// <returns>Bitmap of opened image.</returns>
        public static Bitmap OpenImage()
        {
            // create dialog for open image
            using (System.Windows.Forms.OpenFileDialog opendlg = new OpenFileDialog())
            {
                opendlg.CheckFileExists = true;
                opendlg.CheckPathExists = true;
                opendlg.AddExtension = true;
                opendlg.Multiselect = false;

                opendlg.Filter = "Windows Bitmap (*.bmp)|*.bmp|" +
                                 "Graphics Interchange Format (*.gif)|*.gif|" +
                                 "JPEG File Interchange Format (*.jpg,*.jpeg)|" +
                                 "*.jpg;*.jpeg;*.jfif|" +
                                 "Portable Network Graphics (*.png)|*.png|" +
                                 "Tagged Imaged File Format (*.tif)|*.tif;*.tiff|" +
                                 "All Supported Files|" +
                                 "*.bmp;*.gif;*.jpg;*.jpeg;*.jfif;*.png;*.tif;*.tiff";
                opendlg.FilterIndex = 6;

                
                // if User select file
                if (opendlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap bitmap = (Bitmap)Bitmap.FromFile(opendlg.FileName, true);
                        Bitmap bitmapClone = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                          PixelFormat.Format24bppRgb);
                        bitmap.Dispose();
                        return bitmapClone;
                    }
                    catch
                    {
                        MessageBox.Show("Cannot finde file " + opendlg.FileName,
                                        "Open picture from file ERROR",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// Selecting color by Colour dialog
        /// </summary>
        /// <returns>selected color or Red color if not selected.</returns>
        public static Color SelectColour()
        {
            ColorDialog dialog = new ColorDialog();
            dialog.SolidColorOnly = true;
            if (dialog.ShowDialog() == DialogResult.OK)
                return dialog.Color;
            else
                return Color.Red;
        }
        /// <summary>
        /// show in TextBoxes HSI and RGB components
        /// </summary>
        /// <param name="color">color of pixel</param>
        /// <param name="boxes">6 TextBoxes</param>
        public static void ShowPixelComponents(Color color, params TextBox[] boxes)
        {
            VectorHsi hsi = new VectorHsi(color);
            int h = (int)(hsi.H * 255f);
            int s = (int)(hsi.S * 255f);
            int i = (int)(hsi.I * 255f);
            if (boxes.Length == 6)
            {
                boxes[0].Text = Convert.ToString(h);
                boxes[1].Text = Convert.ToString(s);
                boxes[2].Text = Convert.ToString(i);
                boxes[3].Text = Convert.ToString(color.R);
                boxes[4].Text = Convert.ToString(color.G);
                boxes[5].Text = Convert.ToString(color.B);
            }
        }
        public static void GCFullCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public static int Max(int[,] array)
        {
            int max = int.MinValue;

            for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); ++i)
            {
                for (int j = array.GetLowerBound(1); j <= array.GetUpperBound(1); ++j)
                {
                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                    }
                }
            }

            return max;
        }
        public static int Sum(int[][] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; ++i)
            {
                int[] row = array[i];

                for (int j = 0; j < row.Length; ++j)
                {
                    sum += row[j];
                }
            }

            return sum;
        }
    }
}
