﻿using AspireShop.CatalogDb;

namespace AspireShop.CatalogService;

public static class CatalogApi
{
    public static RouteGroupBuilder MapCatalogApi(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/v1/catalog");

        group.WithTags("Catalog");

        group.MapGet("items/type/all", (CatalogDbContext catalogContext, int? before, int? after, int pageSize = 8)
            => GetCatalogItems(null, catalogContext, before, after, pageSize))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<CatalogItemsPage>();

        group.MapGet("items/type/all/brand/{catalogBrandId:int}", (int catalogBrandId, CatalogDbContext catalogContext, int? before, int? after, int pageSize = 8)
            => GetCatalogItems(catalogBrandId, catalogContext, before, after, pageSize))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<CatalogItemsPage>();

        group.MapGet("items/{catalogItemId:int}/image", async (int catalogItemId, CatalogDbContext catalogDbContext, IHostEnvironment environment) =>
        {
            var item = await catalogDbContext.CatalogItems.FindAsync(catalogItemId);

            if (item is null)
            {
                return Results.NotFound();
            }

            var path = Path.Combine(environment.ContentRootPath, "Images", item.PictureFileName);

            if (!File.Exists(path))
            {
                return Results.NotFound();
            }

            return Results.File(path, "image/jpeg");
        })
        .Produces(404)
        .Produces(200, contentType: "image/jpeg");

        return group;
    }

    private static async Task<IResult> GetCatalogItems(int? catalogBrandId, CatalogDbContext catalogContext, int? before, int? after, int pageSize)
    {
        if (before is > 0 && after is > 0)
        {
            return TypedResults.BadRequest($"Invalid paging parameters. Only one of {nameof(before)} or {nameof(after)} can be specified, not both.");
        }

        var itemsOnPage = await catalogContext.GetCatalogItemsCompiledAsync(catalogBrandId, before, after, pageSize);

        var (firstId, nextId) = itemsOnPage switch
        {
            [] => (0, 0),
            [var only] => (only.Id, only.Id),
            [var first, .., var last] => (first.Id, last.Id)
        };

        return Results.Ok(new CatalogItemsPage(
            firstId,
            nextId,
            itemsOnPage.Count < pageSize,
            itemsOnPage.Take(pageSize)));
    }
}

public record CatalogItemsPage(int FirstId, int NextId, bool IsLastPage, IEnumerable<CatalogItem> Data);
