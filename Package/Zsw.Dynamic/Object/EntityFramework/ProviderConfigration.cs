using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.PipeLine.Command;
using Zsw.Dynamic.PipeLine.Query;

namespace Zsw.Dynamic.Object.EntityFramework
{
    public class ProviderConfigration : IProviderConfigration
    {
        internal Type CurrentDataBaseType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDynamicProvider AssignDatabaseProvider()
        {
            return new DataContextProvider();
        }
        /// <summary>
        /// 
        /// </summary>
        public IExpressionGenerate AssignExpressionGenerater()
        {
            return new DefaultExpressonGenerater();
        }
        /// <summary>
        /// 
        /// </summary>
        public IOrderExpressionGenerate AssignOrderExpressionGenerater()
        {
            return new DefaultOrderExpressionGenerater();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IPipeLineCompent AssignQueryPipeLineCompent()
        {
            return new PipeLine.Query.PipeLineCompent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommandPipeLineCompent AssignCommandPipeLineCompent()
        {
            return new CommandPipeLineCompent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_prefile"></param>
        /// <returns></returns>
        public Type ChangeDatabase(object _prefile)
        {
            this.CurrentDataBaseType = (Type)_prefile;
            return this.CurrentDataBaseType;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Type GetCurrentDatabase()
        {
            return this.CurrentDataBaseType;
        }
    }
}
