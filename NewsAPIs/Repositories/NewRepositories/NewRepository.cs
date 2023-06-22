using Microsoft.EntityFrameworkCore;
using NewsAPIs.Data;
using NewsAPIs.Dtos;
using NewsAPIs.Dtos.NewsDtos;
using NewsAPIs.Models;
using NewsAPIs.Repositories.FileUploadedServices;
using System.Drawing;

namespace NewsAPIs.Repositories.NewRepositories
{
    public class NewRepository : INewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadedService _fileUploadedService;
        public NewRepository(ApplicationDbContext context, IFileUploadedService fileUploadedService)
        {
            _context = context;
            _fileUploadedService = fileUploadedService;
        }
       
        public async Task<GeneralRetDto> CreateNew(NewDto dto)
        {
            var news = await _context.News.Where(p => p.Title == dto.Title).SingleOrDefaultAsync();
            if (news == null)
            {
                var ImagePath = await _fileUploadedService.UploadNewsImages(dto.Image);
                var new_s = new New
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    CreationDate = DateTime.Now,
                    Image = ImagePath,
                    AuthorId = dto.AuthorId,
                    PublicationDate = dto.PublicationDate,
                    
                };
                await _context.News.AddAsync(new_s);
                _context.SaveChanges();
                return new GeneralRetDto
                {
                    Success = true,
                    Message = "Successfully"
                };

            }
            return new GeneralRetDto
            {
                Success = false,
                Message = "The News title is already exist"
            };
        }
       
        public async Task<GeneralRetDto> DeleteNew(int id)
        {
            var New = await _context.News.FindAsync(id);
            if (New == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = $"No New was found with ID: {id}",
                };
            }
            _context.Remove(New);
            _context.SaveChanges();
            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Deleted"
            };
        }

        public async Task<IEnumerable<New>> GetAllNews()
        {
            var News = await _context.News.Include(a => a.Author).ToListAsync();

            return News;
        }

        public async Task<New> GetNewByID(int id)
        {
            var result = await _context.News.Include(f => f.Author).SingleOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<GeneralRetDto> UpdateNew(NewDto dto, int id)
        {
            var New = await _context.News.FindAsync(id);
            if (New == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = $"No New was found with ID: {id}",
                };
            }
            if (dto.Image != null)
            {
                var imagePath = await _fileUploadedService.UploadNewsImages(dto.Image);
                New.Image = imagePath;
            }

            New.Title = dto.Title;
            New.Description = dto.Description;
            New.AuthorId = dto.AuthorId;

            _context.News.Update(New);
            _context.SaveChanges(true);

            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Updated",
            };
        }
    }
}
