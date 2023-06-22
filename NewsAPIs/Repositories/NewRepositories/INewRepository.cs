using NewsAPIs.Dtos.AuthorDtos;
using NewsAPIs.Dtos;
using NewsAPIs.Models;
using NewsAPIs.Dtos.NewsDtos;

namespace NewsAPIs.Repositories.NewRepositories
{
    public interface INewRepository
    {
        Task<IEnumerable<New>> GetAllNews();
        Task<New> GetNewByID(int id);
        Task<GeneralRetDto> CreateNew(NewDto Dto);
        Task<GeneralRetDto> UpdateNew(NewDto dto, int id);
        Task<GeneralRetDto> DeleteNew(int id);
    }
}
