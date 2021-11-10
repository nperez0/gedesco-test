using Application.Models;
using AutoMapper;
using Domain.Aggregates.Users;
using Domain.Events;
using System;

namespace Application
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<UserCreatedEvent, UserDetail>();

            CreateMap<Address, AddressDetail>();

            CreateMap<AddressDetail, AddressDetail>();

            CreateMap<Address, AddressHistory>()
                .ForMember(x => x.CreatedOn, o => o.MapFrom(y => DateTime.Now));

            CreateMap<AddressHistory, AddressHistory>()
                .ForMember(x => x.CreatedOn, o => o.Ignore());
        }
    }
}
