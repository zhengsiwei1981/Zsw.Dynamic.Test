using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsw.Dynamic.Result
{
    public interface IQueryResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        object Data
        {
            get;set;
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalRecord
        {
            get;set;
        }
        /// <summary>
        /// 当前索引
        /// </summary>
        int Index
        {
            get;set;
        }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPageNumber
        {
            get;
        }
        /// <summary>
        /// 每页记录数
        /// </summary>
        int EachPageNumber
        {
            get;set;
        }
    }
}
