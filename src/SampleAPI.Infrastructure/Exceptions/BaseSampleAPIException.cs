namespace SampleAPI.Infrastructure.Exceptions;

internal abstract class BaseSampleAPIException : Exception
{
    protected BaseSampleAPIException(string message) : base(message)
    {

    }
}

