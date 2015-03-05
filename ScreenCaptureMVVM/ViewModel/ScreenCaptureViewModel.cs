using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ScreenCaptureMVVM.Exceptions;
using ScreenCaptureMVVM.Model;
using ScreenCaptureMVVM.View;
using UserControl = System.Windows.Controls.UserControl;

namespace ScreenCaptureMVVM.ViewModel
{
    public class ScreenCaptureViewModel : StateViewModel
    {
        public ScreenCaptureViewModel()
        {
            _image = new CaptureImage("Capture", ImageFormat.Png, new BoundsScreenCapturer(Screen.PrimaryScreen), new Func<CaptureImage, string>[4]);
            AppendSerialize = true;
            _statusMessage = "Press the insert key or capture to save a screen capture";
        }

        public ScreenCapturer ScreenCapturer
        {
            get { return _image.ScreenCapturer; }
            set { _image.ScreenCapturer = value; }
        }

        private readonly CaptureImage _image;

        private string _statusMessage;

        public string BaseName
        {
            get { return _image.BaseName; }
            set
            {
                _image.BaseName = value;
                RaisePropertyChanged("BaseName");
            }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                RaisePropertyChanged("StatusMessage");
            }
        }

        public ICommand CaptureScreenCommand
        {
            get { return new RelayCommand(CaptureScreen); }
        }

        public ImageFormat CurrentImageFormat
        {
            get { return _image.ImageFormat; }
            set
            {
                _image.ImageFormat = value;
                RaisePropertyChanged("CurrentImageFormat");
            }
        }

        public bool AppendResolution
        {
            get { return _image.FileNameArgs[0] != null; }
            set { _image.FileNameArgs[0] = value ? (Func<CaptureImage, string>)(v => String.Format(CultureInfo.CurrentCulture, "{0}x{1}", v.ScreenCapturer.ImageRectangle.Width, v.ScreenCapturer.ImageRectangle.Height)) : null; }
        }

        public bool AppendSerialize
        {
            get { return _image.FileNameArgs[3] != null; }
            set { _image.FileNameArgs[3] = value ? (Func<CaptureImage, string>)(v => CaptureImage.NumImages.ToString("D3", CultureInfo.CurrentCulture)) : null; }
        }

        public bool AppendDate
        {
            get { return _image.FileNameArgs[1] != null; }
            set { _image.FileNameArgs[1] = value ? (Func<CaptureImage, string>)(v => DateTime.Now.ToString("MM-dd-yyyy", CultureInfo.CurrentCulture)) : null; }
        }

        public bool AppendTime
        {
            get { return _image.FileNameArgs[2] != null; }
            set { _image.FileNameArgs[2] = value ? (Func<CaptureImage, string>) (v => DateTime.Now.ToString("hh-mm-ss", CultureInfo.CurrentCulture)) : null; }
        }

        private void CaptureScreen()
        {
            try
            {
                var fileInfo = _image.Save();
                StatusMessage = String.Format(CultureInfo.CurrentCulture, "Succesfuly saved '{0}'!", fileInfo.Name);
            }
            catch (ScreenCaptureException ex)
            {
                StatusMessage = ex.Message;
            }
        }

        public override UserControl NextView
        {
            get { return new FinishedControl(); }
        }

        public override UserControl BackView
        {
            get { return new DescriptionControl(); }
        }

        public override bool NextEnabled
        {
            get { return CaptureImage.NumImages > 0; }
        }

        public override bool BackEnabled
        {
            get { return true; }
        }

        public override string NextMessage
        {
            get { return "DONE"; }
        }

        public override string BackMessage
        {
            get { return "CANCEL"; }
        }
    }
}
