using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger<BaseController> Logger;

        protected BaseController(IMapper mapper, ILogger<BaseController> logger)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task<IActionResult> ExecuteWithErrorHandlingAsync<T>(Func<Task<T>> action, string errorMessage)
        {
            try
            {
                var result = await action();
                return View(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, errorMessage);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
