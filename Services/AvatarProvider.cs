using Microsoft.AspNetCore.Hosting;

namespace Services
{
    public class AvatarProvider
    {
        public readonly string PATH_DIR;
        public AvatarProvider()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Avatars"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Avatars");
            }
            PATH_DIR = Directory.GetCurrentDirectory() + "\\Avatars";
        }
        public async Task SaveAvatarAsync(MemoryStream file, string fileName)
        {
            string path = Path.Combine("C:\\Users\\Artem\\Desktop\\Avatar", fileName + ".jpeg");

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                file.Seek(0, SeekOrigin.Begin);
                await file.CopyToAsync(fileStream);
            }
        }
        public async Task<byte[]> LoadAvatarAsync(string fileName)
        {
            string path = Path.Combine("C:\\Users\\Artem\\Desktop\\Avatar", fileName + ".jpeg");

            byte[] fileBytes = File.ReadAllBytes(path);

            return fileBytes;
        }
    }
}