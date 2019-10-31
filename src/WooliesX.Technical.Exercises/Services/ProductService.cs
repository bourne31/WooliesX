using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.Technical.Exercises.External.Clients;
using WooliesX.Technical.Exercises.External.Contracts.Responses;
using WooliesX.Technical.Exercises.Models;

namespace WooliesX.Technical.Exercises.Services
{
    public class ProductService : IProductService
    {
        private readonly IWolliesXApiClient _wolliesXApiClient;
        private readonly AppSettings _appSettings;

        public ProductService(IWolliesXApiClient wolliesXApiClient,
            IOptions<AppSettings> appSettings)
        {
            _wolliesXApiClient = wolliesXApiClient;
            _appSettings = appSettings.Value;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string sortOption)
        {
            try
            {
                var response = await _wolliesXApiClient.GetProductsAsync(_appSettings.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var products = MapProducts(response.Content);

                return await SortProducts(products, sortOption);
            }
            catch 
            {
                // Log Exception
                return null;
            }
        }

        private IEnumerable<Product> MapProducts(IEnumerable<ProductResponse> products)
        {
            if (products == null)
            {
                return null;
            }

            return products.Select(x => new Product
            {
                Name = x.Name,
                Price = x.Price,
                Quantity = (int)double.Parse(x.Quantity)
            });
        }

        private async Task<IEnumerable<Product>> SortProducts(IEnumerable<Product> products, string sortOption)
        {
            if (string.IsNullOrWhiteSpace(sortOption))
            {
                return products;
            }

            if (sortOption.Equals("recommended", StringComparison.InvariantCultureIgnoreCase))
            {
                var recommendedProducts = await GetRecommendedProducts();
                if (recommendedProducts == null)
                {
                    return products;
                }

                return products.OrderByDescending(x => recommendedProducts.Count(p => p.Name == x.Name));
            }

            return (sortOption.ToLower()) switch
            {
                "low" => products.OrderBy(x => x.Price),
                "high" => products.OrderByDescending(x => x.Price),
                "ascending" => products.OrderBy(x => x.Name),
                "descending" => products.OrderByDescending(x => x.Name),
                _ => products,
            };
        }

        private async Task<IEnumerable<ProductResponse>> GetRecommendedProducts()
        {
            try
            {
                var response = await _wolliesXApiClient.GetShopperHistoryAsync(_appSettings.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var products = response.Content.SelectMany(x => x.Products);

                return products;
            }
            catch
            {
                // Log Exception
                return null;
            }
        }
    }
}
