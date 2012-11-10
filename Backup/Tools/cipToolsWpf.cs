using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace Cip.WpfDemo.Tools
{
    public static class CipToolsWpf
    {
        public static BitmapFrame CipOpenBitmapFrame()
        {
            BitmapFrame bmpFrame;
            // create dialog to open image
            using (System.Windows.Forms.OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.CheckFileExists = true;
                openDlg.CheckPathExists = true;
                openDlg.AddExtension = true;
                openDlg.Multiselect = false;

                openDlg.Filter = "Windows Bitmap (*.bmp)|*.bmp|" +
                                 "Graphics Interchange Format (*.gif)|*.gif|" +
                                 "JPEG File Interchange Format (*.jpg,*.jpeg)|" +
                                 "*.jpg;*.jpeg;*.jfif|" +
                                 "Portable Network Graphics (*.png)|*.png|" +
                                 "Tagged Imaged File Format (*.tif)|*.tif;*.tiff|" +
                                 "All Supported Files|" +
                                 "*.bmp;*.gif;*.jpg;*.jpeg;*.jfif;*.png;*.tif;*.tiff";
                openDlg.FilterIndex = 6;


                // if User select file
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        bmpFrame = BitmapFrame.Create(new Uri(openDlg.FileName),
                                                      BitmapCreateOptions.PreservePixelFormat,
                                                      BitmapCacheOption.Default);
                        return bmpFrame;
                    }
                    catch
                    {
                        MessageBox.Show("Cannot finde file " + openDlg.FileName,
                                        "Open picture from file ERROR",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    return null;
            }
            return null;
            
        }

        public static BitmapSource CipOpenBitmapSource()
        {
            // create dialog to open image
            using (System.Windows.Forms.OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.CheckFileExists = true;
                openDlg.CheckPathExists = true;
                openDlg.AddExtension = true;
                openDlg.Multiselect = false;

                openDlg.Filter = "Windows Bitmap (*.bmp)|*.bmp|" +
                                 "Graphics Interchange Format (*.gif)|*.gif|" +
                                 "JPEG File Interchange Format (*.jpg,*.jpeg)|" +
                                 "*.jpg;*.jpeg;*.jfif|" +
                                 "Portable Network Graphics (*.png)|*.png|" +
                                 "Tagged Imaged File Format (*.tif)|*.tif;*.tiff|" +
                                 "All Supported Files|" +
                                 "*.bmp;*.gif;*.jpg;*.jpeg;*.jfif;*.png;*.tif;*.tiff";
                openDlg.FilterIndex = 6;


                // if User select file
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        BitmapFrame bmpFrame = BitmapFrame.Create(new Uri(openDlg.FileName),
                                                      BitmapCreateOptions.PreservePixelFormat,
                                                      BitmapCacheOption.Default);
                        BitmapSource bmpSrc = bmpFrame.CloneCurrentValue();
                        return bmpSrc;
                    }
                    catch
                    {
                        MessageBox.Show("Cannot finde file " + openDlg.FileName,
                                        "Open picture from file ERROR",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    return null;
            }
            return null;

        }

    }
}
