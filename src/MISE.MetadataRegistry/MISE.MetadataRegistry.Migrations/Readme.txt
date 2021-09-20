== CONFIGURAÇÃO DO ENTITY FRAMEWORK == 
Install Microsoft.EntityFrameworkCore.SqlServer
install Microsoft.EntityFrameworkCore.Tools (usado pelo Package Manager) no UI

== MIGRACOES EM ASSEMBLY ISOLADO == 
Para configurar as migraçoes numa assembly diferente, definir (módulo de infrastructura)
o MigrationsAssembly.

	public static class StartupSetup
	{
		public static void AddDbContext(this IServiceCollection services, string connectionString) =>
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString, b=> b.MigrationsAssembly("MISE.MetadataRegistry.Migrations"))); 
	}


== COMANDOS == (Default project: src\MISE.MetadataRegistry.Migrations)
Help: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=vs
PM> Add-Migration InitialSchemaCreation (to undo use Remove-Migration)
PM> Script-Migration
