namespace CopyCat.Model;

public record ImpersonatedAccountCreationRequest
{
    public string Name { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string SendingFacilityId { get; set; }
    
    public string SendingClientId { get; set; }

    public bool IsActive { get; set; }
}