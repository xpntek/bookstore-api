using Application.Dtos;
using AutoMapper;
using Domain;

namespace Application.Helpers.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Author,AuthorDto>();
        CreateMap<Departament, DepartamentDto>();
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dto => dto.Description,
                config => config.MapFrom(employee => employee.Departament.Description))
            .ForMember(dto=>dto.Address,config=>config.MapFrom(employee=>employee.ApplicationUser.Address))
            .ForMember(dto=>dto.Email,config=>config.MapFrom(employee=>employee.ApplicationUser.Email))
            .ForMember(dto=>dto.FullName,config=>config.MapFrom(employee=>employee.ApplicationUser.Fullname))
            .ForMember(dto=>dto.PhoneNumber,config=>config.MapFrom(employee=>employee.ApplicationUser.PhoneNumber))
            .ForMember(dto=>dto.UserName,config=>config.MapFrom(employee=>employee.ApplicationUser.UserName))
            .ForMember(dto=>dto.UserId,config=>config.MapFrom(employee=>employee.ApplicationUser.Id));

        CreateMap<Client, ClientdDto>()
            .ForMember(dto=>dto.Address,config=>config.MapFrom(client=>client.ApplicationUser.Address))
            .ForMember(dto=>dto.Email,config=>config.MapFrom(client=>client.ApplicationUser.Email))
            .ForMember(dto=>dto.FullName,config=>config.MapFrom(client=>client.ApplicationUser.Fullname))
            .ForMember(dto=>dto.PhoneNumber,config=>config.MapFrom(client=>client.ApplicationUser.PhoneNumber))
            .ForMember(dto=>dto.UserName,config=>config.MapFrom(client=>client.ApplicationUser.UserName))
            .ForMember(dto=>dto.UserId,config=>config.MapFrom(client=>client.ApplicationUser.Id));

        CreateMap<Reserve, ReserveDto>()
            .ForMember(dto => dto.BookId, config => config.MapFrom(reserve => reserve.Book.Id))
            .ForMember(dto => dto.UserId, config => config.MapFrom(reserve => reserve.User.Id));
            


    }
    
}
