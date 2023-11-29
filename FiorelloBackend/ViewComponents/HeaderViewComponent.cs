using FiorelloBackend.Models;
using FiorelloBackend.Services.Interfaces;
using FiorelloBackend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FiorelloBackend.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(ILayoutService layoutService,
                                   IHttpContextAccessor httpContextAccessor,
                                   UserManager<AppUser> userManager)
        {
            _layoutService = layoutService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderVM model = _layoutService.GetHeaderDatas();

            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(userId is not null)
            {
                AppUser currentUser = await _userManager.FindByIdAsync(userId);
                model.UserFullName = currentUser.FullName;
            }

            return await Task.FromResult(View(model));
        }
    }
}
