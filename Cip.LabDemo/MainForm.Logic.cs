using System;
using System.Collections;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Xml;

using Cip;
using Cip.Foundations;
using Cip.Filters;
using Cip.LabDemo.Resources;

namespace Cip.LabDemo
{
    partial class MainForm
    {
        #region Private fields

        private Rectangle sclEditorRectangle;
        private Size sclOriginalBoxSize;
        private Size sclModifyedBoxSize;
        private Point sclOriginalLocation;
        private Point sclModifyedLocation;
        private Point sclCutColorsLocation;
        private Point sclHistoLocation;
        private Point sclNoteEditorLocation;
        private Size sclStandartFormSize;
        private Size sclRichTextBoxSize;
        private Size sclTabControlDescriptionSize;
        private Rectangle screenRectangle;

        private ImageOutForm OutForm;
        // Image for processing.
        private Raster normalImageRaster;
        private Bitmap currentBitmap;
        private Raster[] UndoList;
        //Range of undo list.
        private const int range = 20;
        private int iCurrentIndexOfList;
        private int iCurrentCountOfList;
        // Image for split.
        private Raster rasterComponents;
        //private Bitmap[] UndoListBitmaps;
        private Color clSelectedColour;
        private bool IsHistoStopped;
        private bool IsNullRaster;
        private bool IsOpenEnds;
        //objects for image showing.
        //private Bitmap partialBitmap;
        //private Raster partialRaster;
        private bool IsPictureHolded;
        private Point pointStart;

        //Threads
        private Thread thOpen;

        #endregion Private fields

        #region Methods

        private void ClearComponentsBoxes()
        {
            Bitmap blank = Raster.CreateBitmap(10, 10, Color.White);
            this.pictureBoxH.Image = blank;
            this.pictureBoxS.Image = blank;
            this.pictureBoxI.Image = blank;
            this.pictureBoxR.Image = blank;
            this.pictureBoxG.Image = blank;
            this.pictureBoxB.Image = blank;
        }
        public void CalculateHistogram()
        {
            this.IsHistoStopped = false;
            this.timerCip.Start();
        }
        private void ApplyFilter(ImageFilter filter)
        {
            Raster raster = filter.ProcessRaster(UndoList[iCurrentIndexOfList], backgroundWorkerCip);
            if (raster != null)
            {
                this.IsNullRaster = false;
                if ((iCurrentIndexOfList + 1) < range)
                {
                    UndoList[++iCurrentIndexOfList] = raster;
                    iCurrentCountOfList = iCurrentIndexOfList + 1;
                    this.currentBitmap = raster.ToBitmap();
                    this.picBoxModifyed.Image = this.currentBitmap;
                    this.CheckUndoToolStrip();
                }
                else
                {
                    UndoList[range - 1] = raster;
                    iCurrentCountOfList = range;
                    iCurrentIndexOfList = range - 1;
                    this.currentBitmap = raster.ToBitmap();
                    this.picBoxModifyed.Image = this.currentBitmap;
                    this.CheckUndoToolStrip();
                }
            }
            else
            {
                //MessageBox.Show("this method returns nothing");
                this.IsNullRaster = true;
            }
        }
        public void SplitComponents(object arr)
        {
            SplitterFilter filter = (SplitterFilter)((ArrayList)arr)[0];
            Cip.Filters.SplitMode mode = (Cip.Filters.SplitMode)((ArrayList)arr)[1];
            Raster result = filter.ProcessWithProgress(this.rasterComponents, this.progressBarCip);
            //Raster result = filter.ProcessImage(this.rasterComponents, backgroundWorkerCip);
            switch (mode)
            {
                case SplitMode.Hue:
                    {
                        if (this.pictureBoxH.Image != null)
                            this.pictureBoxH.Image.Dispose();
                        result.ShowFilter(this.pictureBoxH);
                        break;
                    }
                case SplitMode.Saturation:
                    {
                        if (this.pictureBoxS.Image != null)
                            this.pictureBoxS.Image.Dispose();
                        result.ShowFilter(this.pictureBoxS);
                        break;
                    }
                case SplitMode.Intensity:
                    {
                        if (this.pictureBoxI.Image != null)
                            this.pictureBoxI.Image.Dispose();
                        result.ShowFilter(this.pictureBoxI);
                        break;
                    }
                case SplitMode.Red:
                    {
                        if (this.pictureBoxR.Image != null)
                            this.pictureBoxR.Image.Dispose();
                        result.ShowFilter(this.pictureBoxR);
                        break;
                    }
                case SplitMode.Green:
                    {
                        if (this.pictureBoxG.Image != null)
                            this.pictureBoxG.Image.Dispose();
                        result.ShowFilter(this.pictureBoxG);
                        break;
                    }
                case SplitMode.Blue:
                    {
                        if (this.pictureBoxB.Image != null)
                            this.pictureBoxB.Image.Dispose();
                        result.ShowFilter(this.pictureBoxB);
                        break;
                    }
            }
        }
        private void UndoFilter()
        {
            if (iCurrentIndexOfList == 0)
                return;
            //get's previous image from list
            this.picBoxModifyed.Image.Dispose();
            this.picBoxModifyed.Image = UndoList[--iCurrentIndexOfList].ToBitmap();
            this.CheckUndoToolStrip();
        }
        private void RedoFilter()
        {
            if ((iCurrentIndexOfList + 1) == iCurrentCountOfList)
                return;
            if ((iCurrentIndexOfList + 1) < iCurrentCountOfList)
            {
                this.picBoxModifyed.Image.Dispose();
                this.picBoxModifyed.Image = UndoList[++iCurrentIndexOfList].ToBitmap();
                this.CheckUndoToolStrip();
            }
        }
        private void DefaultFilter()
        {
            for (int i = 0; i < UndoList.Length; i++)
            {
                UndoList[i] = null;
            }
            iCurrentIndexOfList = 0;
            UndoList[0] = normalImageRaster;
            iCurrentCountOfList = 1;

            if (this.picBoxModifyed.Image != null)
                this.picBoxModifyed.Image.Dispose();
            normalImageRaster.ShowFilter(this.picBoxModifyed);
            this.CheckUndoToolStrip();
            Cip.CipTools.GCFullCollect();
        }
        public Raster GetCurrentRaster()
        {
            return this.UndoList[iCurrentIndexOfList];
        }
        private void OpenPicture(object param)
        {
            object sender = ((ArrayList)param)[0];
            EventArgs e = (EventArgs)((ArrayList)param)[1];
            Bitmap newBitmap = (Bitmap)((ArrayList)param)[2];

            if (newBitmap != null)
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 1://editor tab
                        {
                            normalImageRaster = new Raster(newBitmap);

                            if (this.currentBitmap != null)
                                this.currentBitmap.Dispose();
                            this.currentBitmap = newBitmap;
                            if (this.picBoxOriginal.Image != null)
                                this.picBoxOriginal.Image.Dispose();
                            if (this.picBoxModifyed.Image != null)
                                this.picBoxModifyed.Image.Dispose();
                            this.picBoxOriginal.Image = this.currentBitmap;
                            this.picBoxModifyed.Image = this.currentBitmap;

                            this.ShowImageAttributes(this.currentBitmap);
                            for (int i = 1; i < UndoList.Length; i++)
                                UndoList[i] = null;
                            iCurrentIndexOfList = 0;
                            UndoList[0] = normalImageRaster;
                            iCurrentCountOfList = 1;
                            this.CalculateHistogram();
                            this.CheckUndoToolStrip();
                            Cip.CipTools.GCFullCollect();
                            break;
                        }
                    case 2://split tab
                        {
                            rasterComponents = new Raster(newBitmap);
                            
                            rasterComponents.ShowFilter(this.pictureBoxOrig);
                            Cip.CipTools.GCFullCollect();
                            break;
                        }
                }

            }
        }
        private void OpenPicture(object sender, EventArgs e, Bitmap newBitmap)
        {
            if (newBitmap != null)
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0://editor tab
                        {
                            normalImageRaster = new Raster(newBitmap);

                            if (this.currentBitmap != null)
                                this.currentBitmap.Dispose();
                            this.currentBitmap = newBitmap;

                            if (this.picBoxOriginal.Image != null)
                                this.picBoxOriginal.Image.Dispose();
                            this.picBoxOriginal.Image = this.currentBitmap;
                            this.ShowImageAttributes(this.currentBitmap);
                            this.defaultStateToolStripMenuItem_Click(sender, e);
                            break;
                        }
                    case 1://split tab
                        {
                            rasterComponents = new Raster(newBitmap);
                            if (this.pictureBoxOrig.Image != null)
                                this.pictureBoxOrig.Image.Dispose();
                            rasterComponents.ShowFilter(this.pictureBoxOrig);
                            Cip.CipTools.GCFullCollect();
                            break;
                        }
                }

            }
        }
        private void OpenPictureFromBitmap(Bitmap picture)
        {
            this.normalImageRaster = new Raster(picture);

            if(this.currentBitmap!=null)
                this.currentBitmap.Dispose();
            this.currentBitmap = picture;
            this.DefaultFilter();
            if(this.picBoxOriginal.Image!=null)
                this.picBoxOriginal.Image.Dispose();
            this.picBoxOriginal.Image = this.currentBitmap;
            this.CalculateHistogram();
        }
        private void ShowImageAttributes(Bitmap bmp)
        {
            if (bmp != null)
            {
                this.statusLabelResolution.Text = bmp.Width.ToString() + "x" + bmp.Height.ToString();
                //PropertyItem[] propItems = bmp.PropertyItems;
                //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                //System.Text.UTF8Encoding enc = new UTF8Encoding();
                //string manufacturer = encoding.GetString(propItems[1].Value);
                //this.attributesLabelResolutionValue.Text = bmp.Width.ToString() + "x" + bmp.Height.ToString();
                //this.attributesLabelEquipmentValue.Text = manufacturer;
            }
        }
        private void SwitchOffEditControlls(bool isOff)
        {
            if (isOff)
            {
                this.toolsToolStripMenuItem.Enabled = false;
                this.editToolStripMenuItem.Enabled = false;
                this.openToolStripMenuItem.Enabled = false;
                this.saveAsToolStripMenuItem.Enabled = false;
                this.groupBoxCutColours.Enabled = false;
                this.examplesToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.toolsToolStripMenuItem.Enabled = true;
                this.editToolStripMenuItem.Enabled = true;
                this.openToolStripMenuItem.Enabled = true;
                this.saveAsToolStripMenuItem.Enabled = true;
                this.groupBoxCutColours.Enabled = true;
                this.examplesToolStripMenuItem.Enabled = true;
            }
        }
        private void CreateMenuForPictureBox()
        {
            MenuItem[] menuItems = new MenuItem[4];
            menuItems[0] = new MenuItem(LabDemoResources.GetString("Open_Image"));
            menuItems[1] = new MenuItem(LabDemoResources.GetString("Mode_Stretch"));
            menuItems[2] = new MenuItem(LabDemoResources.GetString("Mode_Normal"));
            menuItems[3] = new MenuItem(LabDemoResources.GetString("Mode_Zoom"));



            menuItems[0].Click += new EventHandler(menu_open_Click);
            menuItems[1].Click += new EventHandler(menu_Stretch_Click);
            menuItems[2].Click += new EventHandler(menu_Normal_Click);
            menuItems[3].Click += new EventHandler(menu_Zoom_Click);
            this.picBoxOriginal.ContextMenu = new ContextMenu(menuItems);
            switch (this.picBoxOriginal.SizeMode)
            {
                case PictureBoxSizeMode.StretchImage:
                    {
                        this.picBoxOriginal.ContextMenu.MenuItems[1].Checked = true;
                        this.picBoxOriginal.ContextMenu.MenuItems[2].Checked = false;
                        this.picBoxOriginal.ContextMenu.MenuItems[3].Checked = false;
                        break;
                    }
                case PictureBoxSizeMode.Normal:
                    {
                        this.picBoxOriginal.ContextMenu.MenuItems[1].Checked = false;
                        this.picBoxOriginal.ContextMenu.MenuItems[2].Checked = true;
                        this.picBoxOriginal.ContextMenu.MenuItems[3].Checked = false;
                        break;
                    }
                case PictureBoxSizeMode.Zoom:
                    {
                        this.picBoxOriginal.ContextMenu.MenuItems[1].Checked = false;
                        this.picBoxOriginal.ContextMenu.MenuItems[2].Checked = false;
                        this.picBoxOriginal.ContextMenu.MenuItems[3].Checked = true;
                        break;
                    }
            }
            picBoxModifyed.ContextMenu = new ContextMenu();
            MenuItem item_save = new MenuItem(LabDemoResources.GetString("Save_Image"));
            item_save.Click += new EventHandler(menu_save_Click);
            picBoxModifyed.ContextMenu.MenuItems.Add(item_save);

        }
        private void CheckUndoToolStrip()
        {
            if (iCurrentCountOfList >= 2)
                defaultStateToolStripMenuItem.Enabled = true;
            else
                defaultStateToolStripMenuItem.Enabled = false;
            if (iCurrentIndexOfList == 0)
                undoToolStripMenuItem.Enabled = false;
            else
                undoToolStripMenuItem.Enabled = true;
            if ((iCurrentIndexOfList + 1) < iCurrentCountOfList)
                redoToolStripMenuItem.Enabled = true;
            else
                redoToolStripMenuItem.Enabled = false;
        }
        private void SetCulture(string _culture)
        {
            string path = Application.StartupPath;
            string culture = _culture;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(path + "\\Cip.Config.xml");
            }
            catch (Exception expn)
            {
                MessageBox.Show(LabDemoResources.GetString("Message_LoadFilePart1") + " Cip.Config.xml " + LabDemoResources.GetString("Message_LoadFilePart2") + path + " " + LabDemoResources.GetString("Message_LoadFilePart3"),
                                LabDemoResources.GetString("Caption_IOError"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                throw new System.IO.IOException(LabDemoResources.GetString("Message_LoadFilePart1") + " Cip.Config.xml " + LabDemoResources.GetString("Message_LoadFilePart2") + path + " " + LabDemoResources.GetString("Message_LoadFilePart3"), expn);
            }
            XmlNode xmlCulture = xmlDoc.SelectSingleNode("/Cip.LabDemo/Culture");
            if (xmlCulture != null)
            {
                xmlCulture.ChildNodes[0].Value = culture;
            }
            try
            {
                xmlDoc.Save(path + "\\Cip.Config.xml");
            }
            catch (Exception expn)
            {
                MessageBox.Show(LabDemoResources.GetString("Message_SaveFilePart1") + " Cip.Config.xml " + LabDemoResources.GetString("Message_SaveFilePart2") + path + " " + LabDemoResources.GetString("Message_SaveFilePart3"),
                                "IO Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                throw new System.IO.IOException(LabDemoResources.GetString("Message_SaveFilePart1") + " Cip.Config.xml " + LabDemoResources.GetString("Message_SaveFilePart2") + path + " " + LabDemoResources.GetString("Message_SaveFilePart3"), expn);
            }
            //restarts app
            Application.Restart();
        }

        #endregion Methods

    }
}