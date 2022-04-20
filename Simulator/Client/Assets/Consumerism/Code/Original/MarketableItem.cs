namespace F500.Consumerism
{
    public class MarketableItem : IMarketableItem
    {
        public IEconomicItem Item { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
    }
}