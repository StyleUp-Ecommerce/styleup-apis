using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace Infrastructure.Extensions
{
    public class JwtUtil
    {
        private readonly IJwtDecoder _decoder;
        private readonly byte[] _secrectKey;

        public JwtUtil(string key)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA512Algorithm();
            _decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            _secrectKey = urlEncoder.Decode(key);
        }

        public static JwtUtil Instance { get; set; }

        public string ValidateToken(string token)
        {
            return _decoder.Decode(token, _secrectKey, true);
        }

        public static void Init(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("DTMS secret key should not null or empty");
            }
            Instance = new JwtUtil(secretKey);
        }
    }
}
