using TatmanGames.Consumerism.Interfaces;

namespace  TatmanGames.Consumerism.Core
{
    public class MarketableItem : IMarketableItem
    {
        public IEconomicItem Item { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
    }
}