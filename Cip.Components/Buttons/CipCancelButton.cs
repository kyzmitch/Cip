using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Cip.Components.Buttons
{
    [ToolboxItem(true), DesignTimeVisible(true), ToolboxBitmap(typeof(Button)), Description("Cip designed Cancel button")]
    public partial class CipCancelButton : CipButton
    {
        public CipCancelButton()
        {
            InitializeComponent();
        }

        public CipCancelButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            this.Image = Cip.Components.Properties.Resources.cip_cancel_clicked;
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            this.Image = Cip.Components.Properties.Resources.cip_cancel;
        }
    }
}
