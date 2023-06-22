using Microsoft.EntityFrameworkCore;
using NewsAPIs.Data;
using NewsAPIs.Dtos;
using NewsAPIs.Dtos.AuthorDtos;
using NewsAPIs.Models;
using NewsAPIs.Repositories.FileUploadedServices;

namespace NewsAPIs.Repositories.AuthorRepositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;
       
        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<GeneralRetDto> CreateAuthor(AuthorDto Dto)
        {
            var author = await _context.Authors.Where(c => c.Name == Dto.Name).FirstOrDefaultAsync();
            if (author == null)
            {
                
                var auth = new Author
                {
                    Name = Dto.Name,
                    
                };
                await _context.Authors.AddAsync(auth);
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
                Message = "The Author Name is already exist"
            };
        }

        public async Task<GeneralRetDto> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = $"No Author was found with ID: {id}",
                };
            }
            _context.Remove(author);
            _context.SaveChanges();
            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Deleted"
            };
        }

        public async Task<IEnumerable<Author>> GetAllAuthor()
        {
            var authors = await _context.Authors.ToListAsync();

            return authors;
        }

        public async Task<Author> GetAuthorByID(int id)
        {
            var result = await _context.Authors.SingleOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<GeneralRetDto> UpdateAuthor(AuthorDto dto, int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = $"No author was found with ID: {id}",
                };
            }
            
            author.Name = dto.Name;
            
            _context.Authors.Update(author);
            _context.SaveChanges(true);

            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Updated",
            };
        }
    }
}
