namespace Infrastructure.Commons
{

    public class AppConfig
    {
        public IdentityServerConfig IdentityServerConfig { get; set; }
        public EmailServiceConfig EmailServiceConfig { get; set; }
        public FrontendConfig FrontendConfig { get; set; }
        public JwtConfig JwtConfig { get; set; }
    }

    public class IdentityServerConfig
    {
        public IdentityServerClients Clients { get; set; }
        public required string Authority { get; set; }
        public required string IssuerUri { get; set; }
    }

    public class IdentityServerClients
    {
        public InternalClient Web { get; set; }
        public InternalClient Mobile { get; set; }
        public ExternalClient GoogleWeb { get; set; }
    }

    public class InternalClient
    {
        public required string Secret { get; set; }
    }

    public class ExternalClient
    {
        public required string InternalSecret { get; set; }
        public required string ExternalClientId { get; set; }
        public required string ExternalClientSecret { get; set; }
        public required string RedirectUri { get; set; }
        public required string PostLogoutRedirectUri { get; set; }
        public required string AllowedCorsOrigin { get; set; }
    }

    public class EmailServiceConfig
    {
        public required string ConnectionString { get; set; }
        public required string Sender { get; set; }
    }

    public class FrontendConfig
    {
        public string Url { get; set; }
    }

    public class JwtConfig
    {
        public required string SigningKey { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public string Modulus { get; set; }
        public string Exponent { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }


}
