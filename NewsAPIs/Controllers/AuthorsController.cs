using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPIs.Data;
using NewsAPIs.Dtos.AuthorDtos;
using NewsAPIs.Repositories.AuthorRepositories;

namespace NewsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            
        }
        [HttpGet("GetAllAuthors")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthor();

            return Ok(authors);
        }


        [HttpGet("GetAuthorByID/{id}")]
        
        public async Task<IActionResult> GetAuthorByID(int id)
        {
            var category = await _authorRepository.GetAuthorByID(id);
            if (category == null)
            {
                return NotFound($"No Authorwas found with ID: {id}");
            }

            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateNewAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authorRepository.CreateAuthor(dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest(ModelState);
        }


        [HttpPut("UpdateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authorRepository.UpdateAuthor(dto, id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorRepository.DeleteAuthor(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

    }
}
