namespace GetInto.API.Helpers
{
    public interface IUtilImage
    {
        Task<string> SaveImage(IFormFile imageFile, string address);
        void DeleteImage(string imageName, string address);
    }
}
