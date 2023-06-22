using NewsAPIs.Dtos;
using NewsAPIs.Dtos.AuthorDtos;
using NewsAPIs.Models;

namespace NewsAPIs.Repositories.AuthorRepositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthor();
        Task<Author> GetAuthorByID(int id);
        Task<GeneralRetDto> CreateAuthor(AuthorDto Dto);
        Task<GeneralRetDto> UpdateAuthor(AuthorDto dto, int id);
        Task<GeneralRetDto> DeleteAuthor(int id);
    }
}
