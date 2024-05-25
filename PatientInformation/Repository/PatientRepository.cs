using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
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
            try {
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
            }catch(Exception ex)
            {
                return new VmResponseMessage
                {
                    Message = ex.Message,
                    Type = "error"
                };
            }
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
                                     NcdId = nd.NcdId,
                                 }).ToListAsync();
                var allergies = await (from ald in _db.AllergiesDetails
                                 join al in _db.Allergies on ald.AllergyId equals al.Id
                                 where ald.PatientId == pat.Id
                                 select new VmAllergies
                                 {
                                     Id = ald.Id,
                                     Name = al.Name,
                                     AllergyId = ald.AllergyId,
                                 }).ToListAsync();
                pat.Ncds = ncd;
                pat.Allergies = allergies;
                list.Add( pat );
            }
            return list;
        }
        public async Task<VmResponseMessage> DeletePatient(int id)
        {
            try {
                var response = new VmResponseMessage();
                var patient = await _db.Patient.FirstOrDefaultAsync(x => x.Id == id);
                if (patient != null)
                {
                    _db.Remove(patient);
                    await _db.SaveChangesAsync();
                }
                //var ncds = await _db.NcdDetails.Where(x => x.PatientId == id).ToListAsync();
                //foreach (var ncd in ncds)
                //{
                //    var nc = await _db.NcdDetails.FirstOrDefaultAsync(x => x.Id == ncd.Id);
                //    _db.Remove(nc);
                //    await _db.SaveChangesAsync();
                //}
                //var allergies = await _db.AllergiesDetails.Where(x => x.PatientId == id).ToListAsync();
                //foreach (var al in allergies)
                //{
                //    var allergy = await _db.NcdDetails.FirstOrDefaultAsync(x => x.Id == al.Id);
                //    _db.Remove(allergy);
                //    await _db.SaveChangesAsync();
                //}
                response.Type = "Success";
                response.Message = "Successfully Deleted Patient";
                return response;
            }
            catch (Exception ex)
            {
                return new VmResponseMessage
                {
                    Message = ex.Message,
                    Type = "error"
                };
            }
        }

        public async Task<VmPatient> GetPatientById( int id)
        {
            var patient = await (from pt in _db.Patient
                                 join d in _db.Disease on pt.DiseaseId equals d.Id
                                 where pt.Id == id
                                 select new VmPatient
                                 {
                                     Id = pt.Id,
                                     Name = pt.Name,
                                     DiseaseId = d.Id,
                                     DiseaseName = d.Name,
                                     Epilepsy = pt.Epilepsy.ToString(),
                                     Ncds = new List<VmNcds>(),
                                     Allergies = new List<VmAllergies>()
                                 }).FirstOrDefaultAsync();
            
                var ncd = await (from nd in _db.NcdDetails
                                 join n in _db.Ncds on nd.NcdId equals n.Id
                                 where nd.PatientId == patient.Id
                                 select new VmNcds
                                 {
                                     Id = nd.Id,
                                     Name = n.Name,
                                     NcdId = nd.NcdId,
                                 }).ToListAsync();
                var allergies = await (from ald in _db.AllergiesDetails
                                       join al in _db.Allergies on ald.AllergyId equals al.Id
                                       where ald.PatientId == patient.Id
                                       select new VmAllergies
                                       {
                                           Id = ald.Id,
                                           Name = al.Name,
                                           AllergyId = ald.AllergyId,
                                       }).ToListAsync();
            patient.Ncds = ncd;
            patient.Allergies = allergies;
            
            return patient;
        }
        public async Task<VmResponseMessage> UpdatePatient(VmPatient vm)
        {
            var response = new VmResponseMessage();
            var patient = await _db.Patient.FirstOrDefaultAsync(x=> x.Id == vm.Id);
            if (patient != null)
            {
                patient.Name = vm.Name;
                patient.DiseaseId = vm.DiseaseId;
                patient.Epilepsy = (Epilepsy)Enum.Parse(typeof(Epilepsy), vm.Epilepsy);
            }
            _db.Update(patient);
            await _db.SaveChangesAsync();

            var ncds = await _db.NcdDetails.Where(x=> x.PatientId == vm.Id).ToListAsync();
            var allergies = await _db.AllergiesDetails.Where(x => x.PatientId == vm.Id).ToListAsync();
            foreach( var item in ncds) {
                _db.Remove(item);
                await _db.SaveChangesAsync();
            }
            foreach (var item in allergies)
            {
                _db.Remove(item);
                await _db.SaveChangesAsync();
            }
            var ncdIds = vm.NcdIds.Split(',').ToList();
            var allergiesIds = vm.AllergyIds.Split(',').ToList();

            var ncdList = new List<NcdDetails>();
            foreach (var nc in ncdIds)
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

            var alleryList = new List<AllergiesDetails>();
            foreach (var al in allergiesIds)
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
            response.Message = "Successfully Updated...!";
            return response;
        }
    }
    
}
