using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs;

namespace Euphorolog.Services.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<StoriesDTO, Stories>();
            CreateMap<Stories, StoriesDTO>();
        }
    }
}
