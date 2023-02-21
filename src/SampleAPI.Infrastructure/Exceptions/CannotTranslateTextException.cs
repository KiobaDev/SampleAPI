namespace SampleAPI.Infrastructure.Exceptions;

internal sealed class CannotTranslateTextException : BaseSampleAPIException
{
    public CannotTranslateTextException(string message) : base(message)
    {

    }
}
