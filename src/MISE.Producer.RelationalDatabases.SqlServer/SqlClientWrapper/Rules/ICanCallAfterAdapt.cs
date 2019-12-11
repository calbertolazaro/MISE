using System.Data.Common;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
	public interface ICanCallAfterAdapt
	{
		ICanCallAfterUsingThisMapping UsingThisMapping(DataTableMapping tableMapping);
	}
}
