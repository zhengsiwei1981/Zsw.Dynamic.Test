using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.ContextInterface;

namespace Zsw.Dynamic.Object.LinqToSql
{
    internal class Transcation : ITransaction
    {
        /// <summary>
        /// 
        /// </summary>
        internal DbTransaction TransactionInstance
        {
            get;set;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            this.TransactionInstance.Commit();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Rallback()
        {
            this.TransactionInstance.Rollback();
        }
    }
}
