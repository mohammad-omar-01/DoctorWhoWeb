using AutoMapper;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using DoctorWho.DTOs.Models;
using DoctorWho.web.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly AuthorDTOValidator _authorDTOValidtor;

        public AuthorsController(
            AuthorRepository authorRepository,
            IMapper mapper,
            AuthorDTOValidator authorValidator
        )
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _authorDTOValidtor = authorValidator;
        }

        [HttpGet]
        public ActionResult<List<AuthorDTO>> GetAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();
            return Ok(authors.Select(auhtor => _mapper.Map<AuthorDTO>(auhtor)));
        }

        [HttpPut("{authorId}")]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDTO updatedAuthor)
        {
            ValidationResult validator = _authorDTOValidtor.Validate(updatedAuthor);

            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors);
            }
            var existingAuthor = _authorRepository.GetAuthorById(authorId);
            if (existingAuthor == null)
            {
                var author = _mapper.Map<Author>(updatedAuthor);
                _authorRepository.CreateAuthor(author);
                return Created("", updatedAuthor);
            }

            existingAuthor.AuthorName = updatedAuthor.AuthorName;

            _authorRepository.UpdateAuthor(existingAuthor);
            updatedAuthor = _mapper.Map<AuthorDTO>(existingAuthor);

            return Ok(updatedAuthor);
        }
    }
}
