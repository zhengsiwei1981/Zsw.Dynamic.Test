using System;
using System.ComponentModel;
using System.Linq;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic.InterfaceObject
{
    /// <summary>
    /// 
    /// </summary>
    public enum MethodEnum
    {
        [MethodTypeDescripter(typeof(Equal))]
        [Description("Equal")]
        Equal = 1,
        [MethodTypeDescripter(typeof(GreaterThan))]
        [Description("GreaterThan")]
        GreaterThan = 2,
        [MethodTypeDescripter(typeof(GreaterThanOrEqual))]
        [Description("GreaterThanOrEqual")]
        GreaterThanOrEqual = 3,
        [MethodTypeDescripter(typeof(LessThan))]
        [Description("LessThan")]
        LessThan = 4,
        [MethodTypeDescripter(typeof(LessThanOrEqual))]
        [Description("LessThanOrEqual")]
        LessThanOrEqual = 5,
        [MethodTypeDescripter(typeof(Like))]
        [Description("Like")]
        Like = 6,
        [MethodTypeDescripter(typeof(NotEqual))]
        [Description("NotEqual")]
        NotEqual = 7,
        [MethodTypeDescripter(typeof(In))]
        [Description("In")]
        In = 8
    }
    /// <summary>
    /// 
    /// </summary>
    public class MethodTypeDescripterAttribute : Attribute
    {
        public Type MethodType
        {
            get; set;
        }
        public MethodTypeDescripterAttribute(Type methodType)
        {
            this.MethodType = methodType;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class MethodFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static IMethod CreateMethodEntity(int method)
        {
            if (method == 0)
            {
                return null;
            }
            MethodEnum methodEnum;
            if (Enum.TryParse<MethodEnum>(method.ToString(), out methodEnum))
            {
                var field = methodEnum.GetType().GetField(methodEnum.GetDescription());
                var typeDescripter = (MethodTypeDescripterAttribute)field.GetCustomAttributes(typeof(MethodTypeDescripterAttribute), false).FirstOrDefault();
                if (typeDescripter != null)
                {
                    return (IMethod)Activator.CreateInstance(typeDescripter.MethodType);
                }
            }
            else
            {
                throw new Exception("无效的枚举值！");
            }
            return null;
        }
    }
}
