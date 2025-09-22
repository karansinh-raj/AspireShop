using System.Globalization;

namespace AspireShop.Frontend.Services;

public class CatalogServiceClient(HttpClient client)
{
    public Task<CatalogItemsPage?> GetItemsAsync(int? before = null, int? after = null)
    {
        // Make the query string with encoded parameters
        var query = (before, after) switch
        {
            (null, null) => default,
            (int b, null) => QueryString.Create("before", b.ToString(CultureInfo.InvariantCulture)),
            (null, int a) => QueryString.Create("after", a.ToString(CultureInfo.InvariantCulture)),
            _ => throw new InvalidOperationException(),
        };

        return client.GetFromJsonAsync<CatalogItemsPage>($"api/v1/catalog/items/type/all{query}");
    }
    
    public async Task<List<CatalogItem>> GetAllItemsAsync()
    {
        var allItems = new List<CatalogItem>();
        
        try
        {
            // Start with first page
            var currentPage = await GetItemsAsync();
            
            if (currentPage?.Data?.Any() == true)
            {
                allItems.AddRange(currentPage.Data);
                
                // Keep fetching until we get all items
                while (!currentPage.IsLastPage)
                {
                    currentPage = await GetItemsAsync(after: currentPage.NextId);
                    if (currentPage?.Data?.Any() == true)
                    {
                        allItems.AddRange(currentPage.Data);
                    }
                    else
                    {
                        break; // Safety break
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting all catalog items: {ex.Message}");
        }
        
        return allItems;
    }
}

public record CatalogItemsPage(int FirstId, int NextId, bool IsLastPage, IEnumerable<CatalogItem> Data);

public record CatalogItem
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public decimal Price { get; init; }
    public string? PictureUri { get; init; }
    public int CatalogBrandId { get; init; }
    public required string CatalogBrand { get; init; }
    public int CatalogTypeId { get; init; }
    public required string CatalogType { get; init; }
}
