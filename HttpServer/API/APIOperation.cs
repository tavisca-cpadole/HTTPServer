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
}
