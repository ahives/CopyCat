namespace CopyCat.Data.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ImpersonatedAccounts")]
public class ImpersonatedAccountEntity
{
    [Column("Id"), Key, Required]
    public Guid Id { get; set; }

    [Column("Name"), Required]
    public string Name { get; set; }

    [Column("AccountId"), Required]
    public Guid AccountId { get; set; }
    public AccountEntity Account { get; set; }

    [Column("SendingFacilityId")]
    public string SendingFacilityId { get; set; }

    [Column("SendingClientId")]
    public string SendingClientId { get; set; }

    [Column("IsActive"), Required]
    public bool IsActive { get; set; }

    [Column("IsDeleted"), Required]
    public bool IsDeleted { get; set; }

    [Column("CreatedOn"), Required]
    public DateTimeOffset CreatedOn { get; set; }

    [Column("UpdatedOn"), Required]
    public DateTimeOffset UpdatedOn { get; set; }
}