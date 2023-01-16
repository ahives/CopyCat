namespace CopyCat.Data;

public record ImpersonatedAccount
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AccountId { get; set; }
    public string SendingFacilityId { get; set; }
    public string SendingAppId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}