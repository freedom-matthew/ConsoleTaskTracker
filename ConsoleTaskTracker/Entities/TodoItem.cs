using System;

namespace ConsoleTaskTracker.Entities;

public class TodoItem
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public TodoItemStatus Status { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
}