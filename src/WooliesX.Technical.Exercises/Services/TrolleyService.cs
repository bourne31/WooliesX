using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WooliesX.Technical.Exercises.External.Clients;
using WooliesX.Technical.Exercises.External.Contracts.Requests;
using WooliesX.Technical.Exercises.Models;

namespace WooliesX.Technical.Exercises.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly IWolliesXApiClient _wolliesXApiClient;
        private readonly AppSettings _appSettings;

        public TrolleyService(IWolliesXApiClient wolliesXApiClient,
            IOptions<AppSettings> appSettings)
        {
            _wolliesXApiClient = wolliesXApiClient;
            _appSettings = appSettings.Value;
        }

        public async Task<decimal> GetTrolleyTotalAsync(TrolleyRequest trolleyRequest)
        {
            try
            {
                var response = await _wolliesXApiClient.GetTrolleyTotalAsync(_appSettings.Token, trolleyRequest);

                if (!response.IsSuccessStatusCode)
                {
                    return 0;
                }

                return response.Content;
            }
            catch
            {
                // Log Exception
                return 0;
            }
        }
    }
}
