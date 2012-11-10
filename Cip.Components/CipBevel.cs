using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Cip.Components
{
    public partial class CipBevel : UserControl
    {
        public CipBevel()
        {
            InitializeComponent();
        }
        protected override void OnResize(EventArgs e)
        {
            this.Height = 10;
            this.label1.Width = this.Width;
        }
    }
}
