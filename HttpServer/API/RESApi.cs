namespace HttpServer
{
    public class RESApi
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
