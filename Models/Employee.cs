using EmployeeDirectory.Caliburn.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using static EmployeeDirectory.Caliburn.Models.Enums;

namespace EmployeeDirectory.Caliburn.Models
{
    public class Employee : IDataErrorInfo
    {
        private static readonly Random _random = new();
        #region Properties
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredName => $"{FirstName} {LastName}";
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
        public string Error
        {
            get { return string.Empty; }

        }
        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                switch (propertyName)
                {
                    case "FirstName":
                        if (string.IsNullOrWhiteSpace(FirstName))
                            error = "Firstname shouldn't be empty";
                        else if (FirstName.Any(char.IsDigit) || FirstName.Any(char.IsPunctuation))
                            error = "FirstName shouldn't contain digits/special characters";
                        break;
                    case "LastName":
                        if (string.IsNullOrWhiteSpace(LastName))
                            error = "LastName shouldn't be empty";
                        else if (LastName.Any(char.IsDigit) || LastName.Any(char.IsPunctuation))
                            error = "LastName shouldn't contain digits/special characters";
                        break;
                    case "JobTitle":
                        if (string.IsNullOrWhiteSpace(JobTitle))
                            error = "JobTitle shouldn't be empty";
                        else if (JobTitle.Any(char.IsDigit) || JobTitle.Any(char.IsPunctuation))
                            error = "JobTitle shouldn't contain digits/special characters";
                        break;
                    case "Department":
                        if (string.IsNullOrWhiteSpace(Department))
                            error = "Department shouldn't be empty";
                        else if (Department.Any(char.IsDigit) || Department.Any(char.IsPunctuation))
                            error = "Department shouldn't contain digits/special characters";
                        break;
                    case "ContactNumber":
                        if (ContactNumber < 0 || ContactNumber.ToString().Length != 10)
                            error = "Contact number should be of 10 digits";
                        else if (ContactNumber.ToString().Any(char.IsLetter))
                            error = "Contact number should only contain digits.";
                        break;
                    case "ExperienceInYears":
                        if (ExperienceInYears < 0 || ExperienceInYears > 40)
                            error = "Experience should be between 0-40 years";
                        else if (ContactNumber.ToString().Any(char.IsLetter))
                            error = "Experience should only contain digits.";
                        break;
                    case "Salary":
                        if (Salary < 0)
                            error = "Salary shouldn't be negative";
                        else if (ContactNumber.ToString().Any(char.IsLetter))
                            error = "Salary should only contain digits.";
                        break;
                    case "Email":
                        Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        if (!string.IsNullOrWhiteSpace(Email) && !regex.Match(Email).Success)
                            error = "Please enter a valid email.";
                        break;

                }
                return error;
            }
        }
        #region Constructors
        public Employee()
        {
            Id = GenerateId();
        }
        public Employee(string firstName, string lastName, string email, DateTime dob, string jobtitle, string department, int experienceInYears, long phoneNumber, decimal salary, EmployementType empType)
        {
            Id = GenerateId();
            FirstName = firstName;
            LastName = lastName;
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
