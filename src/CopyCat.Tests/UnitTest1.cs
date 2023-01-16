using MassTransit;

namespace CopyCat.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Console.WriteLine(NewId.NextGuid());
        Console.WriteLine(NewId.NextGuid());
        Console.WriteLine(DateTimeOffset.UtcNow);
        Assert.Pass();
    }
}