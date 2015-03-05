using System.Windows.Controls;
using GalaSoft.MvvmLight;

namespace ScreenCaptureMVVM.ViewModel
{
    public abstract class StateViewModel : ViewModelBase
    {
        public abstract UserControl NextView { get; }
        public abstract UserControl BackView { get; }
        public abstract bool NextEnabled { get; }
        public abstract bool BackEnabled { get; }
        public abstract string NextMessage { get; }
        public abstract string BackMessage { get; }
    }
}
