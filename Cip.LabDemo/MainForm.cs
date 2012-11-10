/////////////////////////////////////////////////////////////////////////////////
// Cip.LabDemo                                                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Xml;

using Cip;
using Cip.Foundations;
using Cip.Filters;
using Cip.Transformations;
using Cip.Components.Buttons;
using Cip.LabDemo.DialogBoxes;
using Cip.LabDemo.Resources;


namespace Cip.LabDemo
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            LabDemoResources.Culture = new CultureInfo("ru-RU", true);
            InitializeComponent();

        }

        public MainForm(CultureInfo selectedCulture)
        {
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            LabDemoResources.Culture = selectedCulture;
            InitializeComponent();

        }

        #region Main form events

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //new size of the MainForm
            System.Drawing.Size nFormSize = this.Size;
            if (nFormSize.Height > 903)
                nFormSize.Height = 903;
            System.Drawing.Rectangle nEditorRectangel = this.tabPage1.ClientRectangle;
            System.Drawing.Rectangle nDescriptionRectangle = this.tabPage3.ClientRectangle;
            int delta = this.MinimumSize.Width - this.sclOriginalBoxSize.Width - this.sclModifyedBoxSize.Width;

            if (nFormSize.Width <= this.MinimumSize.Width || nFormSize.Height <= this.MinimumSize.Height)
            {
                this.Size = this.MinimumSize;
                this.groupBox1.Size = this.sclOriginalBoxSize;
                this.groupBox2.Size = this.sclModifyedBoxSize;
                this.groupBox2.Location = this.sclModifyedLocation;
                this.groupBoxCutColours.Location = this.sclCutColorsLocation;
                this.groupBox3.Location = this.sclHistoLocation;
                this.groupBoxNoteEditor.Location = this.sclNoteEditorLocation;
                this.tabControlDescription.Size = this.sclTabControlDescriptionSize;
                this.richTextBoxDescription.Size = this.sclRichTextBoxSize;
            }
            else
            {
                #region Editor Tab

                //Height.
                int clientHeight = nEditorRectangel.Height;
                int clientWidth = nEditorRectangel.Width;
                int nBoxHeight = clientHeight - this.groupBoxCutColours.Size.Height - 38;
                int nFormHeight = nFormSize.Height;
                //Width.
                int nFormWidth = delta + 2 * nBoxHeight;

                nFormSize = new Size(nFormWidth, nFormHeight);
                this.Size = nFormSize;

                this.groupBox1.Size = new Size(nBoxHeight, nBoxHeight);
                this.groupBox2.Size = new Size(nBoxHeight, nBoxHeight);
                this.groupBox2.Location = new Point(sclOriginalLocation.X + 6 + nBoxHeight, sclOriginalLocation.Y);
                this.groupBoxCutColours.Location = new Point(sclCutColorsLocation.X, 12 + nBoxHeight);
                this.groupBox3.Location = new Point(sclHistoLocation.X, 12 + nBoxHeight);
                this.groupBoxNoteEditor.Location = new Point(sclNoteEditorLocation.X, 12 + nBoxHeight);

                #endregion Editor Tab

                #region Description Tab

                int descriptionRichBoxHeight = (nFormSize.Height - this.sclStandartFormSize.Height) + this.sclRichTextBoxSize.Height;
                this.richTextBoxDescription.Size = new Size(this.sclRichTextBoxSize.Width, descriptionRichBoxHeight);
                int descriptionTabControlHeight = (nFormSize.Height - this.sclStandartFormSize.Height) + this.sclTabControlDescriptionSize.Height;
                int descriptionTabControlWidth = (nFormSize.Width - this.sclStandartFormSize.Width) + this.sclTabControlDescriptionSize.Width - 8;
                this.tabControlDescription.Size = new Size(descriptionTabControlWidth, descriptionTabControlHeight);

                #endregion Description Tab

            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            
            #region Scaled components

            this.sclStandartFormSize = this.Size;
            this.sclEditorRectangle = this.tabPage1.ClientRectangle;
            this.sclOriginalBoxSize = this.groupBox1.Size;
            this.sclModifyedBoxSize = this.groupBox2.Size;
            this.sclOriginalLocation = this.groupBox1.Location;
            this.sclModifyedLocation = this.groupBox2.Location;
            this.sclCutColorsLocation = this.groupBoxCutColours.Location;
            this.sclHistoLocation = this.groupBox3.Location;
            this.sclNoteEditorLocation = this.groupBoxNoteEditor.Location;
            this.sclRichTextBoxSize = this.richTextBoxDescription.Size;
            this.sclTabControlDescriptionSize = this.tabControlDescription.Size;
            //this.screenRectangle = Screen.PrimaryScreen.WorkingArea;
            this.screenRectangle = new Rectangle(0, 0, 1280, 1024);
            this.MaximumSize = new Size(screenRectangle.Width, screenRectangle.Height);

            #endregion Scaled components

            this.SwitchOffEditControlls(true);
            this.currentBitmap = (Bitmap)picBoxOriginal.Image;
            this.normalImageRaster = new Raster(this.currentBitmap);
            rasterComponents = new Raster((Bitmap)pictureBoxOrig.Image);
            UndoList = new Raster[range];
            iCurrentIndexOfList = 0;
            UndoList[iCurrentIndexOfList] = normalImageRaster;
            iCurrentCountOfList = 1;
            this.CheckUndoToolStrip();
            //colour box
            this.clSelectedColour = Color.Red;
            this.cutColourPicBox.Image = Raster.CreateBitmap(this.cutColourPicBox.Width, 
                                                             this.cutColourPicBox.Height, 
                                                             this.clSelectedColour);
            this.CreateMenuForPictureBox();
            this.IsHistoStopped = true;
            this.IsOpenEnds = true;
            this.timerCip.Stop();
            this.CalculateHistogram();
            this.thOpen = new Thread(new ParameterizedThreadStart(this.OpenPicture));
            //listBox
            this.listBoxMethods.SelectedIndex = 0;
            //picture showing
            this.IsPictureHolded = false;
            //language
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "ru-RU":
                    {
                        this.russianToolStripMenuItem.Checked = true;
                        this.englishToolStripMenuItem.Checked = false;
                        break;
                    }
                case "en-US":
                    {
                        this.russianToolStripMenuItem.Checked = false;
                        this.englishToolStripMenuItem.Checked = true;
                        break;
                    }
                default:
                    {
                        this.russianToolStripMenuItem.Checked = true;
                        this.englishToolStripMenuItem.Checked = false;
                        break;
                    }
            }
            
            
        }
        private void timerCip_Tick(object sender, EventArgs e)
        {
            if ((!this.thOpen.IsAlive) && (this.IsOpenEnds == false))
            {
                this.SwitchOffEditControlls(false);
                this.IsOpenEnds = true;
                this.timerCip.Stop();
            }
            //histogram after equalization
            if ((!this.backgroundWorkerCip.IsBusy) && (this.IsHistoStopped == false) && (this.IsOpenEnds == true))
            {
                this.IsHistoStopped = true;
                this.ShowImageAttributes(this.GetCurrentRaster().ToBitmap());
                if (!this.IsNullRaster)
                {
                    Thread thread_after = new Thread(new ParameterizedThreadStart(Cip.CipTools.PaintHistogram));
                    thread_after.Priority = ThreadPriority.Highest;
                    ArrayList param1 = new ArrayList();
                    param1.Add(this.GetCurrentRaster());
                    param1.Add(this.pictureBoxHistAfter);
                    param1.Add(Color.Gold);
                    param1.Add("");

                    Thread thread_before = new Thread(new ParameterizedThreadStart(Cip.CipTools.PaintHistogramLine));
                    ArrayList param2 = new ArrayList();
                    if (this.iCurrentIndexOfList - 1 >= 0)
                        param2.Add(this.UndoList[this.iCurrentIndexOfList - 1]);
                    else
                        param2.Add(this.UndoList[this.iCurrentIndexOfList]);
                    param2.Add(this.pictureBoxHistAfter);
                    param2.Add(Color.Black);
                    param2.Add("");
                    thread_before.Priority = ThreadPriority.Highest;

                    thread_after.Start(param1);
                    //if Bitmap is large, then little time is 
                    //not enough to calculate first Thread.
                    Thread.Sleep(500);
                    thread_before.Start(param2);
                    thread_after.Join();
                    thread_before.Join();
                }
                else
                {
                    Thread thread_after = new Thread(new ParameterizedThreadStart(Cip.CipTools.PaintHistogram));
                    thread_after.Priority = ThreadPriority.Normal;
                    ArrayList param1 = new ArrayList();
                    param1.Add(this.GetCurrentRaster());
                    param1.Add(this.pictureBoxHistAfter);
                    param1.Add(Color.Blue);
                    param1.Add("");
                    thread_after.Start(param1);
                }
                this.timerCip.Stop();
            }
        }
        private void picBoxModifyed_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)picBoxModifyed.Image);
            OutForm.ShowDialog();
        }
        private void picBoxOriginal_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)picBoxOriginal.Image);
            OutForm.ShowDialog();
        }
        private void pictureBoxOrig_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)this.pictureBoxOrig.Image);
            OutForm.ShowDialog();
        }
        private void pictureBoxH_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)this.pictureBoxH.Image);
            OutForm.ShowDialog();
        }
        private void pictureBoxS_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)this.pictureBoxS.Image);
            OutForm.ShowDialog();
        }
        private void pictureBoxI_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)this.pictureBoxI.Image);
            OutForm.ShowDialog();
        }
        private void pictureBoxR_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)this.pictureBoxR.Image);
            OutForm.ShowDialog();
        }
        private void pictureBoxG_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)this.pictureBoxG.Image);
            OutForm.ShowDialog();
        }
        private void pictureBoxB_DoubleClick(object sender, EventArgs e)
        {
            OutForm = new ImageOutForm((Bitmap)this.pictureBoxB.Image);
            OutForm.ShowDialog();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        this.SwitchOffEditControlls(true);
                        break;
                    }
                case 1:
                    {
                        this.SwitchOffEditControlls(false);

                        break;
                    }
                case 2:
                    {
                        this.SwitchOffEditControlls(true);
                        this.openToolStripMenuItem.Enabled = true;
                        break;
                    }
                default:
                    {
                        this.SwitchOffEditControlls(true);
                        break;
                    }
            }
        }

        #endregion Main form events

        #region Context Menu Events

        private void menu_Zoom_Click(object sender, EventArgs e)
        {
            this.picBoxOriginal.ContextMenu.MenuItems[1].Checked = false;
            this.picBoxOriginal.ContextMenu.MenuItems[2].Checked = false;
            this.picBoxOriginal.ContextMenu.MenuItems[3].Checked = true;
            this.picBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            this.picBoxModifyed.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void menu_Normal_Click(object sender, EventArgs e)
        {
            this.picBoxOriginal.ContextMenu.MenuItems[1].Checked = false;
            this.picBoxOriginal.ContextMenu.MenuItems[2].Checked = true;
            this.picBoxOriginal.ContextMenu.MenuItems[3].Checked = false;
            this.picBoxOriginal.SizeMode = PictureBoxSizeMode.Normal;
            this.picBoxModifyed.SizeMode = PictureBoxSizeMode.Normal;
        }
        private void menu_Stretch_Click(object sender, EventArgs e)
        {
            this.picBoxOriginal.ContextMenu.MenuItems[1].Checked = true;
            this.picBoxOriginal.ContextMenu.MenuItems[2].Checked = false;
            this.picBoxOriginal.ContextMenu.MenuItems[3].Checked = false;
            this.picBoxOriginal.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picBoxModifyed.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void menu_save_Click(object sender, EventArgs e)
        {
            Cip.CipTools.SaveImage(this.picBoxModifyed.Image);
        }
        private void menu_open_Click(object sender, EventArgs e)
        {
            this.openToolStripMenuItem_Click(sender, e);
        }

        #endregion

        #region BackgroundWorker Events

        private void backgroundWorkerCip_DoWork(object sender, DoWorkEventArgs e)
        {
             this.ApplyFilter((ImageFilter) e.Argument);
        }
        private void backgroundWorkerCip_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage < progressBarCip.Maximum)
                progressBarCip.Value = e.ProgressPercentage;
            statusLabelTime.Text = ((TimeSpan)e.UserState).ToString();
            //progressBarCip.Value = 0;
        }
        #endregion

        #region File menu

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newBitmap = Cip.CipTools.OpenImage();
            
            if(this.tabControl1.SelectedIndex==0)
                this.SwitchOffEditControlls(true);
            this.thOpen = new Thread(new ParameterizedThreadStart(this.OpenPicture));
            thOpen.Priority = ThreadPriority.Normal;
            ArrayList param = new ArrayList();
            param.Add(sender);
            param.Add(e);
            param.Add(newBitmap);

            this.thOpen.Start(param);
            this.IsOpenEnds = false;
            this.timerCip.Start();
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
            {
                DialogResult result = MessageBox.Show(LabDemoResources.GetString("Message_SaveFunctionWarning"),
                                                      LabDemoResources.GetString("Caption_Warning"), 
                                                      MessageBoxButtons.OKCancel,
                                                      MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                    Cip.CipTools.SaveImage(this.picBoxModifyed.Image);
                else return;
            }
            else
                Cip.CipTools.SaveImage(this.picBoxModifyed.Image);
        }

        #endregion

        #region Edit menu

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thUndo = new Thread(new ThreadStart(this.UndoFilter));
            thUndo.Priority = ThreadPriority.Normal;
            thUndo.Start();
            this.CalculateHistogram();
            //this.UndoFilter();
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thRedo = new Thread(new ThreadStart(this.RedoFilter));
            thRedo.Priority = ThreadPriority.Normal;
            thRedo.Start();
            this.CalculateHistogram();
            //this.RedoFilter();
        }
        private void defaultStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thDef = new Thread(new ThreadStart(this.DefaultFilter));
            thDef.Priority = ThreadPriority.Normal;
            thDef.Start();
            this.CalculateHistogram();
            //this.DefaultFilter();
        }
        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.menu_Stretch_Click(sender, e);
        }
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.menu_Normal_Click(sender, e);
        }
        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.menu_Zoom_Click(sender, e);
        }

        #endregion

        #region Settings menu

        private void russianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult wrnResult = MessageBox.Show(LabDemoResources.GetString("Message_ReloadApplication"),
                                                     LabDemoResources.GetString("Caption_Warning"),
                                                     MessageBoxButtons.OKCancel,
                                                     MessageBoxIcon.Warning);
            if (wrnResult == DialogResult.OK)
            {
                this.russianToolStripMenuItem.Checked = true;
                this.englishToolStripMenuItem.Checked = false;
                this.SetCulture("ru-RU");
            }
        }
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult wrnResult = MessageBox.Show(LabDemoResources.GetString("Message_ReloadApplication"),
                                                     LabDemoResources.GetString("Caption_Warning"),
                                                     MessageBoxButtons.OKCancel,
                                                     MessageBoxIcon.Warning);
            if (wrnResult == DialogResult.OK)
            {
                this.russianToolStripMenuItem.Checked = false;
                this.englishToolStripMenuItem.Checked = true;
                this.SetCulture("en-US");
            }
        }
        private void skinsSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.menuStripCip.RenderMode = ToolStripRenderMode.System;
            this.toolStripContainer1.TopToolStripPanel.RenderMode = ToolStripRenderMode.System;
            this.toolStripFile.RenderMode = ToolStripRenderMode.System;
            this.toolStripTools.RenderMode = ToolStripRenderMode.System;
        }
        private void skinsProfessionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.menuStripCip.RenderMode = ToolStripRenderMode.Professional;
            this.toolStripContainer1.TopToolStripPanel.RenderMode = ToolStripRenderMode.Professional;
            this.toolStripFile.RenderMode = ToolStripRenderMode.Professional;
            this.toolStripTools.RenderMode = ToolStripRenderMode.Professional;
        }

        #endregion Settings menu

        #region Help menu
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxCip aboutForm = new AboutBoxCip();
            aboutForm.ShowDialog();
        }
        #endregion Help menu

        #region Pseudocolor processing

        private void thresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cipFormThreshold formThreshold = new cipFormThreshold(this.picBoxModifyed.Image,this.GetCurrentRaster(),Thread.CurrentThread.CurrentUICulture);

            if (formThreshold.ShowDialog()==DialogResult.OK)
            {
                byte level = formThreshold.GetLevel();
                if (!backgroundWorkerCip.IsBusy)
                {
                    ImageFilter filter = new ThresholdFilter(level);
                    backgroundWorkerCip.RunWorkerAsync(filter);
                    this.CalculateHistogram();
                }
            }
        }
        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new GrayScale();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void splitToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.splitToolStripComboBox.SelectedIndex)
            {
                case 0://hue
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.SplitterFilter(SplitMode.Hue);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
                case 1://saturation
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.SplitterFilter(SplitMode.Saturation);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
                case 2://intensity
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.SplitterFilter(SplitMode.Intensity);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
                case 3://red
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.SplitterFilter(SplitMode.Red);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
                case 4://green
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.SplitterFilter(SplitMode.Green);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
                case 5://blue
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.SplitterFilter(SplitMode.Blue);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
            }
        }
        private void negativeRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.ColorAddition(ColorSpaceMode.RGB);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void negativeHSIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.ColorAddition(ColorSpaceMode.HSI);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }

        #endregion

        #region Image characteristics

        /// <summary>
        /// Showing HSI and RGB components of image
        /// </summary>
        private void btnProcessHsiComponents_Click(object sender, EventArgs e)
        {
            this.timerCip.Stop();
            ImageFilter filterHue = new SplitterFilter(Cip.Filters.SplitMode.Hue);
            ImageFilter filterSaturation = new SplitterFilter(Cip.Filters.SplitMode.Saturation);
            ImageFilter filterIntensity = new SplitterFilter(Cip.Filters.SplitMode.Intensity);
            ImageFilter filterRed = new SplitterFilter(Cip.Filters.SplitMode.Red);
            ImageFilter filterGreen = new SplitterFilter(Cip.Filters.SplitMode.Green);
            ImageFilter filterBlue = new SplitterFilter(Cip.Filters.SplitMode.Blue);
            ArrayList h = new ArrayList();
            ArrayList s = new ArrayList();
            ArrayList i = new ArrayList();
            ArrayList r = new ArrayList();
            ArrayList g = new ArrayList();
            ArrayList b = new ArrayList();

            h.Add(filterHue);
            h.Add(Cip.Filters.SplitMode.Hue);
            s.Add(filterSaturation);
            s.Add(Cip.Filters.SplitMode.Saturation);
            i.Add(filterIntensity);
            i.Add(Cip.Filters.SplitMode.Intensity);
            r.Add(filterRed);
            r.Add(Cip.Filters.SplitMode.Red);
            g.Add(filterGreen);
            g.Add(Cip.Filters.SplitMode.Green);
            b.Add(filterBlue);
            b.Add(Cip.Filters.SplitMode.Blue);
            ParameterizedThreadStart delegateFunc = new ParameterizedThreadStart(this.SplitComponents);

            Thread thread_h = new Thread(delegateFunc);
            Thread thread_s = new Thread(delegateFunc);
            Thread thread_i = new Thread(delegateFunc);
            Thread thread_r = new Thread(delegateFunc);
            Thread thread_g = new Thread(delegateFunc);
            Thread thread_b = new Thread(delegateFunc);
            thread_h.Priority = ThreadPriority.Normal;
            thread_s.Priority = ThreadPriority.Normal;
            thread_i.Priority = ThreadPriority.Normal;
            thread_r.Priority = ThreadPriority.Normal;
            thread_g.Priority = ThreadPriority.Normal;
            thread_b.Priority = ThreadPriority.Normal;
            thread_h.Start(h);
            thread_s.Start(s);
            thread_i.Start(i);
            thread_r.Start(r);
            thread_g.Start(g);
            thread_b.Start(b);
        }

        private void btnProcessHsiComponentsOpenImg_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        #endregion

        #region Colour excision

        private void radioButtonSphere_Click(object sender, EventArgs e)
        {
            radioButtonSphere.Checked = true;
            radioButtonCube.Checked = false;
        }
        private void radioButtonCube_Click(object sender, EventArgs e)
        {
            radioButtonSphere.Checked = false;
            radioButtonCube.Checked = true;
        }
        private void cutColourPicBox_Click(object sender, EventArgs e)
        {
            this.clSelectedColour = Cip.CipTools.SelectColour();
            Raster rastColour = new Raster(this.cutColourPicBox.Width, this.cutColourPicBox.Height, this.clSelectedColour);
            rastColour.ShowFilter(this.cutColourPicBox);
        }
        private void cutColourPicBox_MouseHover(object sender, EventArgs e)
        {
            Cip.CipTools.PaintTarget(this.cutColourPicBox, Color.Black, 3);
        }
        private void cutColourPicBox_MouseLeave(object sender, EventArgs e)
        {
            Raster rastColour = new Raster(this.cutColourPicBox.Width, this.cutColourPicBox.Height, this.clSelectedColour);
            rastColour.ShowFilter(this.cutColourPicBox);
        }
        private void btnCutColour_Click(object sender, EventArgs e)
        {
            double radius = Convert.ToDouble(cutRadiusTextBox.Text);
            
            Cip.Filters.ExcisionMode mode;
            if (radioButtonSphere.Checked == true)
                mode = ExcisionMode.Sphere;
            else
                mode = ExcisionMode.Cube;
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.ColorRangeExcision(mode, radius, this.clSelectedColour);
                backgroundWorkerCip.RunWorkerAsync(filter);
            }
        }
        private void picBoxModifyed_MouseUp(object sender, MouseEventArgs e)
        {
            this.IsPictureHolded = false;
            //Under Construction, problem with stretch image, that less than pictureBox!
            if (this.picBoxModifyed.SizeMode == PictureBoxSizeMode.Normal)
            {
                Point location = e.Location;
                if (this.checkBoxPipette.Checked)
                {
                    this.clSelectedColour = ((Bitmap)this.picBoxModifyed.Image).GetPixel(location.X, location.Y);
                    Cip.CipTools.ShowPixelComponents(this.clSelectedColour,
                                                                            this.textBoxH,
                                                                            this.textBoxS,
                                                                            this.textBoxI,
                                                                            this.textBoxR,
                                                                            this.textBoxG,
                                                                            this.textBoxB);
                    Raster rastColour = new Raster(this.cutColourPicBox.Width, this.cutColourPicBox.Height, this.clSelectedColour);
                    rastColour.ShowFilter(this.cutColourPicBox);
                }
                if (this.checkBoxMoving.Checked)
                { 
                    
                }
            }
        }
        private void picBoxModifyed_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsPictureHolded == true)
            { 
                
            }
        }
        private void picBoxModifyed_MouseDown(object sender, MouseEventArgs e)
        {
            this.IsPictureHolded = true;
            this.pointStart = e.Location;
        }

        #endregion

        #region Image Enhancement

        private void intensitySaturationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cipFormIntensitySaturation form = new cipFormIntensitySaturation(this.picBoxModifyed.Image,this.GetCurrentRaster());
            
            if (form.ShowDialog()==DialogResult.OK)
            {
                int intensity = form.GetIntensity();
                int saturation = form.GetSaturation();
                if (form.IsRGB())
                {
                    if (!backgroundWorkerCip.IsBusy)
                    {
                        ImageFilter filter = new Cip.Filters.IntensityChanger(intensity);
                        backgroundWorkerCip.RunWorkerAsync(filter);
                        this.CalculateHistogram();
                    }
                }
                else
                {
                    if (!backgroundWorkerCip.IsBusy)
                    {
                        ImageFilter filter = new Cip.Filters.HsiCorrectionFilter(intensity, saturation);
                        backgroundWorkerCip.RunWorkerAsync(filter);
                        this.CalculateHistogram();
                    }
                }
                    
            }
        }
        private void intensityCorrectionToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = intensityCorrectionToolStripComboBox.SelectedIndex;
            //0 Light Image
            //1 Dark Image
            //2 Soft Image
            switch (selected)
            {
                case 0:
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.IntensityCorrection(IntensityCorrectionMode.LightImage);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
                case 1:
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.IntensityCorrection(IntensityCorrectionMode.DarkImage);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
                case 2:
                    {
                        if (!backgroundWorkerCip.IsBusy)
                        {
                            ImageFilter filter = new Cip.Filters.IntensityCorrection(IntensityCorrectionMode.SoftImage);
                            backgroundWorkerCip.RunWorkerAsync(filter);
                            this.CalculateHistogram();
                        }
                        break;
                    }
            }
        }
        private void lightnessAndContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cipFormLightnessContrast form = new cipFormLightnessContrast(this.picBoxModifyed.Image,this.GetCurrentRaster());

            if (form.ShowDialog()==DialogResult.OK)
            {
                int lightness = form.GetLightness();
                int contrast = form.GetContrast();
                if (!backgroundWorkerCip.IsBusy)
                {
                    ImageFilter filter = new Cip.Filters.LightFilter(lightness, contrast);
                    backgroundWorkerCip.RunWorkerAsync(filter);
                    this.CalculateHistogram();
                }
            }
        }
        
        #region Sharpness
        private void sharpnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cipFormSharpness form = new cipFormSharpness();
            
            if (form.ShowDialog()==DialogResult.OK)
            {
                bool diag = form.IsDiag();
                bool negative = form.IsNegative();
                Cip.Filters.ColorSpaceMode mode = form.GetMode();
                if (!backgroundWorkerCip.IsBusy)
                {
                    ImageFilter filter = new Cip.Filters.SharpnessIncreaseFilter(mode, diag, negative);
                    backgroundWorkerCip.RunWorkerAsync(filter);
                    this.CalculateHistogram();
                }
            }
        }
        #endregion Sharpness

        #region Smoothing
        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cipFormSmoothing form = new cipFormSmoothing(this.picBoxModifyed.Image,this.GetCurrentRaster());

            if (form.ShowDialog()==DialogResult.OK)
            {
                Cip.Filters.ColorSpaceMode mode = form.GetMode();
                Cip.Filters.SmoothingMode sMode = form.GetSMode();
                float sigma = form.GetSigma();
                int radius = form.GetRadius();
                #region switch
                switch (sMode)
                {
                    case SmoothingMode.Smoothing:
                        {
                            if (!backgroundWorkerCip.IsBusy)
                            {
                                ImageFilter filter = new Cip.Filters.SmoothingFilter(mode, radius);
                                backgroundWorkerCip.RunWorkerAsync(filter);
                                this.CalculateHistogram();
                            }
                            break;
                        }
                    case SmoothingMode.Blur:
                        {
                            if (!backgroundWorkerCip.IsBusy)
                            {
                                ImageFilter filter = Cip.Filters.LinearFilter.SimpleBlurFilter(radius);
                                backgroundWorkerCip.RunWorkerAsync(filter);
                                this.CalculateHistogram();
                            }
                            break;
                        }
                    case SmoothingMode.GaussianBlur:
                        {
                            if (!backgroundWorkerCip.IsBusy)
                            {
                                ImageFilter filter = Cip.Filters.LinearFilter.GaussianBlurFilter(radius, sigma);
                                backgroundWorkerCip.RunWorkerAsync(filter);
                                this.CalculateHistogram();
                            }
                            break;
                        }
                }
                #endregion
            }
        }
        #endregion Smoothing

        #region Histogram

        private void equalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.HistogramEqualization();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void histogramNormalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.HistogramNormalize();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void histogramCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.histogramToolStripButton_Click(sender, e);
        }

        #endregion Histogram

        #endregion Image Enhancement

        #region Effects menu

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.SepiaFilter(Color.Empty);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void bloomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cipFormBloom form = new cipFormBloom(this.picBoxModifyed.Image,this.GetCurrentRaster());
            if(form.ShowDialog()==DialogResult.OK)
            {
                int radius = form.Radius;
                float blendFactor = form.BlendFactor;
                float thresholdLevel = form.ThresholdLevel;
                if (!backgroundWorkerCip.IsBusy)
                {
                    ImageFilter filter = new Cip.Filters.Bloom(blendFactor, thresholdLevel, radius);
                    backgroundWorkerCip.RunWorkerAsync(filter);
                    this.CalculateHistogram();
                }
            }
        }
        private void stampingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                StampingFilter filter = new StampingFilter();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void colorPenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter colorPenFilter = LinearFilter.ColorPenFilter();
                backgroundWorkerCip.RunWorkerAsync(colorPenFilter);
                this.CalculateHistogram();
            }
        }

        #endregion Effects menu

        #region Morphologic Processing

        private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.EdgeFilter(1);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void dilateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.DilateFilter(1);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void erodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.ErodeFilter(1);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }

        #endregion Morphologic Processing

        #region Tool strip File

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            this.openToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            this.saveAsToolStripMenuItem_Click(sender, e);
        }

        #endregion

        #region Tool strip tools

        private void lightSharpeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = Cip.Filters.LinearFilter.LightSharpnessIncrease();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void highSharpeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = Cip.Filters.LinearFilter.HighSharpnessIncrease();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void gaussianBlur3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = Cip.Filters.LinearFilter.GaussianBlurFilter(1, 2f);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void fastSmoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.SmoothingFilter(Cip.Filters.ColorSpaceMode.RGB, 1);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void gaussianBlur5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = Cip.Filters.LinearFilter.GaussianBlurFilter(2, 2f);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void histogramToolStripButton_Click(object sender, EventArgs e)
        {
            cipFormHistogram form = new cipFormHistogram(this.picBoxModifyed.Image,this.GetCurrentRaster());

            if (form.ShowDialog()==DialogResult.OK)
            {
                if (form.Mode == HistogramMode.Equalize)
                    this.equalizeToolStripMenuItem_Click(sender, e);
                else
                {
                    if (form.Mode == HistogramMode.Normalize)
                        this.histogramNormalizeToolStripMenuItem_Click(sender, e);
                    else
                        #region Mode selection
                        switch (form.Mode)
                        {
                            case HistogramMode.StretchLuminance:
                                {
                                    if (!backgroundWorkerCip.IsBusy)
                                    {
                                        ImageFilter filter = new Cip.Filters.HistogramStretch(0, form.Level);
                                        backgroundWorkerCip.RunWorkerAsync(filter);
                                        this.CalculateHistogram();
                                    }
                                    break;
                                }
                            case HistogramMode.StretchLinkedChannels:
                                {
                                    if (!backgroundWorkerCip.IsBusy)
                                    {
                                        ImageFilter filter = new Cip.Filters.HistogramStretch(1, form.Level);
                                        backgroundWorkerCip.RunWorkerAsync(filter);
                                        this.CalculateHistogram();
                                    }
                                    break;
                                }
                            case HistogramMode.StretchIndependentChannels:
                                {
                                    if (!backgroundWorkerCip.IsBusy)
                                    {
                                        ImageFilter filter = new Cip.Filters.HistogramStretch(2, form.Level);
                                        backgroundWorkerCip.RunWorkerAsync(filter);
                                        this.CalculateHistogram();
                                    }
                                    break;
                                }
                        }
                        #endregion
                }
            }
        }

        #endregion

        #region Matrix filters
        
        private void prewittFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = Cip.Filters.LinearFilter.PrewittFilter();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void sobelFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = Cip.Filters.LinearFilter.SobelFilter();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }
        private void laplasFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = Cip.Filters.LinearFilter.LaplasFilter();
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }
        }

        #endregion Matrix filters

        #region Example pictures

        private void girlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenPictureFromBitmap(Cip.LabDemo.Properties.Resources.pl_girl);
        }
        private void berrysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenPictureFromBitmap(Cip.LabDemo.Properties.Resources.berrys);
        }
        private void moonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenPictureFromBitmap(Cip.LabDemo.Properties.Resources.Fig3_40_a_sharpness_Moon);
        }
        private void crayonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenPictureFromBitmap(Cip.LabDemo.Properties.Resources.intensity_light_image);
        }

        #endregion

        #region Description

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((this.listBoxMethods.SelectedIndex + 1) == this.listBoxMethods.Items.Count)
                this.listBoxMethods.SelectedIndex = 0;
            else
                this.listBoxMethods.SelectedIndex++;
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if ((this.listBoxMethods.SelectedIndex) == 0)
                this.listBoxMethods.SelectedIndex = this.listBoxMethods.Items.Count - 1;
            else
                this.listBoxMethods.SelectedIndex--;
        }
        private void listBoxMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            DescriptionFunctions description = new DescriptionFunctions(this.tabControlDescription, this.richTextBoxDescription);
            
            #region switch
            switch (this.listBoxMethods.SelectedIndex)
            {
                case 0:
                    {
                        //GrayScale
                        description.GrayScale();
                        break;
                    }
                case 1:
                    {
                        //Threshold
                        description.Threshold();
                        break;
                    }
                case 2:
                    {
                        //Split
                        description.Split();
                        break;
                    }
                case 3:
                    {
                        //Sepia
                        description.SepiaFilter();
                        break;
                    }
                case 4:
                    {
                        //intensity and saturation
                        description.IntensitySaturation();
                        break;
                    }
                case 5:
                    {
                        //lightness and contrast
                        description.LightnessContrast();
                        break;
                    }
                case 6:
                    {
                        //intensity correction
                        description.IntensityCorrection();
                        break;
                    }
                case 7:
                    {
                        //Negative
                        description.Negative();
                        break;
                    }
                case 8:
                    {
                        //Erode
                        description.Erode();
                        break;
                    }
                case 9:
                    {
                        //dilate
                        description.Dilate();
                        break;
                    }
                case 10:
                    {
                        //Edge
                        description.Edge();
                        break;
                    }
                case 11:
                    {
                        //smoothing
                        description.Smoothing();
                        break;
                    }
                case 12:
                    {
                        //sharpness
                        description.Sharpness();
                        break;
                    }
                case 13:
                    {
                        //Histogram equalize
                        description.HistogramEqualize();
                        break;
                    }
                case 14:
                    {
                        //Histogram normalize
                        description.HistogramNormalize();
                        break;
                    }
                case 15:
                    {
                        //Stamping
                        description.Stamping();
                        break;
                    }
                case 16:
                    { 
                        //Bloom
                        description.Bloom();
                        break;
                    }
                case 17:
                    { 
                        //color range excision
                        description.ColorRangeExcision();
                        break;
                    }
                default:
                    {
                        description.Clear();
                        break;
                    }
            }
            #endregion switch
        }

        #endregion Description

        #region Examples

        private void exampleTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.normalImageRaster = new Raster(Cip.LabDemo.Properties.Resources.example_text_Fig4_19_a);
            this.currentBitmap = Cip.LabDemo.Properties.Resources.example_text_Fig4_19_a;
            this.ShowImageAttributes(this.currentBitmap);
            this.defaultStateToolStripMenuItem_Click(sender, e);
            this.picBoxOriginal.Image = Cip.LabDemo.Properties.Resources.example_text_smoothing;
        }
        private void exampleFingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.normalImageRaster = new Raster(Cip.LabDemo.Properties.Resources.finger_print);
            this.currentBitmap = Cip.LabDemo.Properties.Resources.finger_print;
            this.ShowImageAttributes(this.currentBitmap);
            this.defaultStateToolStripMenuItem_Click(sender, e);
            this.picBoxOriginal.Image = Cip.LabDemo.Properties.Resources.finger_print_threshold;
        }
        private void exampleColorTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.normalImageRaster = new Raster(Cip.LabDemo.Properties.Resources.example_text_color);
            this.currentBitmap = Cip.LabDemo.Properties.Resources.example_text_color;
            this.ShowImageAttributes(this.currentBitmap);
            this.defaultStateToolStripMenuItem_Click(sender, e);
            this.picBoxOriginal.Image = Cip.LabDemo.Properties.Resources.example_text_color_result;
        }
        private void exampleColorText2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.normalImageRaster = new Raster(Cip.LabDemo.Properties.Resources.example_text_color);
            this.currentBitmap = Cip.LabDemo.Properties.Resources.example_text_color;
            this.ShowImageAttributes(this.currentBitmap);
            this.defaultStateToolStripMenuItem_Click(sender, e);
            this.picBoxOriginal.Image = Cip.LabDemo.Properties.Resources.example_text_color_result_cut_clr;
        }

        #endregion Examples

        private void resampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cipFormResample form = new cipFormResample(this.picBoxModifyed.Image);

            if (form.ShowDialog()==DialogResult.OK)
            {
                if (!backgroundWorkerCip.IsBusy)
                {
                    ImageFilter filter = new Resample(form.NewImageSize, form.Mode);
                    backgroundWorkerCip.RunWorkerAsync(filter);
                    this.CalculateHistogram();
                }
            }
        }

        private void matrixFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cipFormMatrixFilter matrixForm = new cipFormMatrixFilter(this.GetCurrentRaster().ToBitmap());

            if (matrixForm.ShowDialog()==DialogResult.OK)
            {
                if (!backgroundWorkerCip.IsBusy)
                {
                    ImageFilter filter = matrixForm.Filter;
                    backgroundWorkerCip.RunWorkerAsync(filter);
                    this.CalculateHistogram();
                }
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            AboutBoxCip aboutBox = new AboutBoxCip();
            aboutBox.ShowDialog();

            /*if (!backgroundWorkerCip.IsBusy)
            {
                ImageFilter filter = new Cip.Filters.BitPlainExcision(4);
                backgroundWorkerCip.RunWorkerAsync(filter);
                this.CalculateHistogram();
            }*/
        }

        private void oSInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cip.CipOsVersion osVersion = new CipOsVersion();
            string osInfoString = osVersion.GetInfoString();
            MessageBox.Show(osInfoString, "Operation System info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        

        

        

        

        

        

        

        

        

       

    }
}