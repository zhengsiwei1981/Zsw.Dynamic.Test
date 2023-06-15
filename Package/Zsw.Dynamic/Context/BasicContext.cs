using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Zsw.Dynamic.ContextInterface;

namespace Zsw.Dynamic.Context
{
    public class BasicContext
    {
        /// <summary>
        /// 是否HTTP请求
        /// </summary>
        public bool IsHttpRequest
        {
            get
            {
                return HttpContext.Current != null;
            }
        }
        /// <summary>
        /// 是否json
        /// </summary>
        public bool IsJson
        {
            get
            {
                return HttpContext.Current.Request.Headers["Content-Type"] == "application/json";
            }
        }
        /// <summary>
        /// 对象名称
        /// </summary>
        public string EntityName
        {
            get;set;
        }
        ///// <summary>
        ///// 对象前缀
        ///// </summary>
        //public string EntityPrefix
        //{
        //    get;set;
        //}
        /// <summary>
        /// 调用目标
        /// </summary>
        public IDynamicProvider Provder
        {
            get;set;
        }
        /// <summary>
        /// 调用方
        /// </summary>
        public object Parent
        {
            get;set;
        }
    }
}
