using Ardalis.GuardClauses;
using CleanArchitecture.Application.TodoLists.Commands;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Shared.TodoLists;

namespace CleanArchitecture.Application.SubcutaneousTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);

        await FluentActions.Invoking(() => 
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest
            {
                Title = "New List"
            }));

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
