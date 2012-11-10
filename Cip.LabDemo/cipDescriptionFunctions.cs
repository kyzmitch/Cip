/////////////////////////////////////////////////////////////////////////////////
// Cip.LabDemo                                                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Cip.LabDemo.Resources;

namespace Cip.LabDemo
{
    public class DescriptionFunctions
    {
        public TabControl tab;
        public RichTextBox textBox;

        public DescriptionFunctions(TabControl tab, RichTextBox textBox)
        {
            this.tab = tab;
            this.textBox = textBox;
        }
        public void Clear()
        {
            tab.Controls.Clear();
            textBox.Clear();
        }
        public void UnderConstruction()
        {
            this.Clear();
            textBox.Text = LabDemoResources.GetString("Description_UnderConstruction");
        }
        public void GrayScale()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_GrayScale;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.color_cube;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.GrayScaled;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_BlackAndWhiteImage"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.SelectedIndex = 0;
        }
        public void Threshold()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Threshold;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.finger_print;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.finger_print_threshold;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_ImageAfterThresholding"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.SelectedIndex = 0;
        }
        public void Split()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Split;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.color_cube;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.hue_colorcube_example;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_HueComponent"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.SelectedIndex = 0;
        }
        public void SepiaFilter()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Sepia;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.pl_girl;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.sepia_girl_example;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_ImageAfterSepia"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.SelectedIndex = 0;
        }
        public void IntensitySaturation()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_IntensityAndSaturation;
        }
        public void LightnessContrast()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_LightnessAndContrast;
        }
        public void IntensityCorrection()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_IntensityCorrection;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.intensity_low_contrast;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_LowContrastImage"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.intensity_low_contrast_edited;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_CorrectionResult"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //3ed picture
            PictureBox pic3 = new PictureBox();
            pic3.Image = Cip.LabDemo.Properties.Resources.intensity_light_image;
            TabPage tabPage3 = new TabPage(LabDemoResources.GetString("Description_LightImage"));
            tabPage3.BorderStyle = BorderStyle.FixedSingle;
            tabPage3.Controls.Add(pic3);
            ((PictureBox)tabPage3.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage3.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //4ed picture
            PictureBox pic4 = new PictureBox();
            pic4.Image = Cip.LabDemo.Properties.Resources.intensity_light_image_edited;
            TabPage tabPage4 = new TabPage(LabDemoResources.GetString("Description_CorrectionResult"));
            tabPage4.BorderStyle = BorderStyle.FixedSingle;
            tabPage4.Controls.Add(pic4);
            ((PictureBox)tabPage4.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage4.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //5th picture
            PictureBox pic5 = new PictureBox();
            pic5.Image = Cip.LabDemo.Properties.Resources.intensity_dark_image;
            TabPage tabPage5 = new TabPage(LabDemoResources.GetString("Description_DarkImage"));
            tabPage5.BorderStyle = BorderStyle.FixedSingle;
            tabPage5.Controls.Add(pic5);
            ((PictureBox)tabPage5.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage5.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //6th picture
            PictureBox pic6 = new PictureBox();
            pic6.Image = Cip.LabDemo.Properties.Resources.intensity_dark_image_edited;
            TabPage tabPage6 = new TabPage(LabDemoResources.GetString("Description_CorrectionResult"));
            tabPage6.BorderStyle = BorderStyle.FixedSingle;
            tabPage6.Controls.Add(pic6);
            ((PictureBox)tabPage6.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage6.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;

            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.Controls.Add(tabPage3);
            this.tab.Controls.Add(tabPage4);
            this.tab.Controls.Add(tabPage5);
            this.tab.Controls.Add(tabPage6);
            this.tab.SelectedIndex = 0;
        }
        public void Negative()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Negative;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.negative_description;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_ColorAdditionExample"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.SelectedIndex = 0;
        }
        public void Erode()
        {   
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Erode;
        }
        public void Dilate()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Dilate;
        }
        public void Edge()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Edge;
        }
        public void Smoothing()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Smoothing;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.pl_girl;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.smoothing_example_5;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_ImageAfterBlur5x5"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.SelectedIndex = 0;
        }
        public void Sharpness()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Sharpness;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.pl_girl;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.sharpness_example;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_ImageAfterSharpening"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //3ed picture
            PictureBox pic3 = new PictureBox();
            pic3.Image = Cip.LabDemo.Properties.Resources.sharpness_example_difference;
            TabPage tabPage3 = new TabPage(LabDemoResources.GetString("Description_DifferenceBetweenRgbHsi"));
            tabPage3.BorderStyle = BorderStyle.FixedSingle;
            tabPage3.Controls.Add(pic3);
            ((PictureBox)tabPage3.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage3.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //4ed picture
            PictureBox pic4 = new PictureBox();
            pic4.Image = Cip.LabDemo.Properties.Resources.laplasian_masks;
            TabPage tabPage4 = new TabPage(LabDemoResources.GetString("Description_Masks"));
            tabPage4.BorderStyle = BorderStyle.FixedSingle;
            tabPage4.Controls.Add(pic4);
            ((PictureBox)tabPage4.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage4.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;

            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.Controls.Add(tabPage3);
            this.tab.Controls.Add(tabPage4);
            this.tab.SelectedIndex = 0;
        }
        public void HistogramEqualize()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_HistogramEqualize;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.pl_girl;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.pl_girl_equalized;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_ImageAfterEqualization"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.SelectedIndex = 0;
        }
        public void HistogramNormalize()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_HistogramNormalization;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.intensity_light_image;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.normalization_example;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_ImageAfterNormalization"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.SelectedIndex = 0;
        }
        public void HistogramStretch()
        {
            Clear();
            //textBox.Rtf = Cip.LabDemo.Properties.Resources.description_HistogramEqualize;
            this.UnderConstruction();
        }
        public void Stamping()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Stamping;
        }
        public void Bloom()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_Bloom;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.Subaru_Legacy_B4;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.Subaru_Legacy_B4_bloomed_factor_2_rad_4;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_ImageAfterBloom"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.SelectedIndex = 0;
        }
        public void ColorRangeExcision()
        {
            Clear();
            textBox.Rtf = Cip.LabDemo.Properties.Resources.description_ColorRangeExcision;
            //first picture
            PictureBox pic1 = new PictureBox();
            pic1.Image = Cip.LabDemo.Properties.Resources.cute_colour_range_example;
            TabPage tabPage1 = new TabPage(LabDemoResources.GetString("Description_ExampleCutFromDip"));
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.Controls.Add(pic1);
            ((PictureBox)tabPage1.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage1.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //second picture
            PictureBox pic2 = new PictureBox();
            pic2.Image = Cip.LabDemo.Properties.Resources.example_text_color;
            TabPage tabPage2 = new TabPage(LabDemoResources.GetString("Description_Original"));
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.Controls.Add(pic2);
            ((PictureBox)tabPage2.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage2.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;
            //3ed picture
            PictureBox pic3 = new PictureBox();
            pic3.Image = Cip.LabDemo.Properties.Resources.example_text_color_result_cut_clr;
            TabPage tabPage3 = new TabPage(LabDemoResources.GetString("Description_ImageAfterProcessing"));
            tabPage3.BorderStyle = BorderStyle.FixedSingle;
            tabPage3.Controls.Add(pic3);
            ((PictureBox)tabPage3.Controls[0]).Dock = DockStyle.Fill;
            ((PictureBox)tabPage3.Controls[0]).SizeMode = PictureBoxSizeMode.Zoom;

            this.tab.Controls.Add(tabPage1);
            this.tab.Controls.Add(tabPage2);
            this.tab.Controls.Add(tabPage3);
            this.tab.SelectedIndex = 0;
        }
    }
}
