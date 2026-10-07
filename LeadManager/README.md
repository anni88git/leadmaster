# 🚀 LeadManager API

![.NET Core](https://img.shields.io/badge/.NET%208.0-Purple?logo=dotnet)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-SQL%20Server-blue?logo=nuget)
![FluentValidation](https://img.shields.io/badge/Validation-FluentValidation-brightgreen)
![Minimal APIs](https://img.shields.io/badge/Architecture-Minimal%20APIs-orange)

**LeadManager** is a high-performance, lightweight RESTful Web API built with **ASP.NET Core 8 Minimal APIs**. Designed with clean architecture principles, it provides a robust backend for managing sales leads.

## ✨ Key Features & Technical Highlights

- **Modern Architecture**: Utilizes .NET 8 Minimal APIs with clean endpoint mapping separated from `Program.cs`.
- **Data Transfer Objects (DTOs)**: Clear separation of concerns between Entity Models and API contracts.
- **Robust Validation**: Integrated **FluentValidation** via custom `IEndpointFilter` for elegant and automatic payload validation.
- **Entity Framework Core**: SQL Server integration with LocalDB for rapid local development.
- **Standardized Error Handling**: Leverages ASP.NET Core `ProblemDetails` (RFC 7807) for consistent error responses.
- **API Documentation**: Fully configured **Swagger UI** for interactive exploration and testing.

---

## 🏗️ Project Structure

The project is structured to scale cleanly while maintaining the simplicity of Minimal APIs:

```text
LeadManager/
├── DTOs/                 # Data Transfer Objects for API contracts
├── Endpoints/            # Endpoint route builders (MapGroup)
├── Filters/              # Custom IEndpointFilters (Validation)
├── Models/               # EF Core domain entities
├── Validation/           # FluentValidation rules
├── Data/                 # Entity Framework DbContext
└── Program.cs            # Application bootstrapping & middleware
```

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE of your choice (Visual Studio, VS Code, Rider)

### Setup & Run

1. **Clone & Navigate**
   ```bash
   cd LeadManager
   ```

2. **Restore & Run**
   The application will automatically create the LocalDB database upon first run.
   ```bash
   dotnet restore
   dotnet run
   ```

3. **Explore the API**
   Navigate to `http://localhost:5000/swagger` in your browser to test the API interactively.

---

## 📡 API Endpoints

| Method | Endpoint | Description | Status Codes |
|---|---|---|---|
| `GET` | `/api/leads` | Retrieve a list of all leads | 200 OK |
| `GET` | `/api/leads/{id}` | Retrieve a specific lead by ID | 200 OK, 404 Not Found |
| `POST` | `/api/leads` | Create a new lead | 201 Created, 400 Bad Request |
| `PUT` | `/api/leads/{id}` | Update an existing lead | 204 No Content, 400, 404 |
| `DELETE` | `/api/leads/{id}` | Delete a lead | 204 No Content, 404 Not Found |

### Sample Request (POST)

```json
POST /api/leads
{
  "name": "Jane Doe",
  "email": "jane.doe@example.com",
  "status": "Contacted"
}
```

---

## 🛠️ Testing via Postman
A Postman collection (`LeadManager.postman_collection.json`) is included in the repository. Import this file into Postman to instantly execute requests against the local environment.
