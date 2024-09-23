using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class StringSpliter
    {
        public static List<string> SplitImageString(string images)
        {
            return string.IsNullOrEmpty(images) ? new List<string>() : images.Split(',').ToList(); 
        }

        public static string MergeImageString(List<string> images)
        {
            return string.Join(",",images);
        }
    }
}
