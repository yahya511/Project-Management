using Domain.Enums;

namespace Application.DTOs
{
    public class DepartmentEmployeeDto
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        //public JobTitle JobTitle { get; set; } // إضافة خاصية JobTitle
    }
}
