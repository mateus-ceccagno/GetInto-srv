namespace GetInto.API.Helpers
{
    public class UtilImage : IUtilImage
    {
        private readonly IWebHostEnvironment _hostEnv;
        public UtilImage(IWebHostEnvironment hostEnv)
        {
            _hostEnv = hostEnv;
        }
        public async Task<string> SaveImage(IFormFile imageFile, string address)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                              .Take(8)
                                              .ToArray()
                                         ).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnv.ContentRootPath, @$"Resources/{address}", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        public void DeleteImage(string imageName, string address)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var imagePath = Path.Combine(_hostEnv.ContentRootPath, @$"Resources/{address}", imageName);
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }
        }
    }
}
