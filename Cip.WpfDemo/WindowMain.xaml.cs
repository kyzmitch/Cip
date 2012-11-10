using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

using Cip.WpfDemo.Tools;
using Cip.WpfDemo.Properties;
using Cip;
using Cip.Foundations;
using Cip.Filters;
using Cip.Collections;

using Windows7.DesktopIntegration;
using Windows7.DesktopIntegration.WindowsForms;

namespace Cip.WpfDemo
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        //Delegates for thread-safe work
        delegate void SetPictureCallback();
        delegate void SetMenuDefaultCallback();
        delegate void SetMenuUndoCallback();
        delegate void SetMenuRedoCallback();

        private System.Drawing.Bitmap _sourceBitmap;
        private System.Drawing.Bitmap _currentBitmap;
        private Cip.Foundations.Raster _sourceRaster;
        private Cip.Collections.CipUndoList<Raster> _undoList;

        private System.Windows.Forms.PictureBox _pictureBoxOriginal;
        private System.Windows.Forms.PictureBox _pictureBoxModifyed;

        //private System.Threading.Thread _threadOfProcessing;
        private System.Threading.Mutex _mutex;
        private System.Threading.Timer _timer;
        private System.Windows.Forms.Timer _timerWinForms;
        private bool _bNeedUpdate;

        //Information about OS
        private Cip.CipOsVersion _osVersion;

        //Windows7 gadgets
        private Windows7.DesktopIntegration.JumpListManager _jumpListManager;
        private Windows7.DesktopIntegration.ThumbButton _thumbButton;
        private Windows7.DesktopIntegration.ThumbButtonManager _thumbButtonManager;

        public WindowMain()
        {

            //Information about OS
            _osVersion = new CipOsVersion();
        }

        

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            WindowAbout aboutWnd = new WindowAbout();
            aboutWnd.ShowDialog();
        }

        private void MenuItem_OS_Information_Click(object sender, RoutedEventArgs e)
        {
            Cip.CipOsVersion osVersion = new CipOsVersion();
            string osInfoString = osVersion.GetInfoString();
            System.Windows.MessageBox.Show(
                osInfoString, 
                "Operation System info", 
                System.Windows.MessageBoxButton.OK, 
                System.Windows.MessageBoxImage.Information);
        }

        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        { 
            _sourceBitmap = Cip.CipTools.OpenImage();
            _currentBitmap = _sourceBitmap;
            if (_sourceBitmap != null)
            {
                _pictureBoxOriginal.Image = _sourceBitmap;
                _pictureBoxModifyed.Image = _sourceBitmap;
                _sourceRaster = new Raster(_sourceBitmap);
                this._undoList.ClearCollection();
                this._undoList.Add(this._sourceRaster);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._pictureBoxOriginal = (PictureBox)this.windowsFormsHost1.Child;
            this._pictureBoxModifyed = (PictureBox)this.windowsFormsHost2.Child;
            this._pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            this._pictureBoxModifyed.SizeMode = PictureBoxSizeMode.Zoom;
            this._sourceBitmap = this._currentBitmap = global::Cip.WpfDemo.Properties.Resources.Image_bandon;
            this._pictureBoxOriginal.Image = _currentBitmap;
            this._pictureBoxModifyed.Image = _currentBitmap;
            this._sourceRaster = new Raster(this._sourceBitmap);
            this._undoList = new CipUndoList<Raster>();
            this._undoList.Add(_sourceRaster);
            CipCheckEditMenu();
            //this.editDefaultItem.AddHandler

            this._bNeedUpdate = false;
            this._mutex = new Mutex();
            this._timer = new System.Threading.Timer(new TimerCallback(this.CipTimerCheckProc));
            this._timer.Change(0, 1000);
            this._timerWinForms = new System.Windows.Forms.Timer();
            this._timerWinForms.Tick += new EventHandler(TimerWinForms_Tick);
            this._timerWinForms.Interval = 1000;
            this._timerWinForms.Start();

            this.MakeProgressBar(sender, e);

            //Windows7 gadgets initialization
            if (_osVersion.GetOsTypeId() == CipOsType.Windows61)
                CipWin7SuperBarInitialization();
 
        }
        /// <summary>
        /// Windows 7 SuperBar initialization proc.
        /// </summary>
        void CipWin7SuperBarInitialization()
        {
            _jumpListManager = Cip.WpfDemo.Tools.CipWindows7WpfHelper.CreateJumpListManager(this);
            _jumpListManager.UserRemovedItems += (o, events) =>
            {
                events.CancelCurrentOperation = false;
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
            string cip20Location = System.IO.Path.Combine(
                System.Windows.Application.Current.StartupUri.AbsolutePath,
                "ColourImageProcessing.exe.");
            _jumpListManager.AddUserTask(new ShellLink
            {
                Path = cip20Location,
                Title = "Colour image processing .NET 2.0",
                Category = "Application",
                IconLocation = cip20Location,
                IconIndex = 0
            });

            _jumpListManager.Refresh();

            //Icon construction
            Uri iconUri = new Uri("./../../cip_icon.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            if (_thumbButtonManager == null)
            {
                _thumbButtonManager = Cip.WpfDemo.Tools.CipWindows7WpfHelper.CreateThumbButtonManager(this);
            }
            _thumbButton = _thumbButtonManager.CreateThumbButton(
                1,
                Cip.WpfDemo.Properties.Resources.cip_icon,
                "Cip button");
            _thumbButton.Clicked += new EventHandler(_thumbButton_Clicked);
            _thumbButtonManager.AddThumbButtons(_thumbButton);
            //_thumbButtonManager.DispatchMessage
        }

        void _thumbButton_Clicked(object sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("Thumb button test...");
        }
        private void TimerWinForms_Tick(object sender, EventArgs e)
        {
            this.CipCheckEditMenu();
        }
        private void MakeProgressBar(object sender, RoutedEventArgs e)
        {
            this.statusBar.Items.Clear();
            System.Windows.Controls.Label lbl = new System.Windows.Controls.Label();
            lbl.Background = new LinearGradientBrush(Colors.White, Colors.SlateBlue, 90);
            lbl.Content = "Progress of the operation:";
            this.statusBar.Items.Add(lbl);
            //<Snippet1>
            System.Windows.Controls.ProgressBar progbar = new System.Windows.Controls.ProgressBar();
            progbar.IsIndeterminate = false;
            progbar.Orientation = System.Windows.Controls.Orientation.Horizontal;
            progbar.Width = 150;
            progbar.Height = 15;
            //System.Windows.Duration duration = new Duration(TimeSpan.FromSeconds(10));
            //System.Windows.Media.Animation.DoubleAnimation doubleanimation = new System.Windows.Media.Animation.DoubleAnimation(100.0, duration);
            //progbar.BeginAnimation(System.Windows.Controls.ProgressBar.ValueProperty, doubleanimation);
            //</Snippet1>
            this.statusBar.Items.Add(progbar);
        }

        private void MenuItem_GrayScale_Click(object sender, RoutedEventArgs e)
        {
            Thread thGrayScale = new Thread(new ThreadStart(this.CipGrayScaleProc));
            thGrayScale.Priority = ThreadPriority.Normal;
            thGrayScale.Start();
        }
        private void CipTimerCheckProc(object stateInfo)
        {
            this.CipUpdateEditorState();
            //this.CipCheckEditMenu();
        }
        private void CipGrayScaleProc()
        {
            this._mutex.WaitOne();

            Cip.Filters.GrayScale filterGray = new GrayScale();
            Raster result = filterGray.ProcessWithoutWorker(this._undoList.GetCurrent());
            this._undoList.Add(result);
            this._currentBitmap = result.ToBitmap();
            this._bNeedUpdate = true;

            this._mutex.ReleaseMutex();
        }
        private void CipNegativeProc()
        {
            this._mutex.WaitOne();

            Cip.Filters.ColorAddition filterNegative = new ColorAddition();
            Raster result = filterNegative.ProcessWithoutWorker(this._undoList.GetCurrent());
            this._undoList.Add(result);
            this._currentBitmap = result.ToBitmap();
            this._bNeedUpdate = true;

            this._mutex.ReleaseMutex();
        }

        private void MenuItem_Negative_Click(object sender, RoutedEventArgs e)
        {
            Thread thNegative = new Thread(new ThreadStart(this.CipNegativeProc));
            thNegative.Priority = ThreadPriority.Normal;
            thNegative.Start();
        }
        private void CipUpdateEditorState()
        {
            if (this._bNeedUpdate)
            {
                // InvokeRequired required compares the thread ID of the
			    // calling thread to the thread ID of the creating thread.
			    // If these threads are different, it returns true.
                if (this._pictureBoxModifyed.InvokeRequired)
                {
                    SetPictureCallback d = new SetPictureCallback(CipUpdateEditorState);
                    this._pictureBoxModifyed.Invoke(d);
                }
                else
                { 
                    this._pictureBoxModifyed.Image = _currentBitmap;
                }
            }
        }
        private void CipCheckEditMenu()
        {
            if (_undoList.Count >= 2)
                this.editDefaultItem.IsEnabled = true;
            else
                this.editDefaultItem.IsEnabled = false;
            if (_undoList.Index == 0)
                this.editUndoItem.IsEnabled = false;
            else
                this.editUndoItem.IsEnabled = true;
            if ((_undoList.Index + 1) < _undoList.Count)
                editRedoItem.IsEnabled = true;
            else
                editRedoItem.IsEnabled = false;
        }

        private void MenuItem_Undo_Click(object sender, RoutedEventArgs e)
        {
            Raster result = _undoList.Undo();
            if (result != null)
            {
                _currentBitmap = result.ToBitmap();
                CipUpdateEditorState();
            }
            CipCheckEditMenu();
        }

        private void MenuItem_Redo_Click(object sender, RoutedEventArgs e)
        {
            Raster result = _undoList.Redo();
            if (result != null)
            {
                _currentBitmap = result.ToBitmap();
                CipUpdateEditorState();
            }
            CipCheckEditMenu();
        }


        private void MenuItem_Default_Click(object sender, RoutedEventArgs e)
        {
            Raster result = _undoList.Default();
            _currentBitmap = result.ToBitmap();
            CipUpdateEditorState();
            CipCheckEditMenu();
        }

        
    }
}
