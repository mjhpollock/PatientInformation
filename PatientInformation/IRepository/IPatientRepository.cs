using PatientInformation.ViewModel;

namespace PatientInformation.IRepository
{
    public interface IPatientRepository
    {
        Task<VmResponseMessage> CreatePatient(VmPatient vm);
        Task<List<VmPatient>> GetPatient();
        Task<VmPatient> GetPatientById(int id);
        Task<VmResponseMessage> DeletePatient(int id);
        Task<VmResponseMessage> UpdatePatient(VmPatient vm);
    }
}
