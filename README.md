# CSharp Todo API

Todo CRUD API that build with C# ASP.Net core WebAPI

## Tech stack

- C#
- ASP.Net core
- SQL Server

## API documentation

### Base URL

`/api/todo`

### Endpoints

- Create a Todo
  **Endpoint**: `POST /api/todo`
  **Description**: Creates a new todo item.

- Get All Todos
  **Endpoint**: `GET /api/todo`
  **Description**: Retrieves all todo items.

- Get Todo by ID
  **Endpoint**: `GET /api/todo/{id:guid}`
  **Description**: Retrieves a specific todo item by its ID.

- Update Todo
  **Endpoint**: `PUT /api/todo/{id:guid}`
  **Description**: Updates a specific todo item by its ID.

- Delete Todo
  **Endpoint**: `DELETE /api/todo/{id:guid}`
  **Description**: Deletes a specific todo item by its ID.
