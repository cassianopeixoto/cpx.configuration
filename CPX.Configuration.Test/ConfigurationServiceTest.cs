using CPX.Configuration.Contract;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CPX.Configuration.Test
{
    public class ConfigurationServiceTest
    {
        [Fact]
        public void ShouldBeAbleToInstanciate()
        {
            // arrange
            var builder = new Mock<Contract.IConfigurationBuilder>(MockBehavior.Strict);
            // act
            var service = new ConfigurationService(builder.Object);
            // assert
            Assert.IsAssignableFrom<IConfigurationService>(service);
        }

        [Fact]
        public void ShouldBeAbleToGetValue()
        {
            // arrange
            var key = "key";
            var value = "value";

            var mockSection = new Mock<IConfigurationSection>(MockBehavior.Strict);
            mockSection.Setup(o => o.Value).Returns(value);

            var mockRoot = new Mock<IConfigurationRoot>();
            mockRoot.Setup(o => o.GetSection(key)).Returns(mockSection.Object);

            var mockBuilder = new Mock<Contract.IConfigurationBuilder>(MockBehavior.Strict);
            mockBuilder.Setup(o => o.Build()).Returns(mockRoot.Object);
            // act
            var service = new ConfigurationService(mockBuilder.Object);
            var response = service.Get(key);
            // assert
            Assert.Equal(value, response);
            mockSection.Verify(o => o.Value, Times.Once);
            mockRoot.Verify(o => o.GetSection(key), Times.Once);
            mockBuilder.Verify(o => o.Build(), Times.Once);
        }
    }
}
