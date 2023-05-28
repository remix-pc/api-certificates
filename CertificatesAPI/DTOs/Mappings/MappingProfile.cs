

using AutoMapper;
using CertificatesAPI.Models;

namespace CertificatesAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Certificate, CertificateDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }

    }
}
