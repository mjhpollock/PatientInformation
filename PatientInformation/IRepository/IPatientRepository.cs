using PatientInformation.ViewModel;

namespace PatientInformation.IRepository
{
    public interface IPatientRepository
    {
        Task<VmResponseMessage> CreatePatient(VmPatient vm);
        Task<List<VmPatient>> GetPatient();
    }
}
