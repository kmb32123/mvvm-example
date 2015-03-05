namespace ScreenCaptureMVVM.Model
{
    using System.Drawing;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;
    using Application = System.Windows.Application;

    public abstract class ScreenCapturer
    {
        protected Screen CaptureScreen { get; private set; }

        protected ScreenCapturer(Screen captureScreen)
        {
            CaptureScreen = captureScreen;
        }

        public Bitmap Image
        {
            get
            {
                Bitmap retval;
                using (var memoryImage = new Bitmap(ImageRectangle.Width, ImageRectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    using (var memoryGrahics = Graphics.FromImage(memoryImage))
                        memoryGrahics.CopyFromScreen(ImageRectangle.X, ImageRectangle.Y, 0, 0, ImageRectangle.Size, CopyPixelOperation.SourceCopy);
                    retval = memoryImage.Clone(new Rectangle(0, 0, memoryImage.Width, memoryImage.Height), memoryImage.PixelFormat);
                }
                return retval;
            }
        }

        public abstract Rectangle ImageRectangle { get; }
    }

    public class BoundsScreenCapturer : ScreenCapturer
    {
        public BoundsScreenCapturer(Screen captureScreen) : base(captureScreen) { }

        public override Rectangle ImageRectangle
        {
            get { return CaptureScreen.Bounds; }
        }
    }

    public class WindowsScreenCapturer : ScreenCapturer
    {
        public WindowsScreenCapturer(Screen captureScreen) : base(captureScreen) { }

        public override Rectangle ImageRectangle
        {
            get
            {
                return Application.Current.Windows.OfType<Window>().Single(w => w.IsActive).RestoreBounds.ToRectangle();
            }
        }
    }

    public class WorkingAreaScreenCapturer : ScreenCapturer
    {
        public WorkingAreaScreenCapturer(Screen captureScreen) : base(captureScreen) { }

        public override Rectangle ImageRectangle
        {
            get { return CaptureScreen.WorkingArea; }
        }
    }

    public static class RectangleConversions
    {
        public static Rectangle ToRectangle(this Rect value)
        {
            return new Rectangle((int) value.X, (int) value.Y, (int) value.Width, (int) value.Height);
        }
    }
}
