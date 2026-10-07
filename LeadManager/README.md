# LeadManager API

A minimal ASP.NET Core Web API (.NET 8) for managing sales leads.

## Features
- Single entity: `Lead` (Id, Name, Email, Status)
- Entity Framework Core with SQL Server (LocalDB)
- Minimal APIs with 5 CRUD endpoints
- Basic validation included (Name and valid Email required)
- Swagger integrated for easy testing
- Automatic database creation on startup

## How to Run

1. Open a terminal in the project directory (`LeadManager`).
2. Restore dependencies and run the project:
   ```bash
   dotnet restore
   dotnet run
   ```
3. Once running, open your browser and navigate to `http://localhost:5000/swagger` to interact with the API via the Swagger UI.

## Endpoints

| Method | Endpoint | Description | Sample JSON Request Body |
|---|---|---|---|
| GET | `/leads` | Get all leads | (None) |
| GET | `/leads/{id}` | Get a specific lead | (None) |
| POST | `/leads` | Create a new lead | `{"name":"John Doe","email":"john@example.com","status":"New"}` |
| PUT | `/leads/{id}` | Update an existing lead | `{"id":1,"name":"Jane Doe","email":"jane@example.com","status":"Contacted"}` |
| DELETE | `/leads/{id}` | Delete a lead by ID | (None) |

## API Testing

A Postman collection (`LeadManager.postman_collection.json`) is included in the repository. You can import this file into Postman to test all endpoints quickly.
