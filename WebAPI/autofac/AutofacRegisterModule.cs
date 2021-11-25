using Autofac;
using System.Linq;
using System.Reflection;
using WebAPI.utils;

namespace WebAPI.autofac {
	public class AutofacRegisterModule : Autofac.Module {

		protected override void Load(ContainerBuilder builder) {
			builder.RegisterAssemblyTypes(Assembly.Load("WebAPI"))
				.Where(a => a.Name.EndsWith("SQL") || a.Name.EndsWith("Service")).AsImplementedInterfaces();

			builder.RegisterType<DataSource>().As<IDataSource>().InstancePerDependency().AsImplementedInterfaces();
		}
	}
}
