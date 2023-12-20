using RapidBlazor.Domain.Entities;
using RapidBlazor.WebUi.Shared.TodoLists;
using Riok.Mapperly.Abstractions;

namespace RapidBlazor.Application.TodoLists;

[Mapper]
public static partial class Mapping
{
    public static partial IQueryable<TodoListDto> ProjectToDto(this IQueryable<TodoList> s);
}
