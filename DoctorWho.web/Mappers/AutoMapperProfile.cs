using AutoMapper;
using DoctorWho.DTOs.Models;

namespace DoctorWho.web.Mappers
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Doctor, DoctorDto>().ForMember(doctor=>doctor.BirthDate,opt=>opt.MapFrom(src=>src.BirthDate.Date));

            CreateMap<DoctorDto, Doctor>();

        }
    }
}
