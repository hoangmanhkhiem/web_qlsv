using System;
using System.IO;
using System.Threading.Tasks;

namespace qlsv.Services;

public class ImageService
{
    // Constructors
    public ImageService()
    {

    }

    public async Task<byte[]> ToByteAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is null or empty", nameof(file));
        }

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}