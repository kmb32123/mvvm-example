using System.Windows.Controls;
using ScreenCaptureMVVM.View;

namespace ScreenCaptureMVVM.ViewModel
{
    public class FinishedViewModel : StateViewModel
    {
        public override UserControl NextView
        {
            get { return new ScreenSelectControl(); }
        }

        public override UserControl BackView
        {
            get { return new DescriptionControl(); }
        }

        public override bool NextEnabled
        {
            get { return true; }
        }

        public override bool BackEnabled
        {
            get { return true; }
        }

        public override string NextMessage
        {
            get
            {
                return "RECAPTURE";
            }
        }

        public override string BackMessage
        {
            get { return "EXIT"; }
        }
    }
}
