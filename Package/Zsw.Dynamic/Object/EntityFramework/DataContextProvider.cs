using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.Object.LinqToSql;
using Zsw.Dynamic.Object.LinqToSql.Query;
using Zsw.Dynamic.Query.Interface;

namespace Zsw.Dynamic.Object.EntityFramework
{
    public class DataContextProvider : IDynamicProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public DataContextProvider()
        {
            if (Context == null)
            {
                this.Create();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal DbContext Context
        {
            get;set;
        }
        /// <summary>
        /// 
        /// </summary>
        public void ConnectionClose()
        {
            this.Context.Database.Connection.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ConnectionOpen()
        {
            this.Context.Database.Connection.Open();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Create()
        {
            this.Context = (DbContext)Activator.CreateInstance(Global.Configration.GetCurrentDatabase());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IGet CreateGetWrapper()
        {
            return new Get();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IToList CreateListWrapper()
        {
            return new List();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ITable GetTable(string name)
        {
            var tablePropertyInfo = this.Context.GetType().GetProperty(name);
            return new Table(tablePropertyInfo, this.Context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITable> GetTables()
        {
            var tableProperties = this.Context.GetType().GetProperties().Where(p => p.PropertyType.Name == "Table`1").ToList();
            var tables = new List<ITable>();
            tableProperties.ForEach(info =>
            {
                tables.Add(new Table(info, this.Context));
            });
            return tables;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Submit()
        {
            this.Context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ToObject()
        {
            return this.Context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITransaction Transaction()
        {
            return new Transcation() { TransactionInstance = this.Context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable).UnderlyingTransaction};
        }
    }
}
