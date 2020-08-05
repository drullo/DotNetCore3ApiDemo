using AutoMapper;
using dotnetWebApi.DTOs;
using dotnetWebApi.Models;

namespace dotnetWebApi.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
            
            CreateMap<CommandCreateDto, Command>();

            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}