using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;

namespace Bifrost.Common
{
	/// <summary>
	/// Replacement of System.Web.UI.PropertyConverter class
	/// </summary>
	public sealed class XPropertyConverter
	{
		internal static object InvokeMethod(MethodInfo methodInfo, object obj, object[] parameters)
		{
			try
			{
				object local = methodInfo.Invoke(obj, parameters);
				return local;
			}
			catch (TargetInvocationException e)
			{
				throw e.InnerException;
			}
		}

		private static Type[] s_parseMethodTypes = new Type[] { typeof(string) };

		private static Type[] s_parseMethodTypesWithSOP = new Type[] { typeof(string), typeof(IServiceProvider) };

		public static object EnumFromString(Type enumType, string value)
		{
			object local;

			try
			{
				local = Enum.Parse(enumType, value, true);
			}
			catch (Exception)
			{
				local = null;
			}
			return local;
		}

		public static string EnumToString(Type enumType, object enumValue)
		{
			return Enum.Format(enumType, enumValue, "G").Replace('_', '-');
		}

		public static object ObjectFromString(Type objType, MemberInfo propertyInfo, string value)
		{
			if (value == null)
			{
				return null;
			}
			if (objType.Equals(typeof(bool)) && value.Length == 0)
			{
				return null;
			}
			if (objType.IsEnum)
			{
				return EnumFromString(objType, value);
			}
			if (objType.Equals(typeof(string)))
			{
				return value;
			}

			PropertyDescriptor propertyDescriptor = null;
			if (propertyInfo != null)
			{
				propertyDescriptor = TypeDescriptor.GetProperties(propertyInfo.ReflectedType)[propertyInfo.Name];
			}
			if (propertyDescriptor != null)
			{
				TypeConverter typeConverter = propertyDescriptor.Converter;
				if (typeConverter != null && typeConverter.CanConvertFrom(typeof(string)))
				{
					return typeConverter.ConvertFromInvariantString(value);
				}
			}
			MethodInfo methodInfo = objType.GetMethod("Parse", s_parseMethodTypesWithSOP);
			object local = null;
			if (methodInfo != null)
			{
				object[] locals1 = new object[] { value, CultureInfo.InvariantCulture };
				try
				{
					local = InvokeMethod(methodInfo, null, locals1);
				}
				catch
				{
				}
			}
			else
			{
				methodInfo = objType.GetMethod("Parse", s_parseMethodTypes);
				if (methodInfo != null)
				{
					object[] locals2 = new object[] { value };
					try
					{
						local = InvokeMethod(methodInfo, null, locals2);
					}
					catch
					{
					}
				}
			}
			if (local == null)
			{
				throw new Exception(string.Format("Type_not_creatable_from_string {0} {1}", objType.FullName, value, propertyInfo.Name));
			}
			else
			{
				return local;
			}
		}
	}
}
