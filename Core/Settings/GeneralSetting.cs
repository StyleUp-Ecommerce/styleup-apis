using CleanBase.Core.Settings;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Core.Settings
{
    public class GeneralSetting : AppSettings<GeneralSetting>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public AppENV AppEnv { get; set; } = AppENV.DEV;

    }
}
