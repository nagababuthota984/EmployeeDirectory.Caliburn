using Caliburn.Micro;
using EmployeeDirectory.Caliburn.Data;
using EmployeeDirectory.Caliburn.Models;
using EmployeeDirectory.Caliburn.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static EmployeeDirectory.Caliburn.Models.Enums;

namespace EmployeeDirectory.Caliburn.ViewModels
{
    public class HomeViewModel : Screen, IHandle<Employee>
    {
        #region Fields
        private string _searchInput;
        private string _filterInput;
        private string _viewMoreBtnContent;
        private ObservableCollection<GeneralFilter> _departments;
        private ObservableCollection<GeneralFilter> _jobtitles;
        private GeneralFilter _selectedDept;
        private GeneralFilter _selectedJobTitle;
        private ObservableCollection<Employee> _filteredEmployees;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<GeneralFilter> _visibleJobTitles;
        private AddEditEmployeeViewModel _addEditEmpVM;
        #endregion
        #region Properties

        public string SearchInput
        {
            get { return _searchInput; }
            set 
            { 
                _searchInput = value; 
                FilteredEmployees = FilterService.FilterEmployeesBySearch(value,FilterInput); 
                NotifyOfPropertyChange(nameof(SearchInput)); 
            }
        }
        public string FilterInput
        {
            get { return _filterInput; }
            set 
            { 
                _filterInput = value; 
                FilteredEmployees = FilterService.FilterEmployeesBySearch(SearchInput,value); 
                NotifyOfPropertyChange(nameof(FilterInput)); 
            }
        }
        public string[] FilterCategories { get; set; }
        public GeneralFilter SelectedJobTitle
        {
            get { return _selectedJobTitle; }
            set 
            { 
                _selectedJobTitle = value; 
                if(value!=null)
                    FilteredEmployees = FilterService.FilterEmployeesByJobTitle(value); 
                NotifyOfPropertyChange(nameof(SelectedJobTitle)); 
            }
        }
        public GeneralFilter SelectedDept
        {
            get { return _selectedDept; }
            set
            { 
                _selectedDept = value; 
                FilteredEmployees = FilterService.FilterEmployeesByDepartment(value); 
                NotifyOfPropertyChange(nameof(SelectedDept)); 
            }
        }
        public string ViewMoreBtnContent
        {
            get { return _viewMoreBtnContent; }
            set { _viewMoreBtnContent = value; NotifyOfPropertyChange(nameof(ViewMoreBtnContent)); }
        }
        public ObservableCollection<Employee> FilteredEmployees
        {
            get { return _filteredEmployees; }
            set { _filteredEmployees = value; NotifyOfPropertyChange(nameof(FilteredEmployees)); }
        }
        public ObservableCollection<GeneralFilter> Departments
        {
            get { return _departments; }
            set { _departments = value; NotifyOfPropertyChange(nameof(Departments)); }
        }
        public ObservableCollection<GeneralFilter> JobTitles
        {
            get { return _jobtitles; }
            set { _jobtitles = value; NotifyOfPropertyChange(nameof(JobTitles)); }
        }

        public ObservableCollection<GeneralFilter> VisibleJobTitles
        {
            get { return _visibleJobTitles; }
            set { _visibleJobTitles = value; NotifyOfPropertyChange(nameof(VisibleJobTitles)); }
        }


        #endregion
        public HomeViewModel(IEventAggregator eventAggregator, AddEditEmployeeViewModel addEditEmpVM)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
            _addEditEmpVM = addEditEmpVM;
            _filteredEmployees = new(EmployeeData.Employees);
            _departments = new BindableCollection<GeneralFilter>(EmployeeData.Departments);
            _viewMoreBtnContent = "view more";
            FilterCategories = Enum.GetNames(typeof(FilterCategories));
            _filterInput = FilterCategories[0];
            JobTitles = new(EmployeeData.JobTitles);
            VisibleJobTitles = new(JobTitles.Take(6));
        }
        public void ViewMore()
        {
            if (ViewMoreBtnContent.Equals("view more", StringComparison.OrdinalIgnoreCase))
            {
                VisibleJobTitles = JobTitles;
                ViewMoreBtnContent = "view less";
            }
            else
            {
                VisibleJobTitles = new(JobTitles.Take(6));
                ViewMoreBtnContent = "view more";
            }
        }
        public void AddEmployee()
        {
            _addEditEmpVM.OkBtnContent = "Add Employee";
            _addEditEmpVM.HeadingText = Common.MessageStrings.AddEmpHeading;
            _addEditEmpVM.SelectedEmployee = new();
            _eventAggregator.PublishOnUIThreadAsync(_addEditEmpVM);
            
        }
        public void EditEmployee(Employee selectedEmployee)
        {
            _addEditEmpVM.OkBtnContent = "Update";
            _addEditEmpVM.SelectedEmployee = selectedEmployee;
            _addEditEmpVM.HeadingText = Common.MessageStrings.EditEmpHeading;
            _eventAggregator.PublishOnUIThreadAsync(_addEditEmpVM);
        }
        public void DeleteEmployee(Employee selectedEmployee)
        {
            if (MessageBoxResult.Yes == MessageBox.Show($"{Common.MessageStrings.ConfirmDelete} {selectedEmployee.PreferredName}?", "Delete Employee", MessageBoxButton.YesNo))
                FilteredEmployees.Remove(selectedEmployee);
        }
        public Task HandleAsync(Employee employee, CancellationToken cancellationToken)
        {
            if (EmployeeData.Employees.Any(emp=> emp.Id==employee.Id))
                UpdateEmployee(employee); 
            else
                AddNewEmployee(employee);
            FilteredEmployees = new(EmployeeData.Employees);
            JobTitles = new(EmployeeData.JobTitles);
            return null;
        }
        private static void AddNewEmployee(Employee employee)
        {
            try
            {
                AddJobTitle(employee.JobTitle);
                AddDepartment(employee.Department);
                EmployeeData.Employees.Add(employee);
                SyncJson<GeneralFilter>();
                SyncJson<Employee>();
                MessageBox.Show(Common.MessageStrings.EmployeeAdded, Common.MessageStrings.EmpAddedWinTitle);
            }
            catch (Exception e)
            {
                MessageBox.Show($"{Common.MessageStrings.EmpAddError} {e.Message}", Common.MessageStrings.EmpAddErrorWinTitle);
            }
        }
        private static void UpdateEmployee(Employee employee)
        {
            try
            {
                EmployeeData.Employees.Remove(EmployeeData.Employees.FirstOrDefault(emp => emp.Id.Equals(employee.Id, StringComparison.OrdinalIgnoreCase)));
                EmployeeData.Employees.Add(employee);
                SyncJson<Employee>();
                MessageBox.Show(Common.MessageStrings.EmpUpdateSuccess, Common.MessageStrings.EmpUpdateSuccessWinTitle);
            }
            catch (Exception e)
            {
                MessageBox.Show($"{Common.MessageStrings.EmpUpdateError} {e.Message}", Common.MessageStrings.EmpUpdateErrorWinTitle);
            }
        }
        private static void AddJobTitle(string jobTitle)
        {
            if (!string.IsNullOrEmpty(jobTitle))
            {
                GeneralFilter job = EmployeeData.JobTitles.FirstOrDefault(jt => jt.Category == GeneralFilterCategories.JobTitle && jt.Name.Equals(jobTitle, StringComparison.OrdinalIgnoreCase));
                if (job != null)
                    job.Count += 1;
                else
                    EmployeeData.JobTitles.Add(new GeneralFilter() { Name = jobTitle, Count = 1, Category = GeneralFilterCategories.JobTitle });
            }
        }
        private static void AddDepartment(string department)
        {
            if (!string.IsNullOrEmpty(department))
            {
                GeneralFilter dept = EmployeeData.Departments.FirstOrDefault(jt => jt.Category == GeneralFilterCategories.Department && jt.Name.Equals(department, StringComparison.OrdinalIgnoreCase));
                if (dept != null)
                    dept.Count += 1;
                else
                    EmployeeData.JobTitles.Add(new GeneralFilter() { Name = department, Count = 1, Category = GeneralFilterCategories.Department });
            }
        }
        private static void SyncJson<T>()
        {
            JsonHelper.WriteToJson<T>();
        }
    }
}
