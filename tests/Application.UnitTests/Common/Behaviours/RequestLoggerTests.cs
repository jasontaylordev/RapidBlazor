using CleanArchitectureBlazor.Application.Common.Behaviours;
using CleanArchitectureBlazor.Application.Common.Services.Identity;
using CleanArchitectureBlazor.Application.TodoItems.Commands;
using CleanArchitectureBlazor.WebUI.Shared.TodoItems;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureBlazor.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateTodoItemCommand>> _logger = null!;
    private Mock<ICurrentUser> _currentUser = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateTodoItemCommand>>();
        _currentUser = new Mock<ICurrentUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUser.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUser.Object, _identityService.Object);

        await requestLogger.Process(
            new CreateTodoItemCommand(new CreateTodoItemRequest { ListId = 1, Title = "title" }), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUser.Object, _identityService.Object);

        await requestLogger.Process(
            new CreateTodoItemCommand(new CreateTodoItemRequest { ListId = 1, Title = "title" }), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
