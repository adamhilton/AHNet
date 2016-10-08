using System.Threading.Tasks;
using AHNet.Web.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AHNet.Web.Features.Shared.Components
{
    public class ContentTags : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly ContentTagRepository _contentTagRepository;

        public ContentTags(
            IMapper mapper,
            ContentTagRepository contentTagRepository)
        {
            _mapper = mapper;
            _contentTagRepository = contentTagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await _contentTagRepository.ListAsync();
            
            return View(tags);
        }
    }
}
