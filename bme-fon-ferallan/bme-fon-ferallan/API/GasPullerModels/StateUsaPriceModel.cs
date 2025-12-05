namespace bme_fon_ferallan.API.GasPullerModels
{
    public class StateUsaPriceModel
    {
        public bool Success;
        public StateUsaPrice Result;
    }

    public class StateUsaPrice
    {
        public State State;
        public List<City> Cities;

    }

    public class City
    {
        public string Name;

        public string LowerName;

        public string Currency;

        public double Gasoline;

        public double MidGrade;

        public double Premium;

        public double Diesel;
    }

    public class State
    {
        public string Name;

        public string LowerName;

        public string Currency;

        public double Gasoline;

        public double MidGrade;

        public double Premium;

        public double Diesel;
    }
}
