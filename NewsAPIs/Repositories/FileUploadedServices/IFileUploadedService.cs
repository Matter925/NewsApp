namespace NewsAPIs.Repositories.FileUploadedServices
{
    public interface IFileUploadedService
    {
        Task <string> UploadNewsImages(IFormFile file );

        Task<string> GetUrlCategoryImage(string ImageName);
        

    }
}
