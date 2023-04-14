using CPX.Configuration.Mocks;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;

namespace CPX.Configuration.Test;

public class ConfigurationServiceTest
{
    [Fact]
    public void Should_be_able_to_return_string()
    {
        // Arrange
        var key = "string";
        var value = "value";
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        var result = service.GetString(key);
        // Assert
        Assert.Equal(value, result);
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_not_be_able_to_return_string_when_return_is_null()
    {
        // Arrange
        var key = "string";
        string? value = null;
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            service.GetString(key);
        });
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_be_able_to_return_boolean()
    {
        // Arrange
        var key = "boolean";
        var value = "true";
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        var result = service.GetBoolean(key);
        // Assert
        Assert.True(result);
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_not_be_able_to_return_boolean_when_the_value_is_invalid()
    {
        // Arrange
        var key = "string";
        var value = "value";
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            service.GetBoolean(key);
        });
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_be_able_to_return_datetime()
    {
        // Arrange
        var key = "datetime";
        var date = new DateTime();
        var value = date.ToString();
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        var result = service.GetDateTime(key);
        // Assert
        Assert.Equal(date, result);
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_not_be_able_to_return_datetime_when_the_value_is_invalid()
    {
        // Arrange
        var key = "string";
        var value = "value";
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            service.GetDateTime(key);
        });
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_be_able_to_return_decimal()
    {
        // Arrange
        var key = "decimal";
        var value = "0.5";
        var parsedValue = decimal.Parse(value);
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        var result = service.GetDecimal(key);
        // Assert
        Assert.Equal(parsedValue, result);
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_not_be_able_to_return_decimal_when_the_value_is_invalid()
    {
        // Arrange
        var key = "decimal";
        var value = "value";
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            service.GetDecimal(key);
        });
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_be_able_to_return_integer()
    {
        // Arrange
        var key = "integer";
        var value = "1";
        var parsedValue = int.Parse(value);
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        var result = service.GetInteger(key);
        // Assert
        Assert.Equal(parsedValue, result);
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_not_be_able_to_return_integer_when_the_value_is_invalid()
    {
        // Arrange
        var key = "decimal";
        var value = "value";
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            service.GetDecimal(key);
        });
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_be_able_to_return_object()
    {
        // Arrange
        var key = "object";
        var value = "{ name: \"foo\" }";
        var parsedValue = JsonConvert.DeserializeObject<Foo>(value);
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        var result = service.GetObject<Foo>(key);
        // Assert
        Assert.NotNull(parsedValue);
        if (parsedValue != null)
            Assert.Equal(parsedValue.Name, result.Name);
        mockConfiguration.VerifyAll();
    }

    [Fact]
    public void Should_not_be_able_to_return_object_when_the_value_is_invalid()
    {
        // Arrange
        var key = "object";
        var value = "value";
        var mockConfiguration = GetMockConfiguration(key, value);
        // Act
        var service = new ConfigurationService(mockConfiguration.Object);
        Assert.Throws<ArgumentException>(() =>
        {
            service.GetObject<Foo>(key);
        });
        // Assert
        mockConfiguration.VerifyAll();
    }

    private Mock<IConfiguration> GetMockConfiguration(string key, string? value)
    {
        var mockConfigurationSection = new Mock<IConfigurationSection>();
        mockConfigurationSection.SetupProperty(o => o.Value, value);
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(o => o.GetSection(key)).Returns(mockConfigurationSection.Object).Verifiable();
        return mockConfiguration;
    }
}