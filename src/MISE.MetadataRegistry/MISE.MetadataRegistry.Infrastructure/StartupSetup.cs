using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MISE.MetadataRegistry.Infrastructure.Data;


namespace MISE.MetadataRegistry.Infrastructure
{
	public static class StartupSetup
	{
		public static void AddDbContext(this IServiceCollection services, string connectionString) =>
			services.AddDbContext<MetadataRegistryContext>(options =>
			{
				options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MISE.MetadataRegistry.Migrations"));
			}); 
	}
}
