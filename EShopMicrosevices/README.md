# EShop Microservices

[![CI Pipeline](https://github.com/gene-fx/EShop/actions/workflows/ci.yml/badge.svg)](https://github.com/gene-fx/EShop/actions/workflows/ci.yml)
[![Deploy Pipeline](https://github.com/gene-fx/EShop/actions/workflows/deploy.yml/badge.svg)](https://github.com/gene-fx/EShop/actions/workflows/deploy.yml)

A production-ready e-commerce platform built with .NET 9, demonstrating microservices architecture, CQRS, Domain-Driven Design, and event-driven communication.

## Services

| Service | Port (HTTP) | Port (HTTPS) | Technology |
|---------|-------------|--------------|------------|
| CatalogAPI | 6000 | 6060 | Carter · Marten · PostgreSQL |
| BasketAPI | 6001 | 6061 | FastEndpoints · Marten · Redis |
| DiscountGrpc | 6002 | 6062 | gRPC · EF Core · SQLite |
| OrderingAPI | 6003 | 6063 | Carter · EF Core · SQL Server |
| YarpApiGateway | 6004 | 6064 | YARP Reverse Proxy |
| ShoppingWeb | — | — | Razor Pages · Refit |

## Architecture

- **CQRS** via MediatR with validation and logging pipeline behaviors
- **Clean Architecture** in Ordering (Domain → Application → Infrastructure → API)
- **DDD** aggregates, value objects, and domain events in the Ordering domain
- **Cache-aside** pattern in Basket (Scrutor decorator wraps Redis over PostgreSQL)
- **Event-driven** checkout flow via RabbitMQ / MassTransit
- **API Gateway** with YARP — routing, rate limiting (5 req/10s on `/ordering-service`)

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Run the full stack

```bash
docker-compose up --build
```

This starts all services plus their backing infrastructure (PostgreSQL ×2, Redis, SQL Server, RabbitMQ).

### Run a single service locally

```bash
# Example: CatalogAPI
cd src/Services/Catalog/CatalogAPI
dotnet run
```

## Testing

### Run all tests

```bash
dotnet test EShopMicrosevices.sln --configuration Release
```

### Run tests for a specific service

```bash
# Catalog
dotnet test src/Services/Catalog/Catalog.Tests/Catalog.Tests.csproj

# Basket
dotnet test src/Services/Basket/Basket.Tests/Basket.Tests.csproj

# Ordering
dotnet test src/Services/Ordering/Ordering.Tests/Ordering.Tests.csproj

# API Gateway
dotnet test src/ApiGateways/YarpApiGateway.Tests/YarpApiGateway.Tests.csproj
```

### Run tests with code coverage

```bash
dotnet test EShopMicrosevices.sln \
  --collect:"XPlat Code Coverage" \
  --results-directory ./TestResults
```

### Test projects

| Test Project | Covers | Framework |
|---|---|---|
| `Catalog.Tests` | CreateProduct, GetById, Update, Delete handlers + validators | xUnit · Moq |
| `Basket.Tests` | GetBasket, StoreBasket, DeleteBasket handlers · CachedBasketRepository | xUnit · Moq |
| `Ordering.Tests` | Order aggregate domain logic · Create/Update/Delete order handlers | xUnit · EF InMemory |
| `YarpApiGateway.Tests` | Rate limiter configuration and permit enforcement | xUnit |

## File Watchers

Each microservice runs a background `FileSystemWatcher` that monitors a directory for incoming data files (`.json`). Paths are configurable via `appsettings.json`:

```json
{
  "FileWatcher": {
    "CatalogPath": "data/imports",
    "BasketPath": "data/exports",
    "OrderingPath": "data/orders"
  }
}
```

When a file is created or modified the service logs the event. Extend `OnFileChanged` in each `*FileWatcher.cs` to trigger domain actions.

## CI/CD

### CI Pipeline (`.github/workflows/ci.yml`)

Triggers on every push and pull request to `main`:

1. Restore dependencies
2. Build (Release)
3. Run all 4 test projects (with XPlat Code Coverage)
4. Upload test results as artifacts
5. Upload coverage to Codecov

### Deploy Pipeline (`.github/workflows/deploy.yml`)

Triggers automatically when the CI pipeline passes on `main`:

1. Builds Docker images for all 5 services
2. Pushes to GitHub Container Registry (`ghcr.io`) with `latest` and `<sha>` tags
3. Uses Docker layer caching for fast rebuilds

### Required Secrets

| Secret | Purpose |
|--------|---------|
| `GITHUB_TOKEN` | Auto-provided — used for GHCR push |
| `CODECOV_TOKEN` | Codecov coverage reporting (optional) |

## Project Structure

```
EShopMicrosevices/
├── src/
│   ├── ApiGateways/
│   │   ├── YarpApiGateway/          # Reverse proxy + rate limiting
│   │   └── YarpApiGateway.Tests/    # Gateway unit tests
│   ├── BuildingBlocks/
│   │   ├── BuildingBlocks/          # CQRS, behaviors, exceptions, pagination
│   │   └── BuildingBlocks.Messaging/ # MassTransit + events
│   ├── Services/
│   │   ├── Catalog/
│   │   │   ├── CatalogAPI/          # Product catalog service
│   │   │   └── Catalog.Tests/       # Catalog unit tests
│   │   ├── Basket/
│   │   │   ├── BasketAPI/           # Shopping cart service
│   │   │   └── Basket.Tests/        # Basket unit tests
│   │   ├── Discount/
│   │   │   └── DiscountGrpc/        # Discount gRPC service
│   │   └── Ordering/
│   │       ├── OrderingAPI/         # Ordering presentation layer
│   │       ├── OrderingApplication/ # Use cases + CQRS handlers
│   │       ├── OrderingDomain/      # DDD aggregates + value objects
│   │       ├── OrderingInfrastructure/ # EF Core + SQL Server
│   │       └── Ordering.Tests/      # Ordering unit tests
│   └── WebApps/
│       └── ShoppingWeb/             # Razor Pages client
├── .github/workflows/
│   ├── ci.yml                       # Build + test pipeline
│   └── deploy.yml                   # Docker build + GHCR push
├── docker-compose.yml
├── docker-compose.override.yml
└── EShopMicrosevices.sln
```
