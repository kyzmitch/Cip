using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Cip.Components.Buttons
{
    [ToolboxItem(true), DesignTimeVisible(true), ToolboxBitmap(typeof(Button)), Description("Cip designed Ok button")]
    public partial class CipOkButton : CipButton
    {
        public CipOkButton()
        {
            InitializeComponent();
        }

        public CipOkButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            this.Image = Cip.Components.Properties.Resources.cip_ok_clicked;
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            this.Image = Cip.Components.Properties.Resources.cip_ok;
        }
    }
}
