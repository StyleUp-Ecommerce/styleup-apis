using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Azure.KeyVault
{
    public static class KeyVaultConfiguration
    {
        public static void ConfigureKeyVault(IConfigurationBuilder config, IConfiguration configuration)
        {
            var keyVaultName = configuration["KeyVaultName"];
            var keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";
            var credential = new DefaultAzureCredential();

            config.AddAzureKeyVault(new Uri(keyVaultUri), credential);
        }
    }
}
