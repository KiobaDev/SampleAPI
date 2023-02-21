namespace SampleAPI.Infrastructure.Exceptions;

internal sealed class NoConfigurationException : BaseSampleAPIException
{
    public NoConfigurationException(string message) : base(message)
    {

    }
}

