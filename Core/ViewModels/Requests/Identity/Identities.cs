using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Identity
{
   public class Identities
    {
        [JsonPropertyName("google.com")]
        public List<string> Google { get; set; }

        [JsonPropertyName("email")]
        public List<string> Email { get; set; }
    }
}
