using Autofac;
using System.Linq;
using System.Reflection;

namespace WebAPI.autofac {
	public class AutofacRegisterModule : Autofac.Module {

		/// <summary>
		/// 依赖注入
		/// 
		/// 符合条件的类将由Autofac创建对象 并添加到IOC容器中
		/// </summary>
		/// <param name="builder"></param>
		protected override void Load(ContainerBuilder builder) {
			builder.RegisterAssemblyTypes(Assembly.Load("WebAPI"))
				.Where(a => a.Name.EndsWith("SQL") || a.Name.EndsWith("Service")).AsImplementedInterfaces();
		}
	}
}
