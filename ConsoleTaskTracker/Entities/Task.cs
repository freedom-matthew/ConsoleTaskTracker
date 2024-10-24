﻿namespace ConsoleTaskTracker.Entities;

internal class Task
{
    public int TaskId { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
