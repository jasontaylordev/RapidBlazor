using Ardalis.GuardClauses;
using CleanArchitectureBlazor.Application.TodoItems.Commands;
using CleanArchitectureBlazor.Application.TodoLists.Commands;
using CleanArchitectureBlazor.Domain.Entities;
using CleanArchitectureBlazor.WebUI.Shared.TodoItems;
using CleanArchitectureBlazor.WebUI.Shared.TodoLists;

namespace CleanArchitectureBlazor.Application.SubcutaneousTests.TodoItems.Commands;

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
