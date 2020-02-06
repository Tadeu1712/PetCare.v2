using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace PetCareFinalVersion.Data
{
    public class ImageSave
    {
        
        public static string SaveImage(IFormFileCollection files, string store_local)
        {
            string img_name;
            try
            {
                var image = files[0];
                img_name = Guid.NewGuid()+image.FileName;
                var file_path = Path.Combine($"resources/images/{store_local}" + img_name);
                if(image.Length > 0)
                {
                    using (var fileStream = new FileStream(file_path, FileMode.Create)){
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
