namespace Cip.Components.Buttons
{
    partial class CipCancelButton
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
            // CipCancelButton
            // 
            this.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.Image = global::Cip.Components.Properties.Resources.cip_cancel;
            this.Size = new System.Drawing.Size(74, 30);
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ResumeLayout(false);

        }

        #endregion
    }
}
