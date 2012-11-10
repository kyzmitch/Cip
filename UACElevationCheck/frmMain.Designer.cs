namespace WindowsApplication12
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.lbIsElevated = new System.Windows.Forms.Label();
            this.lbIsAdmin = new System.Windows.Forms.Label();
            this.lbVista = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 184);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(381, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "GetUACInfo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbIsElevated
            // 
            this.lbIsElevated.AutoSize = true;
            this.lbIsElevated.Location = new System.Drawing.Point(166, 128);
            this.lbIsElevated.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIsElevated.Name = "lbIsElevated";
            this.lbIsElevated.Size = new System.Drawing.Size(51, 20);
            this.lbIsElevated.TabIndex = 1;
            this.lbIsElevated.Text = "label1";
            // 
            // lbIsAdmin
            // 
            this.lbIsAdmin.AutoSize = true;
            this.lbIsAdmin.Location = new System.Drawing.Point(166, 87);
            this.lbIsAdmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIsAdmin.Name = "lbIsAdmin";
            this.lbIsAdmin.Size = new System.Drawing.Size(51, 20);
            this.lbIsAdmin.TabIndex = 2;
            this.lbIsAdmin.Text = "label1";
            // 
            // lbVista
            // 
            this.lbVista.AutoSize = true;
            this.lbVista.Location = new System.Drawing.Point(166, 46);
            this.lbVista.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbVista.Name = "lbVista";
            this.lbVista.Size = new System.Drawing.Size(51, 20);
            this.lbVista.TabIndex = 3;
            this.lbVista.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vista:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Elevation type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Elevation:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 248);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbVista);
            this.Controls.Add(this.lbIsAdmin);
            this.Controls.Add(this.lbIsElevated);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMain";
            this.Text = "UAC Elevation Check";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbIsElevated;
        private System.Windows.Forms.Label lbIsAdmin;
        private System.Windows.Forms.Label lbVista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

