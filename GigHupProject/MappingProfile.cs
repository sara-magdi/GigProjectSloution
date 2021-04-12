using AutoMapper;
using DAL;
using DAL.DTOs;
using DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubProject
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notification, NotificationDTO>();
            CreateMap<Gig, GigDTO>();
            CreateMap<User, UserDTO>();

        }
    }
}
