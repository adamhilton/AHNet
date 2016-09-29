using AHNet.Web.Core.Entities;
using AHNet.Web.Features.Admin.Admin.ViewModels;
using AHNet.Web.Features.Blog.ViewModels;
using AutoMapper;

namespace AHNet.Web.Core.AutoMapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<BlogPost, BlogPostPreviewViewModel>();
            CreateMap<BlogPost, BlogPostContentViewModel>();
            CreateMap<CreateBlogPostViewModel, BlogPost>();
            CreateMap<EditBlogPostViewModel, BlogPost>();
            CreateMap<BlogPost, EditBlogPostViewModel>();
        }
    }
}
