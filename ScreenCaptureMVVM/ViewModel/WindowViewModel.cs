using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ScreenCaptureMVVM.Properties.Strings;
using ScreenCaptureMVVM.View;

namespace ScreenCaptureMVVM.ViewModel
{
    public class WindowViewModel : ViewModelBase
    {
        public WindowViewModel()
        {
            _currentStateContainer = new StateContainer();
            CurrentContainerViewModel.PropertyChanged += CurrentStateViewModelOnPropertyChanged;
        }

        private UserControl _currentStateContainer;
        public UserControl CurrentStateContainer
        {
            get { return _currentStateContainer; }
            set
            {
                CurrentContainerViewModel.PropertyChanged -= CurrentStateViewModelOnPropertyChanged;
                _currentStateContainer = value;
                CurrentContainerViewModel.PropertyChanged += CurrentStateViewModelOnPropertyChanged;
                CurrentStateViewModelOnPropertyChanged(null, null);
            }
        }

        public static RelayCommand<CultureInfo> ChangeLanguageCommand
        {
            get { return new RelayCommand<CultureInfo>(ChangeLanguage); }
        }

        public static void ChangeLanguage(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = culture;
            LocalizedStrings.Resources.UpdateStrings();
        }


        // I realy am sorry for the length of this line...
        private static readonly ReadOnlyObservableCollection<CultureInfo> _availableCultures
            = new ReadOnlyObservableCollection<CultureInfo>(
              new ObservableCollection<CultureInfo>(
                  CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .Where(cultureInfo =>  new ResourceManager(typeof(strings)).GetResourceSet(cultureInfo, true, false) != null)));

        public static ReadOnlyObservableCollection<CultureInfo> AvailableCultures
        {
            get
            {
                return _availableCultures;
            }
        }

        private void CurrentStateViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            //TODO: Currently we dont care what happens in the state container so we do nothing.
        }

        public ContainerViewModel CurrentContainerViewModel
        {
            get { return _currentStateContainer.DataContext as ContainerViewModel; }
        }
    }
}
