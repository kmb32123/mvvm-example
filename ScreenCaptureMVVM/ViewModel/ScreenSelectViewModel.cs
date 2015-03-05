using System.Collections.ObjectModel;
using System.Windows.Controls;
using ScreenCaptureMVVM.View;
using Screen = System.Windows.Forms.Screen;

namespace ScreenCaptureMVVM.ViewModel
{
    public class ScreenSelectViewModel : StateViewModel
    {
        public static ObservableCollection<Screen> Screens
        {
            get { return new ObservableCollection<Screen>(Screen.AllScreens); }
        }

        private Screen _selectedScreen;
        public Screen SelectedScreen
        {
            get { return _selectedScreen; }
            set
            {
                _selectedScreen = value;
                RaisePropertyChanged("SelectedScreen");
            }
        }

        public override UserControl NextView
        {
            get
            {
                var v = new AreaSelectControl();
                var viewModel = ((AreaSelectViewModel)v.DataContext);
                viewModel.Screen = SelectedScreen;
                return v;
            }
        }

        public override UserControl BackView
        {
            get { return new DescriptionControl(); }
        }

        public override bool NextEnabled
        {
            get { return SelectedScreen != null; }
        }

        public override bool BackEnabled
        {
            get { return true; }
        }

        public override string NextMessage
        {
            get { return "NEXT"; }
        }

        public override string BackMessage
        {
            get { return "CANCEL"; }
        }
    }
}
