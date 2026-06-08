# PantryPilot

PantryPilot is a full-stack application built with ASP.NET Core and Angular that helps users maximize the number of people they can feed based on the ingredients they have available.

The application accepts a list of ingredients and quantities, evaluates available recipes, and determines the optimal recipe combinations to maximize the number of people fed while minimizing waste.

## Technology Stack

### Backend

* ASP.NET Core 8
* Entity Framework Core
* SQLite
* xUnit
* Docker

### Frontend

* Angular
* Angular Material
* Tailwind CSS
* NGXS
## Prerequisites

### For Local Development

* .NET 8 SDK
* Node.js (v20 or later recommended)
* Angular CLI

### For Docker

* Docker Desktop
* Docker Compose

Verify installation:

```bash
dotnet --version
node --version
npm --version
ng version
docker --version
docker compose version
```

## Backend Setup (Local Development)

Navigate to the backend solution:

```bash
cd src/backend/PantryPilot
```

Restore dependencies and tools:

```bash
dotnet restore
dotnet tool restore
```

Apply database migrations:

```bash
dotnet ef database update --project PantryPilot.Api
```

Run the API:

```bash
dotnet run --project PantryPilot.Api
```

The API will be available at:

```text
https://localhost:5001
```

Swagger documentation:

```text
https://localhost:5001/swagger
```

## Backend Setup (Docker)

From the repository root:

```bash
docker compose up --build
```

The API will be available at:

```text
https://localhost:32768
```

Swagger documentation:

```text
https://localhost:32768/swagger
```

To stop the containers:

```bash
docker compose down
```

To rebuild after changes:

```bash
docker compose up --build
```

## Frontend Setup

Navigate to the frontend project:

```bash
cd src/frontend
```

Install dependencies:

```bash
npm install
```

Run the application:

```bash
ng serve
```

The application will be available at:

```text
http://localhost:4200
```

The frontend is configured to communicate with the Docker-hosted API running on:

```text
https://localhost:32768/api
```

## Running Tests

### Backend

From the backend solution folder:

```bash
dotnet test
```

### Frontend

```bash
ng test
```

## Database

The application uses SQLite.

Database file:

```text
pantrypilot.db
```

The database is created through Entity Framework Core migrations.

## Configuration

### Backend

* `appsettings.json`
* `appsettings.Development.json`

### Frontend

* `environment.ts`
