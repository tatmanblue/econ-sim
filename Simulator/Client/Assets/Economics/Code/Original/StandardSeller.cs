using System.Collections.Generic;

namespace F500.Consumerism
{
    public class StandardSeller : ISeller
    {
        public IMarketActionItem Item { get; set; }

        public StandardSeller()
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

            if (args.Price >= Item.Price)
            {
                List<IMarketPlace> markets = ServiceLocator.Current.GetMarkets();
                foreach (IMarketPlace m in markets)
                {
                    if (m.CanSell(args.Item, Item.Qty))
                        m.Sell(args.Item, Item.Qty);
                }
            }
        }
    }
}