using System.ComponentModel.DataAnnotations;

public abstract class BaseEntity
{
    //public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } 
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
    public bool IsDeleted { get; set; } = false; 
}
