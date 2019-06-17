using AutoMapper;
using SavePacket.Models;
using SavePacket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavePacket.Profiles
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationViewModel, Packet>()
                    .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.PacketLength))
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.PacketTime))
                    .ForMember(dest => dest.SourceIP, opt => opt.MapFrom(src => src.PacketIP));

            });
            return config;
        }
    }
}
