namespace Autoshop.Common.Mapping
{
    using AutoMapper;
    using Autoshop.Models;
    using Autoshop.Services.Models.Blog;
    using Autoshop.Services.Models.Reviews;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Review, ReviewListingServiceModel>()
                .ForMember(r => r.Author, cfg => cfg.MapFrom(a => $"{a.Author.FirstName} {a.Author.LastName.First()}."));
            CreateMap<Post, PostListingServiceModel>()
               .ForMember(plm => plm.Author, cfg => cfg.MapFrom(p => $"{p.Author.FirstName} {p.Author.LastName}"));  
            CreateMap<Post, LatestPostsServiceModel>();
            CreateMap<Category, CategoryListingServiceModel>();
        }
    }
}