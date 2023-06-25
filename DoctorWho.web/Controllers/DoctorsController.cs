using AutoMapper;
using DoctorWho.Db.Repositories;
using DoctorWho.DTOs.Models;
using DoctorWho.Web.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.web.Controllers
{
    [ApiController]
    [Route("/Doctors")]
    public class DoctorsController : ControllerBase
    {

        private readonly DoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly DoctorDtoValidator _doctorDtoValidtor;


        public DoctorsController(DoctorRepository doctorRepository, IMapper mapper, DoctorDtoValidator doctorDtoValidator)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _doctorDtoValidtor = doctorDtoValidator;


        }

        [HttpGet]
        public ActionResult<List<DoctorDto>> GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAllDoctors();

            return Ok(doctors.Select(doctor => _mapper.Map<DoctorDto>(doctor)));
        }
        [HttpPost]
        public ActionResult AddDoctor(DoctorDto doctorDto)
        {
            ValidationResult validationResult = _doctorDtoValidtor.Validate(doctorDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var doctor = _mapper.Map<Doctor>(doctorDto);
            _doctorRepository.AddDoctor(doctor);
            var upsertedDoctorDto = _mapper.Map<DoctorDto>(doctor);

            return Created("Succesfully created", upsertedDoctorDto);
        }
        [HttpPut("/Doctors/{DoctorId}")]
        public ActionResult EditDoctor(int DoctorId,[FromBody] DoctorDto doctorDto)
        {
            ValidationResult validationResult = _doctorDtoValidtor.Validate(doctorDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var existingDoctor = _doctorRepository.GetDoctorById(DoctorId);

            if (existingDoctor != null)
            {
                var updatedDoctor = _mapper.Map(doctorDto, existingDoctor);
                _doctorRepository.UpdateDoctor(updatedDoctor);
            }
            else
            {
                var newDoctor = _mapper.Map<Doctor>(doctorDto);
                newDoctor.DoctorId = DoctorId;
                _doctorRepository.CreateDoctor(newDoctor);


            }
            var upsertedDoctor = _doctorRepository.GetDoctorById(DoctorId);
            var upsertedDoctorDto = _mapper.Map<DoctorDto>(upsertedDoctor);

            return Ok( upsertedDoctorDto);
        }

        [HttpDelete("/Doctors/{DoctorId}")]
        public ActionResult DeleteDoctor(int DoctorId)
        {
            var count=_doctorRepository.DeleteDoctor(DoctorId);
            if (count == 1) { 
            return NoContent();
            }
            return NotFound();

        }


    }
}
