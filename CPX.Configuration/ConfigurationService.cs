using CPX.Configuration.Contract;

namespace CPX.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationBuilder builder;

        public ConfigurationService(IConfigurationBuilder builder)
        {
            this.builder = builder;
        }

        public string Get(string key) => builder.Build().GetSection(key).Value;

    }
}
