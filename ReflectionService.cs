using System;
using System.Reflection;

namespace Beanfun
{
		public static class ReflectionService
	{
				public static object ReflectGetProperty(this object target, string propertyName)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (string.IsNullOrWhiteSpace(propertyName))
			{
				throw new ArgumentException("propertyName can not be null or whitespace", "propertyName");
			}
			PropertyInfo property = target.GetType().GetProperty(propertyName, ReflectionService.BindingFlags);
			if (property == null)
			{
				throw new ArgumentException(string.Format("Can not find property '{0}' on '{1}'", propertyName, target.GetType()));
			}
			return property.GetValue(target, null);
		}

				public static object ReflectInvokeMethod(this object target, string methodName, Type[] argTypes, object[] parameters)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (string.IsNullOrWhiteSpace(methodName))
			{
				throw new ArgumentException("methodName can not be null or whitespace", "methodName");
			}
			MethodInfo method = target.GetType().GetMethod(methodName, ReflectionService.BindingFlags, null, argTypes, null);
			if (method == null)
			{
				throw new ArgumentException(string.Format("Can not find method '{0}' on '{1}'", methodName, target.GetType()));
			}
			return method.Invoke(target, parameters);
		}

			
		static ReflectionService()
		{
		}

				public static readonly BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.CreateInstance;
	}
}
