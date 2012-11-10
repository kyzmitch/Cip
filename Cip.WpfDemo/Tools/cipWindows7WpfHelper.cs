using System;
using System.Collections.Generic;
using System.Text;
using Windows7.DesktopIntegration;
using System.Windows.Interop;
using System.Windows.Media;
using System.Drawing;

namespace Cip.WpfDemo.Tools
{
    public static class CipWindows7WpfHelper
    {
        public static JumpListManager CreateJumpListManager(this System.Windows.Window Window)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(Window);
            return new JumpListManager(wndHelper.Handle);
        }
        public static ThumbButtonManager CreateThumbButtonManager(this System.Windows.Window Window)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(Window);
            return new ThumbButtonManager(wndHelper.Handle);
        }
        /*
        public static Icon ConvertImageSourceToIcon(ImageSource icon)
        {
            ImageSourceConverter conv = new ImageSourceConverter();
            Type typeOfIcon = typeof(System.Drawing.Icon);
            Icon gdiIcon = (Icon)conv.ConvertTo(icon, typeOfIcon); 
            return gdiIcon;
        }*/

    }
}
