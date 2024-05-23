using Microsoft.EntityFrameworkCore;
using PatientInformation.Enums;
using PatientInformation.IRepository;
using PatientInformation.Models;
using PatientInformation.ViewModel;

namespace PatientInformation.Repository
{
    public class PatientRepository: IPatientRepository
    {
        private readonly AppDbContext _db;
        public PatientRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<VmResponseMessage> CreatePatient(VmPatient vm)
        {
            var response = new VmResponseMessage();
            var ncds = vm.NcdIds.Split(',').ToList();
            var allergies = vm.AllergyIds.Split(',').ToList();
            var patient = new Patient
            {
                Name = vm.Name,
                DiseaseId = vm.DiseaseId,
                Epilepsy = (Epilepsy)Enum.Parse(typeof(Epilepsy), vm.Epilepsy),
                CreatedDate = DateTime.Now,
            };
            await _db.AddAsync(patient);
            await _db.SaveChangesAsync();
            try
            {
                var ncdList = new List<NcdDetails>();
                foreach (var nc in ncds)
                {
                    if (string.IsNullOrEmpty(nc))
                    {
                        continue;
                    }
                    var ncd = new NcdDetails
                    {
                        PatientId = patient.Id,
                        NcdId = Convert.ToInt32(nc)
                    };
                    ncdList.Add(ncd);
                }
                await _db.AddRangeAsync(ncdList);
                await _db.SaveChangesAsync();
            }catch (Exception ex)
            {

            }

            var alleryList = new List<AllergiesDetails>();  
            foreach (var al in allergies)
            {
                if (string.IsNullOrEmpty(al))
                {
                    continue;
                }
                var allergy = new AllergiesDetails
                {
                    PatientId = patient.Id,
                    AllergyId = Convert.ToInt32(al)
                };
                alleryList.Add(allergy);
            }
            await _db.AddRangeAsync(alleryList);
            await _db.SaveChangesAsync();

            response.Type = "Success";
            response.Message = "Successfully Saved...!";
            return response;
        }

        public async Task<List<VmPatient>> GetPatient()
        {
            var list = new List<VmPatient>();
            var patient = await (from pt in _db.Patient
                                 join d in _db.Disease on pt.DiseaseId equals d.Id
                                 select new VmPatient
                                 {
                                     Id = pt.Id,
                                     Name = pt.Name,
                                     DiseaseName = d.Name,
                                     Epilepsy = pt.Epilepsy.ToString(),
                                     Ncds = new List<VmNcds>(),
                                     Allergies = new List<VmAllergies>()
                                 }).ToListAsync();
            foreach (var pat in patient)
            {
                var ncd = await (from nd in _db.NcdDetails
                                 join n in _db.Ncds on nd.NcdId equals n.Id
                                 where nd.PatientId == pat.Id
                                 select new VmNcds
                                 {
                                     Id = nd.Id,
                                     Name = n.Name,
                                 }).ToListAsync();
                var allergies = await (from ald in _db.AllergiesDetails
                                 join al in _db.Allergies on ald.AllergyId equals al.Id
                                 where ald.PatientId == pat.Id
                                 select new VmAllergies
                                 {
                                     Id = ald.Id,
                                     Name = al.Name,
                                 }).ToListAsync();
                pat.Ncds = ncd;
                pat.Allergies = allergies;
                list.Add( pat );
            }
            return list;
        }
    }
}
