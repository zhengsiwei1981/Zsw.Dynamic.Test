using System;
using System.ComponentModel;
using System.Reflection;

namespace Zsw.Dynamic
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtenssion
    {
        /// <summary>
        /// 获取枚举数值(int)
        /// </summary>
        /// <param name="enumObj">枚举值</param>
        /// <returns></returns>
        public static int GetValue(this Enum enumObj)
        {
            return Convert.ToInt32(enumObj);
        }

        /// <summary>
        /// 获取枚举数值(string)
        /// </summary>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum enumObj)
        {
            return GetValue(enumObj).ToString();
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumObj)
        {
            Type type = enumObj.GetType();
            FieldInfo info = type.GetField(enumObj.ToString());
            DescriptionAttribute descriptionAttribute = info.GetCustomAttributes(typeof(DescriptionAttribute), true)[0] as DescriptionAttribute;
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;
            else
                return type.ToString();
        }

        /// <summary>
        /// 获取枚举描述(Format版本)
        /// </summary>
        /// <param name="enumObj"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumObj, string text)
        {
            return string.Format(GetDescription(enumObj), text);
        }
    }
}
