using AutoMapper;
using ItomychStudioTask.API.Models;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.API.Profiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<BookCreateModel , Book>();
            CreateMap<BookEditModel , Book>();
        }
    }
}
