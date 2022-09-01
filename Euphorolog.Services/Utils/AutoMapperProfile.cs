using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs;
using Euphorolog.Services.DTOs.AuthDTOs;
using Euphorolog.Services.DTOs.StoriesDTOs;
using Euphorolog.Services.DTOs.UsersDTOs;

namespace Euphorolog.Services.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Stories Mappers
            CreateMap<Stories, GetStoryResponseDTO>();
            CreateMap<PostStoryRequestDTO, Stories>();
            CreateMap<UpdateStoryRequestDTO, Stories>();

            //Authentication Mappers
            CreateMap<SignUpRequestDTO, Users>();
            CreateMap<Users, SignUpResponseDTO>();

            //User Mappers
            CreateMap<Users, UserInfoResponseDTO>();
            CreateMap<UpdateUserInfoRequestDTO, Users>();
            CreateMap<Users, UpdateUserInfoResponseDTO>();
        }
    }
}
