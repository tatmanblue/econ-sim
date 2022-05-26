using System;
using System.Collections.Generic;
using F500.Consumerism;
using TatmanGames.Consumerism;
using TatmanGames.Consumerism.Interfaces;

namespace  TatmanGames.Consumerism.Core
{
    /// <summary>
    /// Market place is the store, where buyers and sellers do their transactions
    /// </summary>
    public class MarketPlace : IMarketPlace
    {
        private PriceChangedEvent priceChangedEvent;
        private QuantityChangedEvent qtyChangedEvent;
        private VolumeChangedEvent volumeChangedEvent;

        public Dictionary<int, IMarketableItem> Items { get; } = new Dictionary<int, IMarketableItem>();
        
        public event PriceChangedEvent PriceChanged
        {
            add
            {
                priceChangedEvent -= value;
                priceChangedEvent += value;
            }
            remove
            {
                priceChangedEvent -= value;
            }
        }
        public event QuantityChangedEvent QuantityChanged
        {
            add
            {
                qtyChangedEvent -= value;
                qtyChangedEvent += value;
            }
            remove
            {
                qtyChangedEvent -= value;
            }
        }
        public event VolumeChangedEvent VolumeChanged
        {
            add
            {
                volumeChangedEvent -= value;
                volumeChangedEvent += value;
            }
            remove
            {
                volumeChangedEvent -= value;
            }
        }

        protected void FirePriceChangeEvent(IMarketableItem item, decimal newPrice, MarketChangeTriggers type)
        {
            PriceChangedEvent safeEvent = priceChangedEvent;
            if (null == safeEvent) return;
            PriceChangedEventArgs args = new PriceChangedEventArgs
            {
                Item = item,
                Price = newPrice,
                Trigger = type
            };

            safeEvent(args);
        }

        protected void FireQtyChangeEvent(IMarketableItem item, decimal newQty, MarketChangeTriggers type)
        {
            QuantityChangedEvent safeEvent = qtyChangedEvent;
            if (null == safeEvent) return;
            QtyChangedEventArgs args = new QtyChangedEventArgs
            {
                Item = item,
                Quantity = newQty,
                Trigger = type
            };

            safeEvent(args);
        }

        protected void FireVolumeChangeEvent(IMarketableItem item, decimal newVolume, MarketChangeTriggers type)
        {
            VolumeChangedEvent safeEvent = volumeChangedEvent;
            if (null == safeEvent) return;
            VolumeChangedEventArgs args = new VolumeChangedEventArgs
            {
                Item = item,
                Volume = newVolume,
                Trigger = type
            };

            safeEvent(args);
        }

        /// <summary>
        /// Applies price adjustments when the qty of item changes in the market place
        /// </summary>
        /// <param name="item"></param>
        /// <param name="qty"></param>
        /// <param name="type"></param>
        protected void ApplyPriceAdjustments(IMarketableItem item, decimal qty, MarketChangeTriggers type)
        {
            List<IMarketResponder> responders = ServiceLocator.Current.GetResponders();
            // we always assume there is more than one responder, especially since there can
            // be different responders for MarketChangeTriggers
            foreach (IMarketResponder res in responders)
            {
                PriceChangedResponse response = res.ComputePrice(new ComputePriceAdjustmentData
                {
                    Item = item,
                    Quantity = qty,
                    Trigger = type
                });

                if (response.NotApplicable == false)
                    item.Price += response.PriceAdjustment;
            }
            
            // to avoid a flurry of buy/sell orders that result from price change, only fire
            // this event once after all responders have made their adjustments
            FirePriceChangeEvent(item, item.Price, type);
        }
        
        public bool CanBuy(IMarketableItem item, decimal qty)
        {
            return true;
        }

        public void Buy(IMarketableItem item, decimal qty)
        {
            FireQtyChangeEvent(item, qty, MarketChangeTriggers.Buy);
            ApplyPriceAdjustments(item, qty, MarketChangeTriggers.Buy);
        }


        public bool CanSell(IMarketableItem item, decimal qty)
        {
            return true;
        }

        public void Sell(IMarketableItem item, decimal qty)
        {
            FireQtyChangeEvent(item, qty, MarketChangeTriggers.Sell);
            ApplyPriceAdjustments(item, qty, MarketChangeTriggers.Buy);
        }
    }
    
}