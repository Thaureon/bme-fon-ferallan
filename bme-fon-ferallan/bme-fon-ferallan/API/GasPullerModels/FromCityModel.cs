namespace bme_fon_ferallan.API.GasPullerModels
{
    public class FromCityModel
    {
        public bool Success;

        public List<GasPrice> Result;
    }

    public class GasPrice
    {
        public string Country;

        public string Currency;

        public double Gasoline;

        public double Diesel;

        public double Lpg;
    }
}
