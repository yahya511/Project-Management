
namespace Domain.Models
{
    public class Address : BaseEntity
    {
        public int AddressID { get; set; } 
        public string AddressText { get; set; }
        public int? TownID { get; set; }

        // علاقات
        public virtual Town Town { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }

}
