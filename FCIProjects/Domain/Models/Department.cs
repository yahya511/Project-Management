namespace Domain.Models
{
    public class Department: BaseEntity
    {
        public int DepartmentID { get; set; } // GUID
        public string DepartmentName { get; set; }
        public int ManagerID { get; set; }

    }
}
