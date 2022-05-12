using EmployeeDirectory.Caliburn.Models;
using System.Collections.Generic;

namespace EmployeeDirectory.Caliburn.Data
{
    public class EmployeeData
    {
        public static List<Employee> Employees = new();
        public static List<GeneralFilter> Departments = new();
        public static List<GeneralFilter> JobTitles = new();
    }
}
