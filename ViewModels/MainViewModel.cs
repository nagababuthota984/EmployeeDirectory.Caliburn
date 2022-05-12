using Caliburn.Micro;

namespace EmployeeDirectory.Caliburn.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        #region Fields
        private object _currentViewModel;
        #endregion
        #region Properties
        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; NotifyOfPropertyChange(nameof(CurrentViewModel)); }
        }
        #endregion
        public MainViewModel()
        {
            ActivateItemAsync(new HomeViewModel());
        }
    }
}
