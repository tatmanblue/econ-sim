using System;
using TatmanGames.Consumerism;
using TatmanGames.Consumerism.Interfaces;

namespace F500.Consumerism
{
    public class ComputePriceAdjustmentData
    {
        public IMarketableItem Item { get; set; }
        public decimal Quantity { get; set; }
        public MarketChangeTriggers Trigger { get; set; } = MarketChangeTriggers.Error;
    }
    
    public class PriceChangedResponse
    {
        public IMarketableItem Item { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public decimal PriceAdjustment { get; set; }
        public bool NotApplicable { get; set; } = true;
        public MarketChangeTriggers Trigger { get; set; } = MarketChangeTriggers.Error;
    }
}