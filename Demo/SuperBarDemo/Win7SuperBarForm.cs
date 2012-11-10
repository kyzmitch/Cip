using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Windows7.DesktopIntegration;
using Windows7.DesktopIntegration.WindowsForms;
using System.Runtime.InteropServices;

using OSVersion;

namespace SuperBarDemo
{

    public partial class Win7SuperBarForm : Form
    {
        private float _fTotalProgress;
        private Windows7.DesktopIntegration.JumpListManager _jumpListManager;
        private Windows7.DesktopIntegration.ThumbButton _thumbButton;
        private Windows7.DesktopIntegration.ThumbButtonManager _thumbButtonManager;

        //Information
        private string _infoUserName;
        private string _infoUserDomain;
        private Cip.CipOsVersion _osV;

        public Win7SuperBarForm()
        {
            InitializeComponent();
            _fTotalProgress = 0.0f;
            

            //Information
            _infoUserName = System.Windows.Forms.SystemInformation.UserName;
            _infoUserDomain = System.Windows.Forms.SystemInformation.UserDomainName;
            this.textBox1.Text = "UserName: " + _infoUserName + " UserDomain: " + _infoUserDomain;

            //OS version
            OSVersionInfo osvi3 = new OSVersionInfo();
            osvi3.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi3);
            bool result = LibWrap.GetVersionOS(ref osvi3);

            textBox5.Text = 
                "OS Build: " + osvi3.dwBuildNumber.ToString() + 
                " CSDVersion: " + osvi3.szCSDVersion;

            Microsoft.VisualBasic.Devices.ComputerInfo compInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            textBox3.Text = compInfo.OSFullName;
            textBox4.Text = compInfo.OSVersion;
            textBox2.Text = compInfo.OSPlatform;

            listBox1.MultiColumn = true;
            listBox1.Items.Add("OS Build:   " + osvi3.dwBuildNumber.ToString());
            listBox1.Items.Add("CSDVersion: " + osvi3.szCSDVersion);
            listBox1.Items.Add("OS Full Name: " + compInfo.OSFullName);
            listBox1.Items.Add("OS Version: " + compInfo.OSVersion);
            listBox1.Items.Add("OS Platform: " + compInfo.OSPlatform);

            _osV = new Cip.CipOsVersion();

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Windows7Taskbar.TaskbarButtonCreatedMessage)
            {
                _jumpListManager = WindowsFormsExtensions.CreateJumpListManager(this);
                _jumpListManager.UserRemovedItems += (o, e) =>
                    {
                        e.CancelCurrentOperation = false;
                    };
                _jumpListManager.AddUserTask(new ShellLink
                {
                    Path = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.System),
                        "calc.exe"),
                    Title = "Calculator",
                    Category = "Application",
                    IconLocation = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.System),
                        "calc.exe"),
                    IconIndex = 0
                });
                _jumpListManager.AddUserTask(new ShellLink
                {
                    Path = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.System),
                        "cmd.exe"),
                    Title = "Command Promt",
                    Category = "Application",
                    IconLocation = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.System),
                        "cmd.exe"),
                    IconIndex = 0
                });

                _jumpListManager.Refresh();

                if (_thumbButtonManager == null)
                {
                    _thumbButtonManager = WindowsFormsExtensions.CreateThumbButtonManager(this);
                }
                _thumbButton = _thumbButtonManager.CreateThumbButton(1, this.Icon, "Test");
                /*_thumbButton.Clicked += delegate
                {
                    MessageBox.Show("Test button clicked");
                };*/
                _thumbButton.Clicked += new EventHandler(_thumbButton_Clicked);
                _thumbButtonManager.AddThumbButtons(_thumbButton);

                if (_thumbButtonManager != null)
                {
                    _thumbButtonManager.DispatchMessage(ref m);
                }

            }

            base.WndProc(ref m);
        }

        private void LayeredWindows()
        {
            // Gets the version of the layered windows feature.
            Version myVersion =
                OSFeature.Feature.GetVersionPresent(OSFeature.LayeredWindows);

            // Prints whether the feature is available.
            if (myVersion != null)
                textBox2.Text = "Layered windows feature is installed.\n";
            else
                textBox2.Text = "Layered windows feature is not installed.\n";

        }

        void _thumbButton_Clicked(object sender, EventArgs e)
        {
            MessageBox.Show("Test button clicked");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_osV.GetOsTypeId() == Cip.CipOsType.Windows61)
            {
                WindowsFormsExtensions.SetTaskbarProgress(this, _fTotalProgress);
            }
            _fTotalProgress += 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_osV.GetOsTypeId() == Cip.CipOsType.Windows61)
            {
                WindowsFormsExtensions.SetTaskbarProgress(this, _fTotalProgress);
            }
            _fTotalProgress -= 10;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_osV.GetOsTypeId() == Cip.CipOsType.Windows61)
            {
                int result = comboBox1.SelectedIndex;
                switch (result)
                {
                    case 0:
                        WindowsFormsExtensions.SetTaskbarProgressState(this, Windows7Taskbar.ThumbnailProgressState.Normal);
                        break;
                    case 1:
                        WindowsFormsExtensions.SetTaskbarProgressState(this, Windows7Taskbar.ThumbnailProgressState.Indeterminate);
                        break;
                    case 2:
                        WindowsFormsExtensions.SetTaskbarProgressState(this, Windows7Taskbar.ThumbnailProgressState.Error);
                        break;
                    case 3:
                        WindowsFormsExtensions.SetTaskbarProgressState(this, Windows7Taskbar.ThumbnailProgressState.Paused);
                        break;
                    case 4:
                        WindowsFormsExtensions.SetTaskbarProgressState(this, Windows7Taskbar.ThumbnailProgressState.NoProgress);
                        break;
                }
            }
        }
    }
}
