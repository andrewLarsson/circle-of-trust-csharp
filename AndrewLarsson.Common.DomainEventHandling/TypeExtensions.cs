using System;
using System.Collections.Generic;
using System.Linq;

namespace AndrewLarsson.Common.Host {
	public static class TypeExtensions {
		public static bool IsConcreteClass(this Type type)
			=> IsClass(type) && !IsAbstract(type) && !IsSealed(type);

		public static bool ImplementsGenericInterface(this Type type, Type interfaceType)
			=> IsGenericInterface(interfaceType) && GetGenericInterfaces(type).Any(t => EqualsGeneric(t, interfaceType));

		public static IEnumerable<Type> GetGenericInterfaces(this Type type) {
			return GetInterfaces(type).Where(IsGenericType);
		}

		public static Type[] GetInterfaces(this Type type)
			=> type.GetInterfaces();

		public static bool IsGenericType(this Type type)
			=> type.IsGenericType;

		public static bool IsGenericInterface(this Type type)
			=> IsGenericType(type) && IsInterface(type);

		public static bool IsInterface(this Type type)
			=> type.IsInterface;

		public static bool EqualsGeneric(this Type type, Type genericType) {
			if ((type == null) || (genericType == null)) {
				return false;
			}
			if (!(IsGenericType(type) && IsGenericType(genericType))) {
				return false;
			}
			if (IsGenericTypeDefinition(type)) {
				return type == genericType.GetGenericTypeDefinition();
			}
			if (IsGenericTypeDefinition(genericType)) {
				return type.GetGenericTypeDefinition() == genericType;
			}
			return type == genericType;
		}

		public static bool IsGenericTypeDefinition(this Type type)
			=> type.IsGenericTypeDefinition;

		public static bool IsClass(this Type type)
			=> type.IsClass && !IsDelegate(type);

		public static bool IsDelegate(this Type type)
			=> IsClassOrDelegate(type)
				&& IsSealed(type)
				&& !IsAbstract(type)
				&& type != typeof(Delegate)
				&& type != typeof(MulticastDelegate)
				&& IsSubclassOf(type, typeof(Delegate));

		public static bool IsClassOrDelegate(this Type type)
			=> type.IsClass;

		public static bool IsAbstract(this Type type)
			=> type.IsAbstract;

		public static bool IsSealed(this Type type)
			=> type.IsSealed;

		public static bool IsSubclassOf(this Type type, Type other)
			=> (type != null) && type.IsSubclassOf(other);

		public static IEnumerable<Type> GetGenericInterfaces(this Type type, Type interfaceType) {
			if (!IsGenericInterface(interfaceType)) {
				return Enumerable.Empty<Type>();
			}
			return GetGenericInterfaces(type, i => EqualsGeneric(i, interfaceType));
		}

		public static IEnumerable<Type> GetGenericInterfaces(this Type type, Func<Type, bool> predicate) {
			return GetGenericInterfaces(type).Where(predicate);
		}
	}
}
