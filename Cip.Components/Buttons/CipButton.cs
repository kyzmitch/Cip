using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace Cip.Components.Buttons
{
    [ToolboxItem(true), DesignTimeVisible(true), ToolboxBitmap(typeof(Button)), Description("Cip designed button")]
    public partial class CipButton : Button
    {
        public CipButton()
        {
            InitializeComponent();
        }

        public CipButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            this.Image = Cip.Components.Properties.Resources.cip_default_clicked;
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            this.Image = Cip.Components.Properties.Resources.cip_default;
        }
    }
}
