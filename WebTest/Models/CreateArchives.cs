namespace WebTest.Models;

public sealed class CreateArchives
{
    public IFormFile[] Archives { get; init; } = Array.Empty<IFormFile>();
}
