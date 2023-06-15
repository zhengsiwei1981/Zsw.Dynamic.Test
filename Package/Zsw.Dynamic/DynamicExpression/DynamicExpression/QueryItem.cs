using System;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic
{
	public class QueryItem
	{
		public string Name
		{
			get;
			set;
		}

		public object Value
		{
			get;
			set;
		}

		internal bool CanCreate
		{
			get;
			set;
		}

		public IMethod Method
		{
			get;
			set;
		}

		public QueryItem(string _name, object _value, IMethod _method)
		{
			SetProperty(_name, _value, _method);
			SetCanCreate();
		}

		public QueryItem(string _name, string _ParamName, IMethod _method, Type vType)
		{
		}

		private void SetProperty(string _name, object _value, IMethod _method)
		{
			Name = _name;
			Value = _value;
			Method = _method;
		}

		private void SetCanCreate()
		{
			if (Value == null || Value.ToString().Length < 1)
			{
				CanCreate = false;
			}
			else
			{
				CanCreate = true;
			}
		}
	}
}
