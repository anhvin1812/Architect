using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Architect.Data.EntityFramework
{
    public class EntityFrameworkAutoFacModule: Module
    {
        private const string MkConnectionName = "MinhKhangStore";

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MkDataContext(MkConnectionName)).As<IMkDataContext>().InstancePerLifetimeScope();


            NamedParameter mkDataConextParam  =new NamedParameter("nameOrConnectionString", MkConnectionName);
            builder.RegisterType<MkDataContext>()
                .WithParameter(mkDataConextParam)
                .As<IDbContext>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
