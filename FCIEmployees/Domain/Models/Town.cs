
namespace Domain.Models
{
    public class Town: BaseEntity
        {
            public int TownID { get; set; } // GUID
            public string Name { get; set; }
            // علاقات
            public virtual ICollection<Address> Addresses { get; set; }
        }
}
