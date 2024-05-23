using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientInformation.IRepository;
using PatientInformation.Models;
using PatientInformation.ViewModel;

namespace PatientInformation.Repository
{
    public class DropDownRepository: IDropDownRepository
    {
        private readonly AppDbContext _db;
        public DropDownRepository( AppDbContext db)
        {
            _db = db;
        }
        public async Task<VmResponseMessage> CreateDisease(VmParam vm)
        {
            if (vm == null)
            {
                return new VmResponseMessage();
            }
            else
            {
                var model = new Disease
                {
                    Name = vm.Name,
                };
                await _db.AddAsync(model);
                await _db.SaveChangesAsync();
                return new VmResponseMessage
                {
                    Type = "Success",
                    Message = "Successfully Saved...!"
                };
            }
        }
        public async Task<VmResponseMessage> CreateNcd(VmParam vm)
        {
            if (vm == null)
            {
                return new VmResponseMessage();
            }
            else
            {
                var model = new Ncds
                {
                    Name = vm.Name,
                };
                await _db.AddAsync(model);
                await _db.SaveChangesAsync();
                return new VmResponseMessage
                {
                    Type = "Success",
                    Message = "Successfully Saved...!"
                };
            }
        }
        public async Task<VmResponseMessage> CreateAllergies(VmParam vm)
        {
            if (vm == null)
            {
                return new VmResponseMessage();
            }
            else
            {
                var model = new Allergies
                {
                    Name = vm.Name,
                };
                await _db.AddAsync(model);
                await _db.SaveChangesAsync();
                return new VmResponseMessage
                {
                    Type = "Success",
                    Message = "Successfully Saved...!"
                };
            }
        }
        public async Task<List<VmDropDown>> GetAllergies()
        {
            var list = await _db.Allergies.Select(s => new VmDropDown
            {
                Id = s.Id.ToString(),
                Name = s.Name,
            }).ToListAsync();
            return list;
        }
        public async Task<List<VmDropDown>> GetNcds()
        {
            var list = await _db.Ncds.Select(s => new VmDropDown
            {
                Id = s.Id.ToString(),
                Name = s.Name,
            }).ToListAsync();
            return list;
        }
        public async Task<List<VmDropDown>> GetDisease()
        {
            var list = await _db.Disease.Select(s => new VmDropDown
            {
                Id = s.Id.ToString(),
                Name = s.Name,
            }).ToListAsync();
            return list;
        }
    }
}
