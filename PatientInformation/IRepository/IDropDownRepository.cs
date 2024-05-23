using PatientInformation.ViewModel;

namespace PatientInformation.IRepository
{
    public interface IDropDownRepository
    {
        Task<VmResponseMessage> CreateDisease(VmParam vm);
        Task<VmResponseMessage> CreateAllergies(VmParam vm);
        Task<VmResponseMessage> CreateNcd(VmParam vm);
        Task<List<VmDropDown>> GetAllergies();
        Task<List<VmDropDown>> GetNcds();
        Task<List<VmDropDown>> GetDisease();
    }
}
