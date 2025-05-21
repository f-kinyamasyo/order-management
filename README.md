# Order Management System – API Feature Implementation (.NET 8)

## Overview

This is a modular and extensible **.NET 8 Web API** that manages customer orders. It features:

- A rule-based discounting system
- Order status tracking with valid transitions
- Analytics endpoint for insights like average order value and fulfillment time
- Unit and integration tests in a **dedicated test project**
- Swagger/OpenAPI documentation
- Performance optimizations


## Project Structure

OrderManagement.API            # Main Web API project
  - Controllers                # API endpoints
  - Services                   # Core business logic (Discount, Status, Analytics)
  - Interfaces                 # Abstractions for DI
  - Models                     # Domain entities
  - DTOs                       # Data transfer objects
  - Program.cs                  # Startup configuration
  - OrderManagement.API.csproj  # API project file

OrderManagement.Tests          # Test project
  - OrdersApiTests.cs           # Integration tests using WebApplicationFactory
  - DiscountServiceTests.cs     # Unit tests for discount logic
  - OrderManagement.Tests.csproj

## Features

### 1. Discount System

- Based on **customer segment** and **order history**
- Built using **Strategy Pattern**
- Easily extendable for future promotions
- Unit tested with multiple scenarios

### 2. Order Status Tracking

- Supports transitions: `Pending → Processing → Fulfilled / Cancelled`
- Implemented via a service using a **State Pattern-inspired** structure
- Prevents invalid transitions through service enforcement

### 3. Order Analytics Endpoint

- `GET /api/orders/analytics` returns:
  - Average order value
  - Average fulfillment time (in hours)
- Built for extensibility and performance (see below)

## Testing Approach & Coverage

### Unit Tests

- Located in `OrderManagement.Tests/DiscountServiceTests.cs`
- Covers multiple customer segments and order conditions
- Isolated using mocked repository interfaces

### Integration Tests

- Located in `OrderManagement.Tests/OrdersApiTests.cs`
- Uses `WebApplicationFactory<Program>` for real HTTP simulation
- Verifies `/api/orders/analytics` response format and values

###  Running Tests

Ensure the API project builds first:

dotnet build OrderManagement.API
dotnet test OrderManagement.Tests


> If you face `testhost.deps.json` issues, ensure `PreserveCompilationContext` is set to `true` in `OrderManagement.API.csproj`.

## API Design & Documentation

- Follows RESTful standards
- Swagger UI auto-generated via Swashbuckle
- DTOs and models use `[JsonPropertyName]` and XML comments

###  Try it out:

dotnet run --project OrderManagement.API
# Then open https://localhost:{port}/swagger in your browser

##  Performance Considerations

### Implemented:

- **Memory Caching**:
  - Order analytics results are cached for 5 minutes using `IMemoryCache`.
  - Reduces repeated computation cost.

- **Optimized DI Lifetimes**:
  - Stateless services (e.g., status tracker) use `Singleton`
  - Data services (e.g., discount calculator) use `Scoped`

- **Serialization Optimization**:
  - Uses `System.Text.Json` with camelCase naming
  - Lightweight response payloads


## Build & Run
git clone https://github.com/your-username/order-management-api.git
cd order-management-api

# Run API
dotnet run --project OrderManagement.API

# Run Tests
dotnet test OrderManagement.Tests


## Assumptions Made

- Order data is stored in-memory for demo/testing
- Customer segmentation is hardcoded or simulated
- Fulfillment timestamps are assumed available


##  Author Notes

This implementation focuses on **clean architecture**, **testability**, and **performance** using idiomatic .NET 8 practices. Designed for extensibility with minimal external dependencies.
