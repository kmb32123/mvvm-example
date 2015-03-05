using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ScreenCaptureMVVM.View;

namespace ScreenCaptureMVVM.ViewModel
{
    public class ContainerViewModel : ViewModelBase
    {
        public ContainerViewModel()
        {
            _currentState = new DescriptionControl();
            CurrentStateViewModel.PropertyChanged += RefreshStateDependentProperties;
        }

        private UserControl _currentState;
        public UserControl CurrentState
        {
            get { return _currentState; }
            set
            {
                CurrentStateViewModel.PropertyChanged -= RefreshStateDependentProperties;
                _currentState = value;
                CurrentStateViewModel.PropertyChanged += RefreshStateDependentProperties;
                RefreshStateDependentProperties(null, null);
            }
        }

        private void RefreshStateDependentProperties(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged("CurrentState");
            RaisePropertyChanged("NextMessage");
            RaisePropertyChanged("BackMessage");
            RaisePropertyChanged("NextEnabled");
            RaisePropertyChanged("BackEnabled");
        }

        public StateViewModel CurrentStateViewModel
        {
            get { return _currentState.DataContext as StateViewModel; }
        }

        public ICommand NextCommand
        {
            get { return new RelayCommand(() => CurrentState = CurrentStateViewModel.NextView); }
        }

        public ICommand BackCommand
        {
            get { return new RelayCommand(() => CurrentState = CurrentStateViewModel.BackView); }
        }

        public string NextMessage
        {
            get { return CurrentStateViewModel.NextMessage; }
        }

        public string BackMessage
        {
            get { return CurrentStateViewModel.BackMessage; }
        }

        public bool NextEnabled
        {
            get { return CurrentStateViewModel.NextEnabled; }
        }

        public bool BackEnabled
        {
            get { return CurrentStateViewModel.BackEnabled; }
        }
    }
}
