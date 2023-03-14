using Application.Dtos;
using AutoMapper;
using Domain;

namespace Application.Helpers.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<Author,AuthorDto>();
        CreateMap<Book,BookDto>();
        CreateMap<Category,CategoryDto>();
        CreateMap<Favorite,FavoriteDto>();
        
    }
    
}
