using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class StringSpliter
    {
        public static List<string> StringToList(string data)
        {
            return string.IsNullOrEmpty(data) ? new List<string>() : data.Split(',').ToList(); 
        }

        public static string ListToString(List<string> data)
        {
            return string.Join(",",data);
        }
    }
}
