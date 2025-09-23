using AspireShop.CatalogDb;

namespace AspireShop.CatalogService.RequestResponseModels;

public record CatalogItemsPage(int FirstId, int NextId, bool IsLastPage, IEnumerable<CatalogItem> Data);
