namespace Cip.Components.Buttons
{
    partial class CipButton
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CipButton
            // 
            this.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Image = global::Cip.Components.Properties.Resources.cip_default;
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Size = new System.Drawing.Size(90, 30);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
