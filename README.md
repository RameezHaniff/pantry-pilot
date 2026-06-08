# PantryPilot

PantryPilot is a full-stack application built with ASP.NET Core and Angular that helps users maximize the number of people they can feed based on the ingredients they have available.

The application accepts a list of ingredients and quantities, evaluates available recipes, and determines the optimal recipe combinations to maximize the number of people fed while minimizing waste.

## Technology Stack

### Backend

* ASP.NET Core 8
* Entity Framework Core
* SQLite
* xUnit

### Frontend

* Angular
* Angular Material
* Tailwind CSS
* NGXS

## Features

* Ingredient selection and quantity management
* Recipe optimization based on available ingredients
* Calculation of maximum people fed
* Display of unused ingredients
* Global API exception handling
* Loading states and user feedback
* Unit tests for services

## Prerequisites

* .NET 8 SDK
* Node.js (v20 or later recommended)
* Angular CLI

Verify installation:

```bash
dotnet --version
node --version
npm --version
ng version
```

## Backend Setup

Navigate to the API project:

```bash
cd src/backend/PantryPilot.Api
```

Restore dependencies:

```bash
dotnet restore
dotnet tool restore
```

Apply database migrations:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

## Frontend Setup

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

## Running Tests

### Backend

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

The database is automatically created through Entity Framework migrations.

