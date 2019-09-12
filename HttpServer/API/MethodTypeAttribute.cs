using System;
namespace HttpServer
{
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
}
