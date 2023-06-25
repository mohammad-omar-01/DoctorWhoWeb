using AutoMapper;
using DoctorWho.DTOs.Models;

namespace DoctorWho.web.Mappers
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();

        }
    }
}
