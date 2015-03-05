using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using ScreenCaptureMVVM.Exceptions;

namespace ScreenCaptureMVVM.Model
{
    internal class CaptureImage
    {
        private static readonly object Locker = new object();
        public Func<CaptureImage, string>[] FileNameArgs;

        public CaptureImage(string baseName, ImageFormat imageFormat, ScreenCapturer screenCapturer,
                            Func<CaptureImage, string>[] funcs)
        {
            BaseName = baseName;
            ImageFormat = imageFormat;
            ScreenCapturer = screenCapturer;
            FileNameArgs = funcs;
        }

        public static int NumImages { get; private set; }

        public ScreenCapturer ScreenCapturer { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public string BaseName { get; set; }

        public FileInfo FileName
        {
            get
            {
                string basename = FileNameArgs.Where(arg => arg != null).Aggregate(BaseName,
                                                                                   (current, arg) =>
                                                                                   current + (" - " + arg.Invoke(this)));
                string convertToString = new ImageFormatConverter().ConvertToString(ImageFormat);
                if (convertToString != null)
                    return new FileInfo(basename + '.' + convertToString.ToLower(CultureInfo.InvariantCulture));
                throw new FileFormatException("Selected format is invalid!");
            }
        }

        public FileInfo Save()
        {
            lock (Locker)
            {
                NumImages++;
                var name = FileName;
                try
                {
                    ScreenCapturer.Image.Save(name.FullName, ImageFormat);
                    return name;
                }
                catch (Exception ex)
                {
                    throw new ScreenCaptureException(String.Format(CultureInfo.CurrentCulture, "Failed to save '{0}'!", name.Name), ex);
                }
            }
        }
    }
}