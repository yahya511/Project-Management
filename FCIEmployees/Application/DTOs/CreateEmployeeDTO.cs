
namespace Application.DTOs
{
    public class CreateEmployeeDTO
    {
        //public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentID { get; set; }
        public int ManagerID { get; set; }
        public int? AddressID { get; set; } // إضافة خاصية AddressID
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        
    }


}
