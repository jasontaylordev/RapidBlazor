using Ardalis.GuardClauses;
using CleanArchitecture.Application.TodoItems.Commands;
using CleanArchitecture.Application.TodoLists.Commands;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Shared.TodoItems;
using CleanArchitecture.WebUI.Shared.TodoLists;

namespace CleanArchitecture.Application.SubcutaneousTests.TodoItems.Commands;

using static Testing;

public class DeleteTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteTodoItemCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var listId = await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest
            {
                Title = "New List"
            }));

        var itemId = await SendAsync(new CreateTodoItemCommand(
            new CreateTodoItemRequest
            {
                ListId = listId,
                Title = "Tasks"
            }));

        await SendAsync(new DeleteTodoItemCommand(itemId));

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().BeNull();
    }
}
