using Microsoft.AspNetCore.Mvc;

namespace EverBill.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
