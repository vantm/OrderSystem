namespace ProductService.ApiService.Products.Models;

public record NewProductDto
{
    public string Name { get; set; } = string.Empty;

    public string? ImageBase64 { get; set; }

    public byte[] ImageBuffer()
    {
        var imageBuffer = Array.Empty<byte>();
        if (!string.IsNullOrEmpty(ImageBase64))
        {
            imageBuffer =
                Convert.FromBase64String(ImageBase64);
        }

        return imageBuffer;
    }
}