using AutoMapper;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using DoctorWho.DTOs.Models;
using DoctorWho.Web.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.web.Controllers
{
    [Route("/episodes")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly IMapper _mapper;
        private readonly EpisodeDtoValidator _episodeDtoValidator;

        public EpisodeController(
            EpisodeRepository episodeRepository,
            IMapper mapper,
            EpisodeDtoValidator episodeValidator
        )
        {
            _episodeRepository = episodeRepository;
            _mapper = mapper;
            _episodeDtoValidator = episodeValidator;
        }

        [HttpGet]
        public ActionResult<List<EpisodeDto>> GetEpisodes()
        {
            var episodes = _episodeRepository.GetAllEpisodes();
            var episodeDtos = _mapper.Map<List<EpisodeDto>>(episodes);
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
    }
}
