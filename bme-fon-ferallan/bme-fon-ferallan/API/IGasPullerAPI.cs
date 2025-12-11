using bme_fon_ferallan.API.GasPullerModels;

using RestEase;
using System.Net.Http.Headers;

namespace bme_fon_ferallan.API
{
    public interface IGasPullerAPI
    {
        [Header("authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        [Get("stateUsaPrice")]
        Task<StateUsaPriceModel> GetStateUsaPrice([Query]string state);

        [Get("fromCity")]
        Task<FromCityModel> GetFromCity([Query] string city, [Query] string type);
    }
}