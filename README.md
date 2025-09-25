# Console Task Tracker

A simple **command-line task tracking application**.  
This project is designed for practicing console input/output, file system interactions, and basic application architecture.

All tasks are stored in a `tasks.json` file located in the current working directory. The app is built with .NET and runs from the command line.

---

## Features

- Add new tasks (`add`)
- Update task descriptions (`update`)
- Delete tasks (`delete`)
- Mark tasks as *in-progress* or *done* (`mark-in-progress`, `mark-done`)
- List all tasks (`list`)
- List tasks by status (`list todo`, `list in-progress`, `list done`)

Each task includes the following properties:

- **id** – unique identifier  
- **description** – short description of the task  
- **status** – current status: `todo`, `in-progress`, `done`  
- **createdAt** – date and time when the task was created  
- **updatedAt** – date and time when the task was last updated  

---

## Usage

Run the app from the command line with different commands:
- dotnet run -- add "Buy groceries"
- dotnet run -- update 1 "Buy groceries and cook dinner"
- dotnet run -- mark-in-progress 1
- dotnet run -- mark-done 1
- dotnet run -- delete 1
- dotnet run -- list
- dotnet run -- list in-progress

---

https://roadmap.sh/projects/task-tracker
