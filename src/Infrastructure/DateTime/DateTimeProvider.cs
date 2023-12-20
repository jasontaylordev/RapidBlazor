using RapidBlazor.Application.Common.Services.DateTime;

namespace RapidBlazor.Infrastructure.DateTime;

public class DateTimeProvider : IDateTimeProvider
{
    public System.DateTime UtcNow => System.DateTime.UtcNow;
}
