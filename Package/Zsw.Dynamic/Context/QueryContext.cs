using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using Zsw.Dynamic.Paramers;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Context
{
    public class QueryContext : BasicContext
    {
        /// <summary>
        /// 
        /// </summary>
        public PageRequestParamer requestParamer
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Zsw.Dynamic.ContextInterface.ITable Table
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal IQueryable Queryable
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public IQueryResult Result
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public QueryType QueryType
        {
            get;set;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum QueryType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("列表")]
        List = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("获取")]
        Get = 1
    }
}
