using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Weigou.Api
{
    /// <summary>  
    /// 反射处理类  
    /// </summary>  
    public class AssemblyHandler
    {
        /// <summary>  
        /// 获取程序集中的类名称  
        /// </summary>  
        /// <param name="assemblyName">程序集</param>  
        public static AssemblyResult GetClass()
        {
            AssemblyResult result = new AssemblyResult();
            //获取包含当前执行的代码的程序集。
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] ts = assembly.GetTypes();
            List<string> classList = new List<string>();
            foreach (Type t in ts)
            {
                classList.Add(t.FullName);
            }
            classList.Sort();
            result.ClassName = classList;
            return result;
        }

        /// <summary>  
        /// 获取类的属性、方法  
        /// </summary>  
        /// <param name="assemblyName">程序集</param>  
        /// <param name="className">类名</param>  
        public static AssemblyResult GetClassInfo(string className)
        {
            AssemblyResult result = new AssemblyResult();
            //获取包含当前执行的代码的程序集。
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetType(className, true, true);
            if (type != null)
            {
                //类的属性  
                List<string> propertieList = new List<string>();
                PropertyInfo[] propertyinfo = type.GetProperties(BindingFlags.Public );
                foreach (PropertyInfo p in propertyinfo)
                {
                    propertieList.Add(p.ToString());
                }
                result.Properties = propertieList;

                //类的方法  
                List<string> methods = new List<string>();
                List<string> methodDesc = new List<string>();
                MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                foreach (MethodInfo mi in methodInfos)
                {
                    methods.Add(mi.Name);
                    object[] desc = mi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (desc != null && desc.Length > 0)
                    {
                        string[] arr = ((DescriptionAttribute)desc[0]).Description.Split('&');
                        Hashtable hs = new Hashtable();
                        foreach (string s in arr)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                string[] kv = s.Split('=');
                                hs.Add(kv[0].Trim(), kv[1].Trim());
                            }
                        }
                        methodDesc.Add(JsonConvert.SerializeObject(hs));
                    }
                    else
                    {
                        methodDesc.Add("");
                    }
                }
                result.Methods = methods;
                result.Desc = methodDesc;
            }
            return result;
        }
    }
}