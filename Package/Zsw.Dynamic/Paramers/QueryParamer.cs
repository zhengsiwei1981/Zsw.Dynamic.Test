using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsw.Dynamic.Paramers
{
    public class QueryParamer
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// 输入值
        /// </summary>
        public object Value
        {
            get; set;
        }
        /// <summary>
        /// 比较方式：1 =，2 > ,3 >= ,4 < ,5<= , 6 包含,7 !=
        /// </summary>
        public int Method
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOr
        {
            get; set;
        }
    }
}
