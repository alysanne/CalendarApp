using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BucksCalendar.Utilities
{
    public class ImageHandler
    {
        public static string ToBase64ImgString(byte[] image)
        {
            var base64Img = Convert.ToBase64String(image);
            return $"data:image/jpg;base64,{base64Img}";
        }

        public static async Task<byte[]> ToMemoryStreamAsync(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    return memoryStream.ToArray();
                }
                
                throw new ImageFormatLimitationException("Please upload a smaller image (max 2MB)");
            }
        }
    }
}