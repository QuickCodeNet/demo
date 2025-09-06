using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuickCode.Demo.Portal.Helpers.Authorization;
using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;

namespace QuickCode.Demo.Portal.Controllers
{
    [Permission("Dashboard")]
    public class HomeController : BaseController
    {
        public HomeController(ITableComboboxSettingsClient tableComboboxSettingsClient, IHttpContextAccessor httpContextAccessor, IMemoryCache cache) : base(tableComboboxSettingsClient, httpContextAccessor, cache)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View("Privacy");
        }
    }
}
