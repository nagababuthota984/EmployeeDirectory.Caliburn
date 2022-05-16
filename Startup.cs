using Caliburn.Micro;
using EmployeeDirectory.Caliburn.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EmployeeDirectory.Caliburn
{
    public class Startup : BootstrapperBase
    {
        private  SimpleContainer _container = new SimpleContainer();
        public Startup()
        {
            Initialize(); 
        }

        protected override void Configure()
        {
            _container.Instance(_container);
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.PerRequest<AddEditEmployeeViewModel>();
            _container.PerRequest<HomeViewModel>();
            _container.PerRequest<MainViewModel>();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);
            DisplayRootViewFor<MainViewModel>();
        }
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);   
        }
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
