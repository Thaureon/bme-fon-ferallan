using bme_fon_ferallan.API.GasPullerModels;

using RestEase;

namespace bme_fon_ferallan.API
{
    public interface IGasPullerAPI
    {
        [Get("stateUsaPrice")]
        Task<StateUsaPriceModel> GetStateUsaPrice([Path]string state);

        [Get("fromCity")]
        Task<FromCityModel> GetFromCity([Path] string city, [Path] string type);
    }
}