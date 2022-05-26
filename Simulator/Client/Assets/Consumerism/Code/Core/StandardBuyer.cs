using System.Collections.Generic;
using TatmanGames.Consumerism.Interfaces;

namespace TatmanGames.Consumerism.Core
{
    /// <summary>
    /// A generic buyer that will buy an item off any market place when the
    /// price reaches a certain level
    /// </summary>
    public class StandardBuyer : IBuyer
    {
        public IMarketActionItem Item { get; set; }
        
        public StandardBuyer()
        {
            List<IMarketPlace> markets = ServiceLocator.Current.GetMarkets();
            foreach (IMarketPlace m in markets)
            {
                m.PriceChanged += OnPriceChangedHandler;
            }
        }

        private void OnPriceChangedHandler(PriceChangedEventArgs args)
        {
            // yikes.....
            if (args.Item.Item.Id != Item.Item.Id)
                return;

            if (args.Price <= Item.Price)
            {
                List<IMarketPlace> markets = ServiceLocator.Current.GetMarkets();
                foreach (IMarketPlace m in markets)
                {
                    if (m.CanBuy(args.Item, Item.Qty))
                        m.Buy(args.Item, Item.Qty);
                }
            }
        }
    }
}