namespace CopyCat.Model;

public record AccountCreationRequest
{
    public string Name { get; set; }
    
    public bool IsActive { get; set; }
}