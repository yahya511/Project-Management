
namespace Application.DTOs
{
    public class GetEmployeeDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentID { get; set; }
        public int ManagerID { get; set; }
        public string? Address { get; set; } // إضافة خاصية AddressID
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        
    }


}
