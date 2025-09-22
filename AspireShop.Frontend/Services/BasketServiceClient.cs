using Grpc.Core;
using Polly.Timeout;
using AspireShop.GrpcBasket;
using AspireShop.BasketService.Models;

namespace AspireShop.Frontend.Services;

public class BasketServiceClient(Basket.BasketClient client)
{
    public async Task<(CustomerBasket? CustomerBasket, bool IsAvailable)> GetBasketAsync(string buyerId)
    {
        try
        {
            var response = await client.GetBasketByIdAsync(new BasketRequest { Id = buyerId });
            var result = !string.IsNullOrEmpty(response.BuyerId) ? MapToCustomerBasket(response) : null;
            return (result, true);
        }
        catch (RpcException ex) when (
            // Service name could not be resolved
            ex.StatusCode is StatusCode.Unavailable ||
            // Polly resilience timed out after retries
            (ex.StatusCode is StatusCode.Internal && ex.Status.DebugException is TimeoutRejectedException))
        {
            return (null, false);
        }
    }

    public async Task<CustomerBasket> AddToCartAsync(string buyerId, int productId)
    {
        Console.WriteLine($"BasketServiceClient.AddToCartAsync called with buyerId: {buyerId}, productId: {productId}");
        
        var (basket, _) = await GetBasketAsync(buyerId);
        basket ??= new CustomerBasket(buyerId);
        
        Console.WriteLine($"Current basket has {basket.Items.Count} items");
        
        var found = false;
        foreach (var item in basket.Items)
        {
            if (item.ProductId == productId)
            {
                ++item.Quantity;
                found = true;
                Console.WriteLine($"Updated existing item {productId} quantity to {item.Quantity}");
                break;
            }
        }

        if (!found)
        {
            var newItem = new BasketItem
            {
                Id = Guid.NewGuid().ToString("N"),
                Quantity = 1,
                ProductId = productId
            };
            basket.Items.Add(newItem);
            Console.WriteLine($"Added new item {productId} to basket with ID {newItem.Id}");
        }

        var response = await client.UpdateBasketAsync(MapToCustomerBasketRequest(basket));
        var result = MapToCustomerBasket(response);
        
        Console.WriteLine($"Basket updated successfully. Total items: {result.TotalItemCount}");
        
        return result;
    }

    public async Task CheckoutBasketAsync(string buyerId)
    {
        _ = await client.CheckoutBasketAsync(new CheckoutCustomerBasketRequest { BuyerId = buyerId });
    }

    public async Task DeleteBasketAsync(string buyerId)
    {
        Console.WriteLine($"BasketServiceClient.DeleteBasketAsync called with buyerId: {buyerId}");
        _ = await client.DeleteBasketAsync(new DeleteCustomerBasketRequest { BuyerId = buyerId });
        Console.WriteLine($"Delete basket request sent for buyerId: {buyerId}");
    }

    private static CustomerBasketRequest MapToCustomerBasketRequest(CustomerBasket customerBasket)
    {
        var response = new CustomerBasketRequest
        {
            BuyerId = customerBasket.BuyerId
        };

        foreach (var item in customerBasket.Items)
        {
            response.Items.Add(new BasketItemResponse
            {
                Id = item.Id,
                OldUnitPrice = item.OldUnitPrice,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
        }

        return response;
    }

    private static CustomerBasket MapToCustomerBasket(CustomerBasketResponse wireBasket)
    {
        var response = new CustomerBasket
        {
            BuyerId = wireBasket.BuyerId
        };

        foreach (var item in wireBasket.Items)
        {
            response.Items.Add(new BasketItem
            {
                Id = item.Id,
                OldUnitPrice = item.OldUnitPrice,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
        }

        return response;
    }
}
