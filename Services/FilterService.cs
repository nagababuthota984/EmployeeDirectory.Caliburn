using Caliburn.Micro;
using EmployeeDirectory.Caliburn.Data;
using EmployeeDirectory.Caliburn.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Caliburn.Services
{
    public class FilterService
    {
        public static ObservableCollection<Employee> FilterEmployeesByJobTitle(GeneralFilter value)
        {
            return new ObservableCollection<Employee>(EmployeeData.Employees.Where(emp => emp.JobTitle.Equals(value.Name, StringComparison.OrdinalIgnoreCase)).ToList());
        }
        public static ObservableCollection<Employee> FilterEmployeesByDepartment(GeneralFilter value)
        {
            return new ObservableCollection<Employee>(EmployeeData.Employees.Where(emp => emp.Department.Equals(value.Name, StringComparison.OrdinalIgnoreCase)).ToList());

        }
        public static ObservableCollection<Employee> FilterEmployeesBySearch(string value, string filterInput)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return new ObservableCollection<Employee>(EmployeeData.Employees.Where(emp =>
                                             (filterInput.Equals("Name") && emp.PreferredName.Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (filterInput.Equals("ContactNumber") && emp.ContactNumber.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (filterInput.Equals("Salary") && emp.Salary.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (filterInput.Equals("Experience") && emp.ExperienceInYears.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))).ToList());
            else
                return new(EmployeeData.Employees);
        }
    }
}
