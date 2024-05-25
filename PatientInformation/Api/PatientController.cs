using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientInformation.IRepository;
using PatientInformation.ViewModel;

namespace PatientInformation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepo;
        public PatientController(IPatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }
        [HttpPost("CreatePatient")]
        public async Task<ActionResult<VmResponseMessage>> CreatePatient(VmPatient vm)
        {
            var response = await _patientRepo.CreatePatient(vm);
            return Ok(response);
        }
        [HttpGet("GetPatient")]
        public async Task<ActionResult<List<VmPatient>>> GetPatient()
        {
            var list = await _patientRepo.GetPatient();
            return Ok(list);
        }
        [HttpGet("DeletePatient")]
        public async Task<ActionResult<VmResponseMessage>> DeletePatient(int id)
        {
            var response = await _patientRepo.DeletePatient(id);
            return Ok(response);
        }
        [HttpGet("GetPatientById")]
        public async Task<ActionResult<VmPatient>> GetPatientById(int id)
        {
            var response = await _patientRepo.GetPatientById(id);
            return Ok(response);
        }
        [HttpPost("UpdatePatient")]
        public async Task<ActionResult<VmPatient>> UpdatePatient(VmPatient vm)
        {
            var response = await _patientRepo.UpdatePatient(vm);
            return Ok(response);
        }
    }
}
