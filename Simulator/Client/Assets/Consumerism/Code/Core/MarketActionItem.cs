using TatmanGames.Consumerism.Interfaces;

namespace TatmanGames.Consumerism.Core
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