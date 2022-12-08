using AutoMapper;
using Metadata;
using MVCApplication.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApplication.Infrastructure
{
    public class AutoMapperGenericProfile : Profile
    {
        public AutoMapperGenericProfile()
        {
            this.CreateMap<ClienteInsertViewModel, Cliente>();
            this.CreateMap<Cliente, ClienteQueryViewModel>();
            this.CreateMap<Cliente, ClienteUpdateViewModel>();
            this.CreateMap<ClienteUpdateViewModel, Cliente>();


        }
    }
}
