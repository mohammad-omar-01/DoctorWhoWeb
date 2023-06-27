using AutoMapper;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using DoctorWho.DTOs.Models;
using DoctorWho.web.Validators;
using DoctorWho.Web.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.web.Controllers
{
    [Route("/episode")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly IMapper _mapper;
        private readonly EpisodeDtoValidator _episodeDtoValidator;
        private readonly EnemyDTOValidator _enmeyDTOVaildator;

        public EpisodeController(
            EpisodeRepository episodeRepository,
            IMapper mapper,
            EpisodeDtoValidator episodeValidator,
            EnemyDTOValidator enemeyValidator
        )
        {
            _episodeRepository = episodeRepository;
            _mapper = mapper;
            _episodeDtoValidator = episodeValidator;
            _enmeyDTOVaildator = enemeyValidator;
        }

        [HttpGet]
        public ActionResult<List<EpisodeDto>> GetEpisodes()
        {
            var episodes = _episodeRepository.GetAllEpisodes();
            var episodeDtos = _mapper.Map<List<EpisodeDto>>(episodes);
            return Ok(episodeDtos);
        }

        [HttpGet("/{episodeId}")]
        public ActionResult<List<EpisodeDto>> GetEpisodes(int episodeId)
        {
            var episode = _episodeRepository.GetEpisodeById(episodeId);
            if (episode == null)
            {
                return NotFound();
            }
            var episodeDtos = _mapper.Map<EpisodeDto>(episode);
            return Ok(episodeDtos);
        }

        [HttpPost]
        public ActionResult<int> CreateEpisode([FromBody] EpisodeCreationRequesetDTO episodeDto)
        {
            ValidationResult validationResult = _episodeDtoValidator.Validate(episodeDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var episode = _mapper.Map<Episode>(episodeDto);
            _episodeRepository.CreateEpisode(episode);

            return episode.EpisodeId;
        }

        [HttpPut("{episodeId}/{enemyId}")]
        public ActionResult AddEnemyToEpisode(int episodeId, int enemyId)
        {
            var episode = _episodeRepository.GetEpisodeByIdWithEnimes(episodeId);
            if (episode == null)
            {
                return NotFound($"Episode with ID {episodeId} not found.");
            }

            var enemeyForEpisode = episode.Enemies.FirstOrDefault(
                enemy => enemy.EnemyId == enemyId
            );

            if (enemeyForEpisode == null)
            {
                _episodeRepository.AddEnemyToEpisode(episodeId, enemyId);
                return Ok();
            }

            return NoContent();
        }

        [HttpDelete("{episodeId}/{enemyId}")]
        public ActionResult<int> DeleteEnmeyFromEpisode(int episodeId, int enemyId)
        {
            var episode = _episodeRepository.GetEpisodeByIdWithEnimes(episodeId);
            if (episode == null)
            {
                return NotFound($"Episode with id = {episodeId} was not found");
            }
            var enmeyForEpisode = episode.Enemies.FirstOrDefault(enmey => enmey.EnemyId == enemyId);
            if (enmeyForEpisode == null)
            {
                return NotFound($"Enemy with id ={enemyId} Was not found");
            }
            _episodeRepository.DeleteEnemyFromEpisode(episodeId, enemyId);
            return Ok();
        }
    }
}
