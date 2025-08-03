var builder = DistributedApplication.CreateBuilder(args);

// Postgres database with PgAdmin
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

if (builder.ExecutionContext.IsRunMode)
{
    // Data volumes don't work on ACA for Postgres so only add when running
    postgres.WithDataVolume();
}

// Catalog database on postgres
var catalogDb = postgres.AddDatabase("catalogdb");

// Redis cache for basket
var basketCache = builder.AddRedis("basketcache")
    .WithDataVolume()
    .WithRedisCommander();

// Catalog database manager project with health check and reset command
var catalogDbManager = builder.AddProject<Projects.AspireShop_CatalogDbManager>("catalogdbmanager")
    .WithReference(catalogDb)
    .WaitFor(catalogDb)
    .WithHttpHealthCheck("/health")
    .WithHttpCommand("/reset-db", "Reset Database", commandOptions: new() { IconName = "DatabaseLightning" });

// Catalog service project with health check and reference to catalog database
var catalogService = builder.AddProject<Projects.AspireShop_CatalogService>("catalogservice")
    .WithReference(catalogDb)
    .WaitFor(catalogDbManager)
    .WithHttpHealthCheck("/health");

// Basket service project with reference to Redis cache
var basketService = builder.AddProject<Projects.AspireShop_BasketService>("basketservice")
    .WithReference(basketCache)
    .WaitFor(basketCache);

// Frontend project with references to basket and catalog services
builder.AddProject<Projects.AspireShop_Frontend>("frontend")
    .WithExternalHttpEndpoints()
    .WithUrlForEndpoint("https", url => url.DisplayText = "Online Store (HTTPS)")
    .WithUrlForEndpoint("http", url => url.DisplayText = "Online Store (HTTP)")
    .WithHttpHealthCheck("/health")
    .WithReference(basketService)
    .WithReference(catalogService)
    .WaitFor(catalogService);

builder.Build().Run();
