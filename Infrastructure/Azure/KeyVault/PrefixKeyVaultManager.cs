using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Infrastructure.Azure.KeyVault
{
    public class PrefixKeyVaultManager : IKeyVaultSecretManager
    {
        private readonly string _prefix;

        public PrefixKeyVaultManager(string prefix)
        {
            _prefix = $"{prefix}-";
        }

        public string GetKey(SecretBundle secret)
        {
            return secret.SecretIdentifier.Name.Substring(_prefix.Length)
                .Replace("--", ConfigurationPath.KeyDelimiter);
        }

        public bool Load(SecretItem secret)
        {
            return secret.Identifier.Name.StartsWith(_prefix);
        }
    }
}
