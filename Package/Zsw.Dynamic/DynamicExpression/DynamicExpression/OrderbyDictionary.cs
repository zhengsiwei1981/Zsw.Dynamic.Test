using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zsw.Dynamic;

namespace Zsw.Dynamic
{
	public class OrderbyDictionary : Dictionary<OrderByArgument, OrderByType>
	{
		public KeyValuePair<OrderByArgument, OrderByType> this[string key] => this.FirstOrDefault((KeyValuePair<OrderByArgument, OrderByType> kv) => kv.Key.PropertyName.Equals(key));

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Order by ");
			this.ToList().ForEach(delegate(KeyValuePair<OrderByArgument, OrderByType> kv)
			{
				sb.Append(kv.Key.PropertyName);
				sb.Append(" ");
				sb.Append(kv.Value.ToString());
				sb.Append(" ");
			});
			return sb.ToString();
		}
	}
}
