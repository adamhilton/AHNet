using AHNet.Web.Core.Entities;
using AHNet.Web.Features.Blog.ViewModels;
using AutoMapper;

namespace AHNet.Web.Core.AutoMapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<BlogPost, BlogPostPreviewViewModel>();
        }
    }
}
