using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AndrewLarsson.Common.DependencyInjection {
	public static class AutomaticAddServiceCollectionExtensions {
		public static IServiceCollection AddTransientGenericTypeDefinition(this IServiceCollection serviceCollection, Type genericType, Assembly assembly) {
			IEnumerable<Type> types = assembly.GetTypes().Where(t => t.IsConcreteClass() && t.ImplementsGenericInterface(genericType));
			foreach (Type type in types) {
				IEnumerable<Type> interfaceTypes = type.GetGenericInterfaces(genericType);
				foreach (Type interfaceType in interfaceTypes) {
					serviceCollection.AddTransient(interfaceType, type);
				}
			}
			return serviceCollection;
		}
	}
}
