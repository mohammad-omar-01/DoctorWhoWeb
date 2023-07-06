using AutoMapper;
using DoctorWho.Db.Models;
using DoctorWho.DTOs.Models;

namespace DoctorWho.web.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Doctor, DoctorDto>()
                .ForMember(
                    doctor => doctor.BirthDate,
                    opt => opt.MapFrom(src => src.BirthDate.Date)
                );
            CreateMap<DoctorDto, Doctor>();
            CreateMap<DoctorCreationRequestDTO, Doctor>();
            CreateMap<Doctor, DoctorCreationRequestDTO>()
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId));
            CreateMap<DoctorUpsertRequestDTO, Doctor>();
            CreateMap<Doctor, DoctorUpsertRequestDTO>();
            CreateMap<EpisodeCreationRequesetDTO, Episode>();
            CreateMap<Episode, EpisodeCreationRequesetDTO>();
            CreateMap<EpisodeDto, Episode>();
            CreateMap<Episode, EpisodeDto>();
        }
    }
}
