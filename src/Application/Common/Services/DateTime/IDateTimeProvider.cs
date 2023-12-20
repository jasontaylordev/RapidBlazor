namespace RapidBlazor.Application.Common.Services.DateTime;

public interface IDateTimeProvider
{
    System.DateTime UtcNow { get; }
}
