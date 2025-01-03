using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitForm(string Name, string Email, string Message)
        {
            ViewBag.Message = "Dziękujemy za przesłanie formularza!";
            return View("Index");
        }
    }

}
