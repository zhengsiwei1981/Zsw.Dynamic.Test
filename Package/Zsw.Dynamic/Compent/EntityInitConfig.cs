using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsw.Dynamic.Compent
{
    public class EntityInitConfig
    {
        public EntityInitConfig(bool _isGlobal = false)
        {
            this.initFields = new List<InitField>();
            this.IsGlobal = _isGlobal;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsGlobal
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Type EntityType
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal List<InitField> initFields
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initField"></param>
        public void AddField(InitField initField)
        {
            this.initFields.Add(initField);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public bool Match(Type entityType)
        {
            return this.EntityType == entityType;
        }
        /// <summary>
        /// 
        /// </summary>
        public void AssignValue(object obj)
        {
            this.initFields.ForEach(field =>
            {
                field.Instance = obj;
                field.SetValue();
            });
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class InitField
    {
        internal object Instance
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Func<object> ProviderValue
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal void SetValue()
        {
            if (this.ProviderValue == null)
            {
                return;
            }
            var property = Instance.GetType().GetProperty(this.Name);
            if (property != null)
            {
                if (property.PropertyType.IsGenericType == false)
                {
                    property.SetValue(this.Instance, Convert.ChangeType(this.ProviderValue(), property.PropertyType));
                }
                else
                {
                    property.SetValue(this.Instance, Convert.ChangeType(this.ProviderValue(), property.PropertyType.GetGenericArguments()[0]));
                }
            }
        }
    }
}
