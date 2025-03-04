using AutoMapper;
using RatingEngine.DTO.Province;
using RatingEngine.Persistance.Entities;

namespace RatingEngine.DomainServices.MapToDto
{
    public class MappingToDto : Profile
    {
        public MappingToDto()
        {
            CreateMap<Province,ProvinceDto>()
                .ReverseMap();
        }
    }
}
