using Domain.Enums;

namespace Domain.Models
{
    public class Employee : BaseEntity
    {
        public int EmployeeID { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public JobTitle JobTitle { get; set; }
        public int DepartmentID { get; set; }
        public int ManagerID { get; set; }
        public int? AddressID { get; set; } // إضافة خاصية AddressID
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        // علاقات
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> Subordinates { get; set; }
        public virtual Address Address { get; set; } // علاقة مع العنوان
    }


}
