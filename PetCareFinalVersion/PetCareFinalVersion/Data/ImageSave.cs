using System.IO;
using Microsoft.AspNetCore.Http;

namespace PetCareFinalVersion.Data
{
    public static class ImageSave
    {
        public static string SaveImage(IFormFileCollection files, string store_local)
        {
            string img_name;
            try
            {
                var image = files[0];
                var filePath = Path.Combine($"resources/images/{store_local}", image.FileName);
                img_name = image.FileName;
                if (image.Length > 0)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                }
            }
            catch
            {
                img_name = "Default.png";
            }
            return img_name;
        }
    }
}
