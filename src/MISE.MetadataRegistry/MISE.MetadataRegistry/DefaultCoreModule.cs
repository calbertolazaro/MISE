using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISE.MetadataRegistry.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<DailyDataSetsSearchService>()
            //.As<IDailyDataSetsSearchService>().InstancePerLifetimeScope();
        }
    }
}
