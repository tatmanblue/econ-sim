using System;
using TatmanGames.Consumerism.Interfaces;

namespace TatmanGames.Consumerism
{
    public class PriceChangedEventArgs
    {
        public IMarketableItem Item { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public MarketChangeTriggers Trigger { get; set; } = MarketChangeTriggers.Error;
    }
    
    public class VolumeChangedEventArgs
    {
        public IMarketableItem Item { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public decimal Volume { get; set; }
        public MarketChangeTriggers Trigger { get; set; } = MarketChangeTriggers.Error;
    }

    public class QtyChangedEventArgs
    {
        public IMarketableItem Item { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public decimal Quantity { get; set; }
        public MarketChangeTriggers Trigger { get; set; } = MarketChangeTriggers.Error;
    }
    
    public delegate void PriceChangedEvent(PriceChangedEventArgs args);

    public delegate void VolumeChangedEvent(VolumeChangedEventArgs args);

    public delegate void QuantityChangedEvent(QtyChangedEventArgs args);
}