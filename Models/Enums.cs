namespace EmployeeDirectory.Caliburn.Models
{
    public class Enums
    {
        public enum EmployementType
        {
            Contract,
            Permanent
        }
        public enum FilterCategories
        {
            Name,
            ContactNumber,
            Salary,
            Experience,
        }
        public enum GeneralFilterCategories
        {
            Department,
            JobTitle
        }
        public enum Gender
        {
            Male,
            Female,
            Nonbinary
        }
        public enum Status
        {
            Existing,
            Resigned
        }

    }
}
