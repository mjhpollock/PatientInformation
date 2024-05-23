using Microsoft.AspNetCore.Mvc;

namespace PatientInformation.Controllers
{
    public class AllergiesController : Controller
    {
        public IActionResult Allergies()
        {
            return View();
        }
    }
}
