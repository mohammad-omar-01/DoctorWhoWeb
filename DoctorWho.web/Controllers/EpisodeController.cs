using AutoMapper;
using DoctorWho.Db.Repositories;
using DoctorWho.DTOs.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.web.Controllers
{
    [Route("/episodes")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly IMapper _mapper;

        public EpisodeController(EpisodeRepository episodeRepository, IMapper mapper)
        {
            _episodeRepository = episodeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<EpisodeDto>> GetEpisodes()
        {
            var episodes = _episodeRepository.GetAllEpisodes();
            var episodeDtos = _mapper.Map<List<EpisodeDto>>(episodes);
            return Ok(episodeDtos);
        }
    }
}
