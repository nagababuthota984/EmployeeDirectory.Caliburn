using EmployeeDirectory.Caliburn.Data;
using System.Windows;

namespace EmployeeDirectory.Caliburn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            JsonHelper.InitGeneralFiltersData();
            JsonHelper.InitEmployeeData();
        }
    }
}
