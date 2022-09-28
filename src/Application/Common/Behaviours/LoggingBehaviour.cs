using CleanArchitecture.Application.Common.Services.Identity;
using MediatR.Pipeline;

namespace CleanArchitecture.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(
        ILogger<TRequest> logger,
        ICurrentUser currentUser,
        IIdentityService identityService)
    {
        _logger = logger;
        _currentUser = currentUser;
        _identityService = identityService;
    }

    public async Task Process(TRequest request,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUser.UserId ?? string.Empty;
        var userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
