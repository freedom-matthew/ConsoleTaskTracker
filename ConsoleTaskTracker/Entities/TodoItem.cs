using System;
using System.Text.Json.Serialization;

namespace ConsoleTaskTracker.Entities;

public class TodoItem
{
    public int Id { get; init; }
    public required string Description { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TodoItemStatus Status { get; set; } = TodoItemStatus.Todo;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }
}