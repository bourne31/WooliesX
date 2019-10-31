using System.Collections.Generic;

namespace WooliesX.Technical.Exercises.External.Contracts.Responses
{
    public class ShopperHistoryResponse
    {
        public int CustomerId { get; set; }
        public IEnumerable<ProductResponse> Products { get; set; }
    }
}
