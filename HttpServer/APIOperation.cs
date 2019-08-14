using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using Newtonsoft.Json.Linq;
namespace HttpServer
{
    class APIOperation
    {
        [MethodType("POST","year")]
        public string Year(string year)
        {
            if (Int32.Parse(year) % 4 == 0)
                return "Leap year";
            else
                return "Normal Year";
        }

        [MethodType("POST", "age")]
        public string Age(string age)
        {
            if (Int32.Parse(age)  >=18)
                return "Eligible To Vote";
            else
                return "Not Eligible To Vote";
        }

        [MethodType("POST", "marks/iu")]
        public string Marks(string marks)
        {
            if (Int32.Parse(marks) >= 100)
                return "Pass";
            else
                return "Fail";
        }
    }


    //below line is used to limit the target types to which this custom attribute could be applied
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodTypeAttribute : Attribute
    {
    
        public MethodTypeAttribute(string Type, string Method)
        {
            this.Type = Type;
            this.Method = Method;
        }

        public string Type { get; set; }
        public string Method { get; set; }
    }

    public class MethodTypeHelper
    {
        public string GetMethod(string httptype ,string method)
        {
            foreach (var prop in typeof(APIOperation).GetMethods())
            {
                var attrs = (MethodTypeAttribute[])prop.GetCustomAttributes
                    (typeof(MethodTypeAttribute), false);
                foreach (var attr in attrs)
                {
                   
                    if (attr.Type == httptype && method == attr.Method)
                        return prop.Name;
                }
            }
            return "No Such Method";
        }
    }
}
