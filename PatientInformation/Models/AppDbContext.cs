using Microsoft.EntityFrameworkCore;

namespace PatientInformation.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        public DbSet<Disease> Disease { get; set; }
        public DbSet<Allergies> Allergies { get; set; }
        public DbSet<Ncds> Ncds { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<NcdDetails> NcdDetails { get; set; }
        public DbSet<AllergiesDetails> AllergiesDetails { get; set; }
    }
}
