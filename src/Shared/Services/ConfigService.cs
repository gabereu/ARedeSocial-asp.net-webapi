using Microsoft.Extensions.Configuration;

namespace dotnetServer.Shared.Services
{
    public class ConfigService
    {
        public readonly string JwtSecrete;
        public static string JwtSecreteFieldName = "token_secret";

        public ConfigService(IConfiguration configuration)
        {
            JwtSecrete = configuration.GetValue<string>(ConfigService.JwtSecreteFieldName);
        }
    }
}