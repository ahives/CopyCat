namespace CopyCat.Model;

public record CreateImpersonatedAccountRequest
{
    public string Name { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string SendingFacilityId { get; set; }
    
    public string SendingAppId { get; set; }

    public bool IsActive { get; set; }
}