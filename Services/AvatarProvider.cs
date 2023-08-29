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
            string path = Path.Combine(PATH_DIR, fileName + ".jpeg");

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        public async Task<byte[]> LoadAvatarAsync(string fileName)
        {
            string path = Path.Combine(PATH_DIR, fileName + ".jpeg");

            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] bytes = new byte[fileStream.Length];
            await fileStream.ReadAsync(bytes, 0, (int)fileStream.Length);

            fileStream.DisposeAsync();

            return bytes;
        }
    }
}