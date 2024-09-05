using AutoMapper;
 
namespace API;

public class AutoMapperProfiles : Profile {

    /**
    * This method is used to define the mapping between the entities and the DTOs
    * usign AutoMapper
    */
    public AutoMapperProfiles(){
        CreateMap<AppUser, MembersDto>()
        .ForMember(
            d => d.age, o => o.MapFrom(s=>s.dateOfBirth.calculateAge())
        )
        .ForMember(
            d => d.photoUrl, o => o.MapFrom(s=>s.Photos.FirstOrDefault(x=>x.isMain)!.url
        ));
        CreateMap<Photo, PhotoDto>();
    }
}