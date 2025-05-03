# FinanceNewsService

The **FinanceNewsService** is a C# ASP.NET Core Web API designed to manage and serve financial news articles. It provides full CRUD (Create, Read, Update, Delete) functionality, along with search capabilities. The application follows clean architecture principles, using a layered structure that separates concerns across controllers, services, and data access layers.

---

## Table of Contents

* [Overview](#overview)
* [Architecture](#architecture)
* [Technologies Used](#technologies-used)
* [Project Structure](#project-structure)
* [API Endpoints](#api-endpoints)
* [Getting Started](#getting-started)
* [Database Setup](#database-setup)
* [Sample HTTP Requests](#sample-http-requests)
* [Logging](#logging)
* [Future Enhancements](#future-enhancements)
* [License](#license)

---

## Overview

The purpose of this service is to act as a centralized microservice for storing and querying financial news. It is part of a broader microservices-based architecture that could include stock management, budget calculation, and opinion analytics modules.

Key features:

* News data storage and retrieval via RESTful API
* Search functionality based on content keywords
* Entity Framework Core integration for persistence
* Modular, scalable, and testable structure

---

## Architecture

The architecture follows a layered pattern:

* **Controller Layer**: Manages HTTP requests and responses.
* **Service Layer**: Contains business logic and validation.
* **Data Layer**: Handles database interactions via Entity Framework Core.
* **Model Layer**: Defines the data structure used across the application.

Dependency Injection (DI) is used to manage service lifetimes and decouple components.

---

## Technologies Used

* **ASP.NET Core 6.0+**
* **C#**
* **Entity Framework Core**
* **SQL Server**
* **LINQ**
* **ILogger (Microsoft.Extensions.Logging)**
* **Swashbuckle/Swagger (optional)**

---

## Project Structure

```
FinanceNewsService/
├── Controllers/
│   └── NewsController.cs             // API endpoints
├── Data/
│   └── AppDbContext.cs               // DbContext setup
├── Models/
│   └── NewsData.cs                   // News data model
├── Services/
│   ├── INewsService.cs              // Interface for service
│   └── NewsService.cs               // Service implementation
├── Properties/
│   └── launchSettings.json           // Debugging configuration
├── FinanceNewsService.csproj         // Project configuration file
├── Program.cs                        // Application startup
├── appsettings.json                  // App configuration
├── FinanceNewsService.http           // Sample HTTP requests
└── .gitignore
```

---

## API Endpoints

### GET /api/news

Returns all news articles.

### GET /api/news/{id}

Retrieves a single news article by its unique ID.

### POST /api/news

Creates a new news article.
**Body**:

```json
{
  "title": "Market Update",
  "description": "The stock market saw a significant rise today...",
  "timestamp": "2025-05-02T14:30:00Z"
}
```

### PUT /api/news/{id}

Updates an existing news article.
**Body**: Same structure as POST.

### DELETE /api/news/{id}

Deletes a news article by ID.

### GET /api/news/search/{term}

Searches articles containing the specified keyword in the title or description.

---

## Getting Started

### Prerequisites

* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Setup Instructions

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/FinanceNewsService.git
   cd FinanceNewsService
   ```

2. Set the SQL Server connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=FinanceNewsDB;Trusted_Connection=True;"
   }
   ```

3. Run database migrations (if applicable):

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. Build and run the application:

   ```bash
   dotnet build
   dotnet run
   ```

---

## Database Setup

The application uses Entity Framework Core. The `AppDbContext` defines a single `DbSet<NewsData>` representing the news articles.

Schema for `NewsData`:

| Field       | Type     | Description                  |
| ----------- | -------- | ---------------------------- |
| Id          | int (PK) | Unique identifier            |
| Title       | string   | Title of the news article    |
| Description | string   | Content of the article       |
| Timestamp   | DateTime | Date and time of publication |

---

## Sample HTTP Requests

Stored in `FinanceNewsService.http`, the file includes:

* GET all news
* GET by ID
* POST a new article
* PUT update article
* DELETE by ID
* SEARCH by keyword

Use tools like **VS Code REST Client**, **Postman**, or **cURL** to test.

---

## Logging

The application uses the built-in `ILogger` interface for structured logging. Logs are printed to the console and can be redirected to external logging systems such as Serilog or Application Insights.

---

## Future Enhancements

* Authentication and authorization (e.g., JWT-based security)
* Tag-based or category-based news filtering
* Pagination and sorting of news results
* Caching for high-read endpoints
* Integration with external news APIs
* Docker support and CI/CD pipeline

---

