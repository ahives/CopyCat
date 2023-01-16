namespace CopyCat.Model;

public record CreateAccountRequest
{
    public string Name { get; set; }
    
    public bool IsActive { get; set; }
}