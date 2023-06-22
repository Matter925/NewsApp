using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAPIs.Dtos.AuthorDtos;
using NewsAPIs.Dtos.NewsDtos;
using NewsAPIs.Repositories.AuthorRepositories;
using NewsAPIs.Repositories.NewRepositories;

namespace NewsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewRepository _newRepository;
        public NewsController(INewRepository newRepository)
        {
            _newRepository = newRepository;

        }
        [HttpGet("GetAllNews")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllNews()
        {
            var authors = await _newRepository.GetAllNews();

            return Ok(authors);
        }


        [HttpGet("GetNewByID/{id}")]

        public async Task<IActionResult> GetNewByID(int id)
        {
            var News = await _newRepository.GetNewByID(id);
            if (News == null)
            {
                return NotFound($"No New was found with ID: {id}");
            }

            return Ok(News);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateNews")]
        public async Task<IActionResult> CreateNew([FromForm] NewDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _newRepository.CreateNew(dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest(ModelState);
        }


        [HttpPut("UpdateNew/{id}")]
        public async Task<IActionResult> UpdateNew(int id, [FromForm] NewDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _newRepository.UpdateNew(dto, id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteNew/{id}")]
        public async Task<IActionResult> DeleteNew(int id)
        {
            var result = await _newRepository.DeleteNew(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

    }
}
