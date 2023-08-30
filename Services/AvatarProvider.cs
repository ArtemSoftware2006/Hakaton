using Microsoft.AspNetCore.Hosting;

namespace Services
{
    public class AvatarProvider
    {
        public readonly string PATH_DIR = Directory.GetCurrentDirectory() + "\\Avatars";
        public AvatarProvider()
        {
            if (!Directory.Exists(PATH_DIR))
            {
                Directory.CreateDirectory(PATH_DIR);
            }
        }
        public async Task SaveAvatarAsync(MemoryStream file, string fileName)
        {
            string path = Path.Combine(PATH_DIR, fileName + ".jpeg");

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                file.Seek(0, SeekOrigin.Begin);
                await file.CopyToAsync(fileStream);
            }
        }
        public async Task<byte[]> LoadAvatarAsync(string fileName)
        {
            string path = Path.Combine(PATH_DIR, fileName + ".jpeg");

            byte[] fileBytes = File.ReadAllBytes(path);

            return fileBytes;
        }
    }
}