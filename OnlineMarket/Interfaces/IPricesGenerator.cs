namespace OnlineMarket.Interfaces
{
    public interface IPricesGenerator
    {
        double[] CurrentPrices { get; set; }
        void Generate(object state);
    }
}
