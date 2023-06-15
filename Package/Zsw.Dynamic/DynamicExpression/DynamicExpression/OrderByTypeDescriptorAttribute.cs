using System;

namespace Zsw.Dynamic
{
	[AttributeUsage(AttributeTargets.Field)]
	public class OrderByTypeDescriptorAttribute : Attribute
	{
		public string MethodMap
		{
			get;
			set;
		}
	}
}
