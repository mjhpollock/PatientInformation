using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientInformation.IRepository;
using PatientInformation.Models;
using PatientInformation.ViewModel;

namespace PatientInformation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        public readonly IDropDownRepository _drpRepo;
        public DropDownController(IDropDownRepository drpRepo)
        {
            _drpRepo = drpRepo;
        }
        [HttpPost("CreateDisease")]
        public async Task<ActionResult<VmResponseMessage>> CreateDisease(VmParam vm)
        {
            var res = await _drpRepo.CreateDisease(vm);
            return Ok(res);
        }
        [HttpPost("CreateAllergies")]
        public async Task<ActionResult<VmResponseMessage>> CreateAllergies(VmParam vm)
        {
            var res = await _drpRepo.CreateAllergies(vm);
            return Ok(res);
        }
        [HttpPost("CreateNcd")]
        public async Task<ActionResult<VmResponseMessage>> CreateNcd(VmParam vm)
        {
            var res = await _drpRepo.CreateNcd(vm);
            return Ok(res);
        }
        [HttpGet("GetAllergies")]
        public  async Task<ActionResult<List<VmDropDown>>> GetAllergies()
        {
            var res = await _drpRepo.GetAllergies();
            return Ok(res);
        }
        [HttpGet("GetNcds")]
        public async Task<ActionResult<List<VmDropDown>>> GetNcds()
        {
            var res = await _drpRepo.GetNcds();
            return Ok(res);
        }
        [HttpGet("GetDisease")]
        public async Task<ActionResult<List<VmDropDown>>> GetDisease()
        {
            var res = await _drpRepo.GetNcds();
            return Ok(res);
        }
    }
}
