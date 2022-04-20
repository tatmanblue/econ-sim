namespace F500.Consumerism
{
    public class MarketActionItem : IMarketActionItem
    {
        public IEconomicItem Item { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Volume { get; set; }
        public decimal ActionQty { get; set; }
    }
}