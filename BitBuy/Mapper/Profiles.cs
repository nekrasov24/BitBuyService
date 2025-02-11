using AutoMapper;
using BitBuy.Models;
using BitBuy.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.Mapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<EditUserRequestModel, User>();
        }
    }
}
