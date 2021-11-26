using Autofac;
using System.Linq;
using System.Reflection;

namespace WebAPI.autofac {
	public class AutofacRegisterModule : Autofac.Module {

		protected override void Load(ContainerBuilder builder) {
			builder.RegisterAssemblyTypes(Assembly.Load("WebAPI"))
				.Where(a => a.Name.EndsWith("SQL") || a.Name.EndsWith("Service")).AsImplementedInterfaces();
		}
	}
}
