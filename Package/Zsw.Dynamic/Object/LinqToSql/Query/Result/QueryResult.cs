using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Object.LinqToSql.Query.Result
{
    public class QueryResult : IQueryResult
    {
        public object Data { get; set; }
        public int TotalRecord { get; set; }
        public int Index { get; set; }
        public int TotalPageNumber {
            get
            {
                if (TotalRecord == 0)
                {
                    return 0;
                }
                if (TotalRecord <= this.EachPageNumber)
                {
                    return 1;
                }
                var left = TotalRecord % EachPageNumber;
                if (left > 0)
                {
                    return (TotalRecord - left) / EachPageNumber + 1;
                }
                return TotalRecord / EachPageNumber;
            }
        }
        public int EachPageNumber { get; set; }
    }
}
