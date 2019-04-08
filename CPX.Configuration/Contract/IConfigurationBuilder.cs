using Microsoft.Extensions.Configuration;

namespace CPX.Configuration.Contract
{
    public interface IConfigurationBuilder
    {
        IConfigurationRoot Build();
    }
}
