using static EmployeeDirectory.Caliburn.Models.Enums;

namespace EmployeeDirectory.Caliburn.Models
{
    public class GeneralFilter
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public GeneralFilterCategories Category { get; set; }
        public bool IsVisible { get; set; }

    }
}
