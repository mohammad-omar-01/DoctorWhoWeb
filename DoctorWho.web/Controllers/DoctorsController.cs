using AutoMapper;
using DoctorWho.Db.Repositories;
using DoctorWho.DTOs.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.web.Controllers
{
    [ApiController]
    [Route("/Doctors")]
    public class DoctorsController : ControllerBase
    {

        private readonly DoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorsController(DoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<DoctorDto>> GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAllDoctors();

            return Ok(doctors.Select(doctor=>_mapper.Map<DoctorDto>(doctor)));
        }

    }
}
