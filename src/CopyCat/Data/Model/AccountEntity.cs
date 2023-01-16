namespace CopyCat.Data.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Accounts")]
public class AccountEntity
{
    [Column("Id"), Key, Required]
    public Guid Id { get; set; }

    [Column("Name"), Required]
    public string Name { get; set; }

    [Column("IsActive"), Required]
    public bool IsActive { get; set; }

    [Column("IsDeleted"), Required]
    public bool IsDeleted { get; set; }
    
    [Column("CreatedOn"), Required]
    public DateTimeOffset CreatedOn { get; set; }

    [Column("UpdatedOn"), Required]
    public DateTimeOffset UpdatedOn { get; set; }
}