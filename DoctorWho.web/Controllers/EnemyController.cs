using AutoMapper;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using DoctorWho.DTOs.Models;
using DoctorWho.web.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.web.Controllers
{
    [ApiController]
    [Route("/Enemies")]
    public class EnemyController : ControllerBase
    {
        private readonly EnemyRepository _enemyRepository;
        private readonly IMapper _mapper;
        private readonly EnemyDTOValidator _enemeyDTOValidator;

        public EnemyController(
            EnemyRepository enemeyRepository,
            IMapper mapper,
            EnemyDTOValidator enemeyDtoValidator
        )
        {
            _enemyRepository = enemeyRepository;
            _mapper = mapper;
            _enemeyDTOValidator = enemeyDtoValidator;
        }

        [HttpGet]
        public ActionResult<List<DoctorDto>> GetAllEnemies()
        {
            var eneimes = _enemyRepository.GetAllEnemies();

            return Ok(eneimes.Select(enemey => _mapper.Map<DoctorDto>(enemey)));
        }

        [HttpPost]
        public ActionResult AddEnemy(EnemyDTO EnemyDto)
        {
            ValidationResult validationResult = _enemeyDTOValidator.Validate(EnemyDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var enemy = _mapper.Map<Enemy>(EnemyDto);
            _enemyRepository.CreateEnemy(enemy);
            var createdEnemey = _mapper.Map<EnemyDTO>(enemy);

            return Created("Succesfully created", createdEnemey);
        }

        [HttpPut("/{enemyId}")]
        public ActionResult EditEnemy(int enemyId, [FromBody] EnemyDTO enemyDto)
        {
            ValidationResult validationResult = _enemeyDTOValidator.Validate(enemyDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var existingEnemy = _enemyRepository.GetEnemyById(enemyId);

            if (existingEnemy != null)
            {
                var updatedenemey = _mapper.Map(enemyDto, existingEnemy);
                _enemyRepository.GetEnemyById(enemyId);
                var updatedenemeyDto = _mapper.Map<EnemyDTO>(updatedenemey);
                return Ok(updatedenemeyDto);
            }
            else
            {
                var newenemey = _mapper.Map<Enemy>(enemyDto);
                newenemey.EnemyId = enemyId;
                _enemyRepository.CreateEnemy(newenemey);
                var createdenemeyDto = _mapper.Map<EnemyDTO>(newenemey);
                return Created("", createdenemeyDto);
            }
        }

        [HttpDelete("/{enemeyId}")]
        public ActionResult Deleteenemey(int enemeyId)
        {
            var count = _enemyRepository.DeleteEnemy(enemeyId);
            if (count == 1)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
