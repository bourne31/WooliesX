using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Technical.Exercises.Models;

namespace WooliesX.Technical.Exercises.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(string sortOption);
    }
}