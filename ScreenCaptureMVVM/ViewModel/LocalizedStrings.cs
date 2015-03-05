using System.Windows;
using GalaSoft.MvvmLight;
using ScreenCaptureMVVM.Properties.Strings;

namespace ScreenCaptureMVVM.ViewModel
{
    public class LocalizedStrings : ViewModelBase
    {
        public static LocalizedStrings Resources
        {
            get { return Application.Current.Resources["LocalizedStrings"] as LocalizedStrings; }
        }
        
        public strings LocalizedResources
        {
            get { return new strings(); }
        }

        public void UpdateStrings()
        {
            RaisePropertyChanged(() => LocalizedResources);
        }
    }
}