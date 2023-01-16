namespace CopyCat;

public record Result
{
    public bool IsData { get; set; }
    public bool HasFaulted { get; set; }
}

public record Result<T> :
    Result
{
    public T Data { get; set; }
}