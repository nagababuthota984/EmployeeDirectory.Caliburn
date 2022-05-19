using Caliburn.Micro;
using EmployeeDirectory.Caliburn.Data;
using EmployeeDirectory.Caliburn.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmployeeDirectory.Caliburn.ViewModels
{
    public class AddEditEmployeeViewModel : Screen
    {
        #region Fields
        private Employee _employee;
        private string _headingText;
        private string _okBtnContent;
        private IEventAggregator _eventAggregator;
        #endregion
        #region Properties
        public Employee SelectedEmployee
        {
            get { return _employee; }
            set { _employee = value; NotifyOfPropertyChange(nameof(SelectedEmployee)); }
        }
        public string HeadingText
        {
            get { return _headingText; }
            set { _headingText = value; NotifyOfPropertyChange(nameof(HeadingText)); }
        }
        public string OkBtnContent
        {
            get { return _okBtnContent; }
            set { _okBtnContent = value; NotifyOfPropertyChange(nameof(OkBtnContent)); }
        }
        #endregion

        public AddEditEmployeeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        public void Submit()
        {
            _eventAggregator.PublishOnUIThreadAsync(SelectedEmployee);
            OnCancel();
        }
        public void OnCancel()
        {
            _eventAggregator.PublishOnUIThreadAsync("back");
        }
    }
}
