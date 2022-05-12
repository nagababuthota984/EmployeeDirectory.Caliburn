using EmployeeDirectory.Caliburn.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static EmployeeDirectory.Caliburn.Models.Enums;

namespace EmployeeDirectory.Caliburn.Data
{
    public class JsonHelper
    {
        private static readonly string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static void WriteToJson<T>()
        {
            if (typeof(T) == typeof(Employee))
                File.WriteAllText($"{projectDirectory}\\Data\\Employee.json",
                                            JsonConvert.SerializeObject(EmployeeData.Employees, Formatting.Indented));
            else if (typeof(T) == typeof(GeneralFilter))
            {
                string deptData = JsonConvert.SerializeObject(EmployeeData.Departments, Formatting.Indented);
                string jobData = JsonConvert.SerializeObject(EmployeeData.JobTitles, Formatting.Indented);
                File.WriteAllText($"{projectDirectory}\\Data\\GeneralFilters.json",
                                                $"{deptData[0..^1]},{jobData[1..]}");
            }
        }
        public static string ReadFromJson(string typeOfData)
        {
            return File.ReadAllText($"{projectDirectory}\\Data\\{typeOfData}.json");
        }
        public static void InitEmployeeData()
        {
            string data = ReadFromJson("Employee");
            if (!string.IsNullOrWhiteSpace(data))
                EmployeeData.Employees = JsonConvert.DeserializeObject<List<Employee>>(data).Where(emp => emp.Status == Status.Existing).ToList();
        }
        public static void InitGeneralFiltersData()
        {
            string data = ReadFromJson("GeneralFilters");
            if (!string.IsNullOrWhiteSpace(data))
            {
                EmployeeData.JobTitles = JsonConvert.DeserializeObject<List<GeneralFilter>>(data).Where(filter => filter.Category == GeneralFilterCategories.JobTitle).ToList();
                EmployeeData.Departments = JsonConvert.DeserializeObject<List<GeneralFilter>>(data).Where(filter => filter.Category == GeneralFilterCategories.Department).ToList();
            }
        }
    }
}
