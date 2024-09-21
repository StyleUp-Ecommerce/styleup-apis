namespace Core.Constants
{
    public class Languages
    {
        public static readonly string VI_VN = "vi-vn";
        public static readonly string EN_US = "en-us";

        public readonly HashSet<string> SupportLangs = new HashSet<string>()
        {
            VI_VN,
            EN_US
        };

        public bool IsSupported(string langCode)
        {
            return SupportLangs.Contains(langCode);
        }
    }
}
