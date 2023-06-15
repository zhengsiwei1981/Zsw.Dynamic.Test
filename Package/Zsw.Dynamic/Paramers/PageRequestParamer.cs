using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsw.Dynamic.Paramers
{
    public class PageRequestParamer
    {
        public string EntityName
        {
            get;set;
        }
        public int Index
        {
            get; set;
        }
        public int EachPageNumber
        {
            get; set;
        }
        public List<QueryParamer> QueryParams
        {
            get; set;
        }
        public List<OrderParamer> Orders
        {
            get; set;
        }
        public string FillMethod
        {
            get; set;
        }
    }
}
