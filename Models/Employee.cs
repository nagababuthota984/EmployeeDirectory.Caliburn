using EmployeeDirectory.Caliburn.Data;
using System;
using System.Linq;
using static EmployeeDirectory.Caliburn.Models.Enums;

namespace EmployeeDirectory.Caliburn.Models
{
    public class Employee
    {
        private static readonly Random _random = new();
        #region Properties
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public int ExperienceInYears { get; set; }
        public long ContactNumber { get; set; }
        public decimal Salary { get; set; }
        public EmployementType EmploymentType { get; set; }
        #endregion Properties
        #region Constructors
        public Employee()
        {
            Dob = DateTime.Today;
        }
        public Employee(string firstName, string lastName, string email, DateTime dob, string jobtitle, string department, int experienceInYears, long phoneNumber, decimal salary, EmployementType empType)
        {
            Id = GenerateId();
            FirstName = firstName;
            LastName = lastName;
            PreferredName = $"{firstName} {lastName}";
            Email = email;
            Dob = dob;
            JobTitle = jobtitle;
            Department = department;
            ExperienceInYears = experienceInYears;
            Salary = salary;
            ContactNumber = phoneNumber;
            EmploymentType = empType;
            Status = Status.Existing;
        }
        public Employee(Employee employee) :
            this(employee.FirstName, employee.LastName, employee.Email, employee.Dob, employee.JobTitle, employee.Department, employee.ExperienceInYears, employee.ContactNumber, employee.Salary, employee.EmploymentType)
        {
        } 
        #endregion
        private static string GenerateId()
        {
            string id;
            do
            {
                id = _random.Next(0, 99999).ToString("D4");
            } while (EmployeeData.Employees.Any(emp => emp.Id.Equals(id)));
            return id;
        }

    }
}
