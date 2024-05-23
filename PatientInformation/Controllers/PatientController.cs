using Microsoft.AspNetCore.Mvc;

namespace PatientInformation.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Patient()
        {
            return View();
        }
    }
}
