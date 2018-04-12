using System.Reflection;

namespace AndrewLarsson.Common.DependencyInjection {
	public static class AssemblyExtensions {
		public static Assembly ToAssembly(this string assemblyName)
			=> ToAssembly(ToAssemblyName(assemblyName));

		public static Assembly ToAssembly(this AssemblyName assemblyName)
			=> Assembly.Load(assemblyName);

		public static AssemblyName ToAssemblyName(this string assemblyName)
			=> new AssemblyName(assemblyName);
	}
}
