using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.TodoItems.Commands;
using CleanArchitecture.Application.TodoLists.Commands;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Shared.TodoItems;
using CleanArchitecture.WebUI.Shared.TodoLists;

namespace CleanArchitecture.Application.SubcutaneousTests.TodoItems.Commands;

using static Testing;

public class CreateTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoItemCommand(
            new CreateTodoItemRequest());

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest
            {
                Title = "New List"
            }));

        var command = new CreateTodoItemCommand(
            new CreateTodoItemRequest
            {
                ListId = listId,
                Title = "Tasks"
            });

        var itemId = await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.Item.ListId);
        item.Title.Should().Be(command.Item.Title);
        item.CreatedBy.Should().Be(userId);
        item.CreatedUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModifiedUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}
