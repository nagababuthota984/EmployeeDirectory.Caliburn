using Caliburn.Micro;
using EmployeeDirectory.Caliburn.ViewModels;
using System.Windows;

namespace EmployeeDirectory.Caliburn
{
    public class Startup : BootstrapperBase
    {
        public Startup()
        {
            Initialize(); 
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
