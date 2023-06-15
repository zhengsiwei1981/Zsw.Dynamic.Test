using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsw.Dynamic.Paramers
{
    /// <summary>
    /// 
    /// </summary>
    public class OperationParamers
    {
        /// <summary>
        /// 
        /// </summary>
        public string EntityName
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Validation
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Executing
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Executed
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> Ids
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public JObject obj
        {
            get; set;
        }
    }
}
