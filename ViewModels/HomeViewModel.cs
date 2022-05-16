using Caliburn.Micro;
using EmployeeDirectory.Caliburn.Data;
using EmployeeDirectory.Caliburn.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using static EmployeeDirectory.Caliburn.Models.Enums;

namespace EmployeeDirectory.Caliburn.ViewModels
{
    public class HomeViewModel : Screen
    {
        #region Fields
        private string _searchInput;
        private string _filterInput;
        private string _viewMoreBtnContent;
        private BindableCollection<Employee> _employees;
        private BindableCollection<GeneralFilter> _departments;
        private BindableCollection<GeneralFilter> _jobtitles;
        private Employee _selectedEmployee;
        private GeneralFilter _selectedDept;
        private GeneralFilter _selectedJobTitle;
        private BindableCollection<Employee> _filteredEmployees;
        private readonly IEventAggregator _eventAggregator;
        private AddEditEmployeeViewModel _addEditEmpVM;
        #endregion
        #region Properties

        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; FilterEmployeesBySearch(value); NotifyOfPropertyChange(nameof(SearchInput)); }
        }
        public string FilterInput
        {
            get { return _filterInput; }
            set { _filterInput = value; FilterEmployeesBySearch(SearchInput); NotifyOfPropertyChange(nameof(FilterInput)); }
        }
        public string[] FilterCategories { get; set; }
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; NotifyOfPropertyChange(nameof(SelectedEmployee)); }
        }
        public GeneralFilter SelectedJobTitle
        {
            get { return _selectedJobTitle; }
            set { _selectedJobTitle = value; FilterEmployeesByJobTitle(value); NotifyOfPropertyChange(nameof(SelectedJobTitle)); }
        }
        public GeneralFilter SelectedDept
        {
            get { return _selectedDept; }
            set { _selectedDept = value; FilterEmployeesByDepartment(value); NotifyOfPropertyChange(nameof(SelectedDept)); }
        }
        public string ViewMoreBtnContent
        {
            get { return _viewMoreBtnContent; }
            set { _viewMoreBtnContent = value; NotifyOfPropertyChange(nameof(ViewMoreBtnContent)); }
        }
        public BindableCollection<Employee> FilteredEmployees
        {
            get { return _filteredEmployees; }
            set { _filteredEmployees = value; NotifyOfPropertyChange(nameof(FilteredEmployees)); }
        }
        public BindableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; NotifyOfPropertyChange(nameof(Employees)); }
        }
        public BindableCollection<GeneralFilter> Departments
        {
            get { return _departments; }
            set { _departments = value; NotifyOfPropertyChange(nameof(Departments)); }
        }
        public BindableCollection<GeneralFilter> JobTitles
        {
            get { return _jobtitles; }
            set { _jobtitles = value; NotifyOfPropertyChange(nameof(JobTitles)); }
        }

        #endregion
        public HomeViewModel(IEventAggregator eventAggregator, AddEditEmployeeViewModel addEditEmpVM)
        {
            _eventAggregator = eventAggregator;
            _addEditEmpVM = addEditEmpVM;
            Employees = new BindableCollection<Employee>(EmployeeData.Employees);
            FilteredEmployees = Employees;
            Departments = new BindableCollection<GeneralFilter>(EmployeeData.Departments);
            ViewMoreBtnContent = "view more";
            FilterCategories = Enum.GetNames(typeof(FilterCategories));
            FilterInput = FilterCategories[0];
            LoadJobTitles();
        }

        private void LoadJobTitles()
        {
            if (EmployeeData.JobTitles.Count >= 6)
                EmployeeData.JobTitles.Take(6).ToList().ForEach(job => job.IsVisible = true);
            JobTitles = new(EmployeeData.JobTitles);
            
        }

        public void ViewMore()
        {
            if (ViewMoreBtnContent.Equals("view more", StringComparison.OrdinalIgnoreCase))
            {
                JobTitles.Skip(6).ToList().ForEach(job => job.IsVisible = true) ;
                ViewMoreBtnContent = "view less";
            }
            else
            {
                JobTitles.Skip(6).ToList().ForEach(job => job.IsVisible = false);
                ViewMoreBtnContent = "view more";
            }
        }

        public void AddEmployee()
        {
            _eventAggregator.PublishOnUIThreadAsync(_addEditEmpVM);
        }
        private void OnEditEmp()
        {

           
        }
        public void Delete()
        {

            if (MessageBoxResult.Yes == MessageBox.Show($"Are you sure you want to delete {SelectedEmployee.PreferredName}?", "Delete Employee", MessageBoxButton.YesNo))
                Employees.Remove(SelectedEmployee);
        }

        private void FilterEmployeesByJobTitle(GeneralFilter value)
        {
            FilteredEmployees = new BindableCollection<Employee>(Employees.Where(emp => emp.JobTitle.Equals(value.Name, StringComparison.OrdinalIgnoreCase)).ToList());
        }
        private void FilterEmployeesByDepartment(GeneralFilter value)
        {
            FilteredEmployees = new BindableCollection<Employee>(Employees.Where(emp => emp.Department.Equals(value.Name, StringComparison.OrdinalIgnoreCase)).ToList());

        }
        private void FilterEmployeesBySearch(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                FilteredEmployees = new BindableCollection<Employee>(Employees.Where(emp =>
                                             (FilterInput.Equals("Name") && emp.PreferredName.Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (FilterInput.Equals("ContactNumber") && emp.ContactNumber.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (FilterInput.Equals("Salary") && emp.Salary.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (FilterInput.Equals("Experience") && emp.ExperienceInYears.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))).ToList());
            else
                FilteredEmployees = Employees;
        }
    }
}
