using System.Windows.Controls;
using ScreenCaptureMVVM.View;

namespace ScreenCaptureMVVM.ViewModel
{
    public class DescriptionViewModel : StateViewModel
    {
        public override UserControl NextView
        {
            get { return new ScreenSelectControl(); }
        }

        public override UserControl BackView
        {
            get { return new FinishedControl(); }
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
            get { return "START"; }
        }

        public override string BackMessage
        {
            get { return "EXIT"; }
        }
    }
}
