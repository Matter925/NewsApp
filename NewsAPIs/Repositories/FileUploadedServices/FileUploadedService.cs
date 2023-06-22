using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace NewsAPIs.Repositories.FileUploadedServices
{
    public class FileUploadedService : IFileUploadedService
    {
        private readonly IWebHostEnvironment _environment;
        public FileUploadedService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Task<string> GetUrlCategoryImage(string ImageName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadNewsImages(IFormFile file )
        {
            string Pathcom = Path.Combine("//Images/", file.FileName);
           string HostUrl = "https://localhost:7056"; 
            string PathImage = HostUrl + Pathcom;

            string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", file.FileName);
            using (var stream = System.IO.File.Create(filePathImage))
            {
                await file.CopyToAsync(stream);
                stream.Close();
            }
            
            return PathImage;
        }
        
        
    }
}
