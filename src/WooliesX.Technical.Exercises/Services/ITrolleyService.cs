using System.Threading.Tasks;
using WooliesX.Technical.Exercises.External.Contracts.Requests;

namespace WooliesX.Technical.Exercises.Services
{
    public interface ITrolleyService
    {
        Task<decimal> GetTrolleyTotalAsync(TrolleyRequest trolleyRequest);
    }
}