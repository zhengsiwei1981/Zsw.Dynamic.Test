using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;

namespace Zsw.Dynamic
{
    internal static class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        public static void ObjectFill(this object obj, object source)
        {
            var jobject = source as JObject;

            if (jobject != null)
            {
                var jpList = jobject.Properties().ToList();
                jpList.ForEach(jp =>
                {
                    var p = obj.GetType().GetProperty(jp.Name);
                    if (p != null)
                    {
                        Type valType = p.PropertyType.IsGenericType ? p.PropertyType.GetGenericArguments()[0] : p.PropertyType;
                        var jobjectValueMethod = jobject.GetType().GetMethods().Where(m => m.Name == "Value" && m.GetParameters().Count() == 1).FirstOrDefault();
                        if (jobjectValueMethod != null)
                        {
                            var value = jobjectValueMethod.MakeGenericMethod(valType).Invoke(jobject, new object[] { jp.Name });
                            p.SetValue(obj, value);
                        }
                    }
                });
            }
            else
            {
                throw new Exception("只能序列化JObject对象！");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="action"></param>
        public static void InterfaceExectue<T>(this string target, Action<T> action) where T : class
        {
            if (!string.IsNullOrEmpty(target))
            {
                var objs = target.Split(new char[] { ',' });
                objs.ToList().ForEach(obj =>
                {
                    T instance = null;
                    var isFindedInstance = false;
                    foreach (var assembly in Global.AssemblyNameList)
                    {
                        var instancePath = string.Format("{0}.{1}", assembly, obj);
                        instance = Assembly.Load(assembly).CreateInstance(instancePath) as T;
                        if (instance != null)
                        {
                            action(instance);
                            isFindedInstance = true;
                            break;
                        }
                    }
                    if (!isFindedInstance)
                    {
                        var entryAssembly = Assembly.GetEntryAssembly();
                        if (entryAssembly != null)
                        {
                            instance = entryAssembly.CreateInstance(entryAssembly.GetName().Name + "." + obj) as T;
                            if (instance != null)
                            {
                                action(instance);
                            }
                        }
                    }
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="entityName"></param>
        public static object CreateEntity(this object obj, string entityName)
        {
            var providerPath = obj.GetType().Namespace;
            var entityPath = string.Format("{0}.{1}", providerPath, entityName);
            return obj.GetType().Assembly.CreateInstance(entityPath);
        }
    }
}
