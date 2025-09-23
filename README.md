# 🛍️ AspireShop - Microservices E-Commerce Platform (.NET 8 & .NET Aspire)

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Aspire](https://img.shields.io/badge/.NET%20Aspire-9.3.0-purple.svg)](https://learn.microsoft.com/en-us/dotnet/aspire/)
[![Docker](https://img.shields.io/badge/Docker-Required-blue.svg)](https://www.docker.com/products/docker-desktop/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-blueviolet.svg)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)

This repository contains a comprehensive Proof-of-Concept (POC) demonstrating a modern microservices-based e-commerce platform built with **.NET 8** and **.NET Aspire**. It showcases cloud-native application development patterns, distributed service orchestration, observability, and resilience features in a real-world shopping scenario.

<img width="1711" height="986" alt="Aspire Shop" src="https://github.com/user-attachments/assets/ea1144e0-f347-4f3f-bb14-75d9a49a769d" />

## 🎯 What is this POC about?

AspireShop is a fully functional e-commerce application that demonstrates:

🏗️ **Microservices Architecture**: Service-oriented design with clear separation of concerns  
🔄 **Service Orchestration**: Using .NET Aspire for service discovery, configuration, and lifecycle management  
📊 **Observability**: Built-in telemetry, logging, and health monitoring  
🔌 **Inter-Service Communication**: HTTP APIs and gRPC for efficient service-to-service communication  
💾 **Data Persistence**: PostgreSQL for relational data and Redis for caching  
🎨 **Modern UI**: Blazor Server for interactive web interface  
🐳 **Containerization**: Docker-based deployment with development containers  
🔧 **Developer Experience**: Hot reload, debugging, and local development optimizations  

The application simulates a complete online shopping experience:
1. 🏪 Browse product catalog with pagination
2. 🛒 Add/remove items from shopping cart
3. 💾 Persistent cart storage across sessions
4. 📱 Responsive web interface
5. 🔍 Real-time health monitoring and observability

## ✨ When and Why Use .NET Aspire?

**.NET Aspire** is a cloud-native application framework designed for building distributed applications. Use it when:

### 🎯 **Microservices Orchestration**
- **Service Discovery**: Automatic service registration and discovery without hardcoded endpoints
- **Configuration Management**: Centralized configuration with environment-specific overrides
- **Dependency Management**: Explicit service dependencies and startup ordering

### 📊 **Built-in Observability**
- **Distributed Tracing**: End-to-end request tracking across all services
- **Structured Logging**: Consistent logging patterns with correlation IDs
- **Metrics Collection**: Performance metrics, health checks, and custom telemetry
- **Real-time Monitoring**: Live dashboard for application health and performance

### 🛠️ **Developer Productivity**
- **Local Development**: Simplified setup with automatic container orchestration
- **Hot Reload**: Fast development cycles with live updates
- **Debugging**: Comprehensive debugging experience across distributed services
- **Testing**: Built-in support for integration testing scenarios

### 🔒 **Production-Ready Features**
- **Resilience Patterns**: Automatic retry policies, circuit breakers, and timeouts
- **Security**: Built-in authentication, authorization, and secure communication patterns
- **Scalability**: Container-ready with cloud deployment support
- **Health Checks**: Comprehensive health monitoring with automated recovery

## 🏗️ Architecture & Design Patterns

AspireShop follows modern architectural principles combining **Clean Architecture**, **Domain-Driven Design**, and **Microservices patterns**:

### 🎯 **Microservices Architecture**

```
┌─────────────────────────────────────────────────────────────┐
│                    Frontend (Blazor)                        │
│               User Interface & Experience                   │
└─────────────────┬─────────────────┬─────────────────────────┘
                  │                 │
                  ▼                 ▼
┌─────────────────────────────────┐ ┌─────────────────────────┐
│      Catalog Service            │ │     Basket Service      │
│      (HTTP REST API)            │ │     (gRPC Service)      │
│                                 │ │                         │
│ • Product Management            │ │ • Shopping Cart         │
│ • Image Serving                 │ │ • Session Management    │
│ • Search & Filtering            │ │ • Real-time Updates     │
└─────────────┬───────────────────┘ └──────────┬──────────────┘
              │                                │
              ▼                                ▼
┌─────────────────────────────────┐ ┌─────────────────────────┐
│      PostgreSQL                 │ │         Redis           │
│   Catalog Database              │ │      Basket Cache       │
│                                 │ │                         │
│ • Product Information           │ │ • Session Storage       │
│ • Categories                    │ │ • Fast Access           │
│ • Pricing & Inventory           │ │ • Distributed Cache     │
└─────────────────────────────────┘ └─────────────────────────┘
```

### 🧩 **Service Breakdown**

| Service | Technology | Purpose | Database |
|---------|------------|---------|----------|
| **🌐 Frontend** | Blazor Server | User interface, shopping experience | - |
| **📦 Catalog Service** | ASP.NET Core Web API | Product catalog management | PostgreSQL |
| **🛒 Basket Service** | gRPC Service | Shopping cart operations | Redis |
| **🗄️ CatalogDb Manager** | ASP.NET Core Web API | Database initialization & management | PostgreSQL |
| **⚙️ Service Defaults** | Class Library | Shared configurations & extensions | - |
| **🎛️ App Host** | .NET Aspire Host | Orchestration & service discovery | - |

### 🔧 **Communication Patterns**

- **🌐 Frontend ↔ Catalog Service**: HTTP REST API with service discovery
- **🌐 Frontend ↔ Basket Service**: gRPC with strongly-typed contracts
- **📦 Catalog Service ↔ PostgreSQL**: Entity Framework Core with health checks
- **🛒 Basket Service ↔ Redis**: StackExchange.Redis with connection pooling

## 📋 Prerequisites

### 🛠️ **Required Tools & Software**

- **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** (Latest version)
- **[Docker Desktop](https://www.docker.com/products/docker-desktop/)** (For containerized services)
- **[Visual Studio 2022 17.10+](https://visualstudio.microsoft.com/vs/)** with .NET Aspire workload (Recommended)
  - Or **[Visual Studio Code](https://code.visualstudio.com/)** with C# extension
- **[Git](https://git-scm.com/)** for version control

### 🔧 **Visual Studio Extensions (Recommended)**

- **.NET Aspire Workload**: Essential for Aspire project support
- **Docker for Visual Studio**: Container debugging and management
- **Entity Framework Core Tools**: Database migrations and tooling

### ☁️ **Optional Cloud Tools**

- **[Azure CLI](https://docs.microsoft.com/en-us/cli/azure/)**: For Azure deployment
- **[kubectl](https://kubernetes.io/docs/tasks/tools/)**: For Kubernetes deployment

## 🚀 Implementation Guide

### Step 1: Environment Setup

1. **Install Prerequisites**
   ```bash
   # Verify .NET 8 installation
   dotnet --version  # Should show 8.0.x
   
   # Verify Docker is running
   docker --version
   docker ps
   ```

2. **Clone the Repository**
   ```bash
   git clone https://github.com/karansinh-raj/AspireShop.git
   cd AspireShop
   ```

3. **Install .NET Aspire Workload** (if not already installed)
   ```bash
   dotnet workload install aspire
   ```

### Step 2: Understanding the Project Structure

```
AspireShop/
├── 🎛️ AspireShop.AppHost/              # Aspire orchestration host
│   ├── AppHost.cs                      # Service configuration & dependencies
│   └── Program.cs                      # Application entry point
├── 🌐 AspireShop.Frontend/             # Blazor Server application
│   ├── Components/                     # Blazor components
│   ├── Services/                       # HTTP/gRPC clients
│   └── Program.cs                      # Frontend configuration
├── 📦 AspireShop.CatalogService/       # Product catalog API
│   ├── Controllers/                    # REST API endpoints
│   ├── Models/                         # Data models
│   └── Program.cs                      # API configuration
├── 🛒 AspireShop.BasketService/        # Shopping cart gRPC service
│   ├── Services/                       # gRPC implementations
│   ├── Protos/                         # Protocol buffer definitions
│   └── Program.cs                      # gRPC configuration
├── 🗄️ AspireShop.CatalogDb/           # Database context & models
│   ├── Models/                         # Entity models
│   └── CatalogDbContext.cs            # EF Core context
├── 🔧 AspireShop.CatalogDbManager/     # Database management API
└── ⚙️ AspireShop.ServiceDefaults/      # Shared configurations
    └── Extensions.cs                   # Aspire service extensions
```

### Step 3: Running the Application

#### 🎯 **Option A: Visual Studio (Recommended)**

1. **Open Solution**
   ```
   Open AspireShop.sln in Visual Studio 2022
   ```

2. **Set Startup Project**
   - Right-click on `AspireShop.AppHost`
   - Select "Set as Startup Project"

3. **Run Application**
   - Press `F5` or click "Start Debugging"
   - Aspire Dashboard will automatically open in your browser

#### 🎯 **Option B: .NET CLI**

1. **Navigate to AppHost Directory**
   ```bash
   cd AspireShop.AppHost
   ```

2. **Run the Application**
   ```bash
   dotnet run
   ```

3. **Access Services**
   - Aspire Dashboard: `https://localhost:15888` (or as shown in console)
   - Frontend Application: `https://localhost:7042` (or as configured)

### Step 4: Exploring the Aspire Dashboard

1. Aspire Resources - Table view
   
   <img width="1710" height="985" alt="Image" src="https://github.com/user-attachments/assets/31b927e3-6721-4a4b-aed7-f1ce44764458" />
   
2. Aspire Resources - Graph view
   
   <img width="1710" height="985" alt="Image" src="https://github.com/user-attachments/assets/9c30010d-c69e-48fe-9178-2a4ce2313593" />
   
3. Aspire Console Logs
   
   <img width="1710" height="985" alt="Image" src="https://github.com/user-attachments/assets/d786ad76-7230-490e-85ff-12fe29cc7980" />

4. Aspire Structured Logs
   
   <img width="1710" height="985" alt="Image" src="https://github.com/user-attachments/assets/9576801a-ed08-4d6c-84d4-697dbca1bb53" />
   
5. Aspire Traces
    
   <img width="1710" height="985" alt="Image" src="https://github.com/user-attachments/assets/5aefd81c-f352-43f2-a3ec-f4c6898a6b49" />

6. Aspire Trace Overview - Add Item to cart, and update cart page
    
   <img width="1710" height="985" alt="Image" src="https://github.com/user-attachments/assets/4a6fe54f-05da-4c08-b006-a7c5ef0accc4" />
   
7. Aspire Metrics
    
   <img width="1710" height="985" alt="Image" src="https://github.com/user-attachments/assets/d2835a44-83ac-4b38-baa9-f7ecce48dd85" />

The Aspire Dashboard provides comprehensive monitoring:

- **🏥 Resources**: View all services, containers, and their health status
- **📊 Telemetry**: Distributed tracing, metrics, and performance data
- **📝 Logs**: Structured logging with correlation across services
- **🔍 Traces**: End-to-end request tracking through the system

### Step 5: Understanding Service Configuration

The `AppHost.cs` file demonstrates Aspire's power:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// 🗄️ Infrastructure Services
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()                          // Add PgAdmin for database management
    .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");

var basketCache = builder.AddRedis("basketcache")
    .WithDataVolume()                       // Persistent data
    .WithRedisCommander();                  // Redis management UI

// 🛠️ Application Services with Dependencies
var catalogDbManager = builder.AddProject<Projects.AspireShop_CatalogDbManager>("catalogdbmanager")
    .WithReference(catalogDb)               // Database dependency
    .WaitFor(catalogDb)                     // Startup ordering
    .WithHttpHealthCheck("/health");        // Health monitoring

var catalogService = builder.AddProject<Projects.AspireShop_CatalogService>("catalogservice")
    .WithReference(catalogDb)
    .WaitFor(catalogDbManager)              // Wait for DB initialization
    .WithHttpHealthCheck("/health");

var basketService = builder.AddProject<Projects.AspireShop_BasketService>("basketservice")
    .WithReference(basketCache)
    .WaitFor(basketCache);

// 🌐 Frontend with Service References
builder.AddProject<Projects.AspireShop_Frontend>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(basketService)           // gRPC service reference
    .WithReference(catalogService)          // HTTP API reference
    .WaitFor(catalogService);
```

## ⚙️ Configuration Details

### 🔧 **Service Defaults Configuration**

The `AspireShop.ServiceDefaults` project provides shared functionality:

```csharp
public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
{
    // 📊 OpenTelemetry for observability
    builder.ConfigureOpenTelemetry();
    
    // 🏥 Health checks
    builder.AddDefaultHealthChecks();
    
    // 🔍 Service discovery
    builder.Services.AddServiceDiscovery();
    
    // 🔄 HTTP resilience (retry, circuit breaker, timeout)
    builder.Services.ConfigureHttpClientDefaults(http =>
    {
        http.AddStandardResilienceHandler();
        http.AddServiceDiscovery();
    });
    
    return builder;
}
```

### 🌐 **Frontend Service Configuration**

```csharp
// Service registration with automatic discovery
builder.Services.AddHttpServiceReference<CatalogServiceClient>(
    "https+http://catalogservice", 
    healthRelativePath: "health");

// gRPC client with health checks
builder.Services.AddGrpcServiceReference<Basket.BasketClient>(
    "https://basketservice", 
    failureStatus: HealthStatus.Degraded);
```

### 🗄️ **Database Configuration**

```csharp
// PostgreSQL with Entity Framework
builder.AddNpgsqlDbContext<CatalogDbContext>("catalogdb");

// Redis for caching
builder.AddRedis("basketcache");
```

## 🌍 Real-World Use Cases

### 1. 🏪 **E-Commerce Platforms**
- **Product Catalogs**: Scalable product management with search and filtering
- **Shopping Carts**: Session-based cart management with Redis persistence
- **User Experience**: Responsive Blazor interface with real-time updates

### 2. 🏢 **Enterprise Applications**
- **Microservices Migration**: Gradual transition from monolithic to microservices
- **Service Mesh**: Advanced service communication patterns
- **Observability**: Comprehensive monitoring and debugging capabilities

### 3. ☁️ **Cloud-Native Development**
- **Container Orchestration**: Docker-based development and deployment
- **Kubernetes Ready**: Easy migration to Kubernetes environments
- **Azure Integration**: Native support for Azure services and deployment

### 4. 🚀 **Startup MVPs**
- **Rapid Prototyping**: Quick setup with built-in infrastructure
- **Scalable Foundation**: Architecture that grows with your business
- **Developer Productivity**: Focus on business logic, not infrastructure

## 📊 Application Flow Diagrams

### 🛒 **Shopping Cart Flow**

```
User browses products → Adds to cart → gRPC call to BasketService → Redis storage → Real-time UI update
```

### 📦 **Catalog Service Flow**

```
Frontend requests → HTTP API → CatalogService → PostgreSQL query → JSON response → UI rendering
```

### 🔄 **Service Discovery Flow**

```
Service startup → Aspire registration → Service discovery → Automatic endpoint resolution → HTTP/gRPC communication
```

### 📊 **Performance Monitoring**

#### **Key Metrics to Monitor**
- **Response Times**: HTTP API and gRPC call durations
- **Memory Usage**: Container memory consumption
- **Database Connections**: PostgreSQL connection pool usage
- **Cache Hit Ratio**: Redis cache effectiveness

#### **Using Application Insights** (Optional)
```csharp
// Add to any service's Program.cs
builder.Services.AddApplicationInsightsTelemetry();
```

## 🔮 Future Enhancements & Roadmap

### 🚀 **Phase 1: Core Features Enhancement**
- [ ] **User Authentication**: JWT-based authentication with Identity Server
- [ ] **Order Management**: Complete order processing workflow
- [ ] **Payment Integration**: Stripe/PayPal payment processing
- [ ] **Inventory Management**: Real-time stock tracking
- [ ] **Email Notifications**: Order confirmations and updates

### 🛡️ **Phase 2: Production Readiness**
- [ ] **API Rate Limiting**: Prevent abuse and ensure fair usage
- [ ] **Data Protection**: GDPR compliance and data encryption
- [ ] **Advanced Caching**: Multi-level caching strategies
- [ ] **Database Migrations**: Automated schema management
- [ ] **Security Hardening**: HTTPS enforcement, CORS policies

### ☁️ **Phase 3: Cloud & DevOps**
- [ ] **Azure Deployment**: Container Apps and AKS deployment
- [ ] **CI/CD Pipeline**: GitHub Actions for automated deployment
- [ ] **Infrastructure as Code**: Bicep/Terraform templates
- [ ] **Monitoring & Alerting**: Application Insights integration
- [ ] **Load Testing**: Performance benchmarking

## 📚 Learning Resources

### 📖 **Official Documentation**
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [gRPC in .NET](https://learn.microsoft.com/en-us/aspnet/core/grpc/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
### 🔧 **Tools & Extensions**
- [Aspire Dashboard](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/dashboard)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [pgAdmin](https://www.pgadmin.org/) - PostgreSQL administration
- [Redis Commander](https://github.com/joeferner/redis-commander) - Redis management

## 🎉 Getting Started Checklist

- [ ] ✅ Install .NET 8 SDK
- [ ] 🐳 Install Docker Desktop
- [ ] 🔧 Install Visual Studio 2022 with Aspire workload
- [ ] 📥 Clone this repository
- [ ] 🚀 Run the application using `AspireShop.AppHost`
- [ ] 🌐 Access the Aspire Dashboard
- [ ] 🛍️ Test the shopping experience
- [ ] 📊 Explore observability features
- [ ] 🔍 Review the code structure
- [ ] 🚀 Deploy to your preferred environment

**Happy Coding!** 🎊
