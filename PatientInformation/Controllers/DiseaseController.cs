using Microsoft.AspNetCore.Mvc;

namespace PatientInformation.Controllers
{
    public class DiseaseController : Controller
    {
        public IActionResult Disease()
        {
            return View();
        }
    }
}
