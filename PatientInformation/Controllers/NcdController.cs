using Microsoft.AspNetCore.Mvc;

namespace PatientInformation.Controllers
{
    public class NcdController : Controller
    {
        public IActionResult Ncd()
        {
            return View();
        }
    }
}
