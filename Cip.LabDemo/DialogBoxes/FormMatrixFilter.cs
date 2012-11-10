using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cip.LabDemo.DialogBoxes
{
    public partial class cipFormMatrixFilter : Form
    {
        #region Designer



        #endregion Designer

        /// <summary>
        /// Kernel of linear filter.
        /// </summary>
        private float[,] kernel;
        /// <summary>
        /// Size of kernel.
        /// </summary>
        private int size;
        /// <summary>
        /// The divisor.
        /// </summary>
        private int divisor;
        /// <summary>
        /// The bias.
        /// </summary>
        private int bias;
        /// <summary>
        /// The filter.
        /// </summary>
        private Cip.Filters.LinearFilter filter;
        /// <summary>
        /// Constructor of the form.
        /// </summary>
        /// <param name="image">Source image.</param>
        public cipFormMatrixFilter(Bitmap image)
        {
            InitializeComponent();

            //SWITCH OFF, NOT CONSTRUCTED
            this.comboBoxFilters.Enabled = false;

            this.SetDefaultState();
            if (image.Width > 800)
            { 
                Cip.Transformations.CipSize nSize = Cip.Transformations.Resample.SizeAdaptHeight(image.Size,800);
                image = (Bitmap)Cip.Transformations.ResampleGdi.ResizeImageHQ(image, 800, nSize.Height);
            }

            this.textBoxDivisor.Enabled = true;
            this.checkBoxDivisor.Checked = true;
            this.textBoxBias.Enabled = false;
            this.checkBoxBias.Checked = false;
            this.comboBoxFilters.SelectedIndex = 0;
            this.filterPreviewBox1.SetFilter((Cip.Foundations.ImageFilter)filter);
            this.filterPreviewBox1.SetBitmap(image);
            
        }
        /// <summary>
        /// Provides access to kernel.
        /// </summary>
        public float[,] Kernel
        {
            get { return this.kernel; }
        }
        /// <summary>
        /// Provides access to size of kernel.
        /// </summary>
        public int MatrixDimension
        {
            get { return this.size; }
        }
        /// <summary>
        /// Provides access to filter.
        /// </summary>
        public Cip.Filters.LinearFilter Filter
        {
            get { return this.filter; }
        }
        /// <summary>
        /// Sets default state of the filter.
        /// </summary>
        private void SetDefaultState()
        {
            size = 3;
            divisor = size * size;
            bias = 0;
            kernel = new float[size, size];
            this.dataMatrix.ColumnCount = size;
            this.dataMatrix.RowCount = size;
            this.dataMatrix.ColumnHeadersVisible = false;
            this.dataMatrix.RowHeadersVisible = false;
            this.textBoxDivisor.Text = Convert.ToString(divisor);
            this.textBoxBias.Text = Convert.ToString(bias);
            this.upDownDimension.Value = size;
            this.upDownDimension.Increment = 2;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    dataMatrix.Columns[j].Width = 30;
                    dataMatrix[j, i].Value = 1;
                    this.kernel[i, j] = 1.0f;
                }
            }
            filter = Cip.Filters.LinearFilter.SimpleBlurFilter(size);
        }
        /// <summary>
        /// Set matrix of filter.
        /// </summary>
        /// <param name="dim">Dimension of the matrix.</param>
        private void SetMatrix(int dim)
        {
            this.size = dim;
            kernel = new float[size, size];
            this.dataMatrix.ColumnCount = size;
            this.dataMatrix.RowCount = size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    dataMatrix.Columns[j].Width = 30;
                    dataMatrix[j, i].Value = 1;
                }
            }
            CalculateDivizorSum();
            FilterUpdate();
        }
        /// <summary>
        /// Update filter.
        /// </summary>
        private void FilterUpdate()
        {
            if (checkBoxDivisor.Checked)
            {
                if (this.divisor != 0)
                {
                    for (int i = 0; i < size; i++)
                        for (int j = 0; j < size; j++)
                        {
                            try
                            {
                                this.kernel[i, j] = (float)Convert.ToDouble(dataMatrix[j, i].Value) / this.divisor;
                            }
                            catch (Exception)
                            { 
                            }
                        }
                }
                else
                {
                    for (int i = 0; i < size; i++)
                        for (int j = 0; j < size; j++)
                        {
                            try
                            {
                                this.kernel[i, j] = (float)Convert.ToDouble(dataMatrix[j, i].Value);
                            }
                            catch (Exception)
                            { 
                            }

                        }
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        try
                        {
                            this.kernel[i, j] = (float)Convert.ToDouble(dataMatrix[j, i].Value);
                        }
                        catch (Exception)
                        { 
                        }
                    }
            }
            if (checkBoxBias.Checked)
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        this.kernel[i, j] += this.bias;
            }

            filter = new Cip.Filters.LinearFilter(this.Kernel);
            this.filterPreviewBox1.SetFilter((Cip.Foundations.ImageFilter)filter);
        }
        /// <summary>
        /// Update matrix in the form.
        /// </summary>
        private void FilterUpdate2()
        {
            this.size = this.Kernel.GetLength(0);
            this.dataMatrix.ColumnCount = size;
            this.dataMatrix.RowCount = size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    dataMatrix.Columns[j].Width = 30;
                    dataMatrix[j, i].Value = (int)this.kernel[i, j];
                }
            }
            this.filterPreviewBox1.SetFilter((Cip.Foundations.ImageFilter)this.Filter);
        }
        /// <summary>
        /// Calculates divisor.
        /// </summary>
        private void CalculateDivizorSum()
        {
            int div = 0;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    try
                    {
                        div += Convert.ToInt32(dataMatrix[j, i].Value);
                    }
                    catch(Exception)
                    { 
                    }
                }
            this.divisor = div;
            this.textBoxDivisor.Text = Convert.ToString(this.divisor);
            this.FilterUpdate();
        }

        #region Events

        private void upDownDimension_ValueChanged(object sender, EventArgs e)
        {
            if (this.upDownDimension.Value < 3)
                this.upDownDimension.Value = 3;
            this.SetMatrix((int)this.upDownDimension.Value);
        }

        private void divisorCalcButton_Click(object sender, EventArgs e)
        {
            CalculateDivizorSum();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            FilterUpdate();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dataMatrix_SelectionChanged(object sender, EventArgs e)
        {
            FilterUpdate();
        }

        private void checkBoxDivisor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDivisor.Checked)
                textBoxDivisor.Enabled = true;
            else
                textBoxDivisor.Enabled = false;
            FilterUpdate();
        }

        private void checkBoxBias_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBias.Checked)
                textBoxBias.Enabled = true;
            else
                textBoxBias.Enabled = false;
            FilterUpdate();
        }

        private void comboBoxFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkBoxDivisor.Checked = false;
            this.checkBoxBias.Checked = false;
            switch (this.comboBoxFilters.SelectedIndex)
            {
                case 0:
                    {
                        //this.filter = Cip.Filters.LinearFilter.SimpleBlurFilter(3);
                        //this.kernel = this.Filter.Kernel;
                        //FilterUpdate2();
                        break;
                    }
            }
        }

        #endregion Events
    }
}
