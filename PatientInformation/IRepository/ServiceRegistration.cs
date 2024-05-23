using PatientInformation.Repository;

namespace PatientInformation.IRepository
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddRepoServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDropDownRepository, DropDownRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            return services;
        }
    }
}
