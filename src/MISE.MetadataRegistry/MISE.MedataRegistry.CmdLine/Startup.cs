using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MISE.MetadataRegistry.CommandLine.Extensions;
using MISE.MetadataRegistry.Infrastructure;
using MISE.SharedKernel.Extensions.DependencyInjection;

namespace MISE.MedataRegistry.UI.Console
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRegistryServices(Configuration);

            string connectionString = Configuration.GetSection("Registry:ConnectionStrings:Default").Value;
            services.AddDbContext(connectionString);

            services.UseAppLogger();
        }
    }
    
}
