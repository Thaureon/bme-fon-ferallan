using bme_fon_ferallan.API.GasPullerModels;
using System.Net.Http.Headers;

namespace bme_fon_ferallan.API
{
    public class ApiPageViewModel(IGasPullerAPI gasPullerApi)
    {
        public async Task<StateUsaPriceModel> GetStateUsaPrice(string state)
        {
            var token = await SecureStorage.Default.GetAsync("auth_token");
            gasPullerApi.Authorization = new AuthenticationHeaderValue("Basic", token);
            return await gasPullerApi.GetStateUsaPrice(state);
        }

        public async Task<FromCityModel> GetFromCity(string city, string type)
        {
            var token = await SecureStorage.Default.GetAsync("auth_token");
            gasPullerApi.Authorization = new AuthenticationHeaderValue("Basic", token);
            return await gasPullerApi.GetFromCity(city, type);
        }
    }
}
