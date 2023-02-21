using AutoMapper;
using UserEntity.Dtos;
using UserEntity.Model;

namespace UserEntity
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AddUserDto>();
            CreateMap<AddUserDto, User>();
        }
    }
}
