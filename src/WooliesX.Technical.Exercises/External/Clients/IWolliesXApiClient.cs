using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Technical.Exercises.External.Contracts.Responses;

namespace WooliesX.Technical.Exercises.External.Clients
{
    public interface IWolliesXApiClient
    {
        [Get("/api/resource/products")]
        Task<ApiResponse<IEnumerable<ProductResponse>>> GetProductsAsync(string token);

        [Get("/api/resource/shopperhistory")]
        Task<ApiResponse<IEnumerable<ShopperHistoryResponse>>> GetShopperHistoryAsync(string token);
    }
}
