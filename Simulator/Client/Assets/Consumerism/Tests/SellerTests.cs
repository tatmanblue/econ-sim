using System;
using System.Threading;
using F500.Consumerism;
using NUnit.Framework;
using TatmanGames.Consumerism;
using TatmanGames.Consumerism.Core;

namespace ConsumerismTests
{
    public class SellerTests
    {
        [Test]
        public void SellerSellsAtPrice()
        {
            bool sellerSold = false;
            using (Semaphore wait = new Semaphore(0, 1, "SellsAtPrice"))
            {
                EconomicItem woodItem = new EconomicItem(1, "1", "wood");
                MarketableItem woodMarketItem = new MarketableItem()
                {
                    Item = woodItem,
                    Price = 50,
                    Qty = 1000
                };

                TestMarketPlace marketPlace = new TestMarketPlace();

                marketPlace.QuantityChanged += args =>
                {
                    sellerSold = true;
                    wait.Release();
                };

                marketPlace.Items.Add(woodMarketItem.Item.Id, woodMarketItem);

                ServiceLocator.Current.RegisterMarket(marketPlace);
                MarketActionItem actionItem = new MarketActionItem()
                {
                    Item = woodItem,
                    ActionQty = 1,
                    Price = 60,
                    Qty = Constants.NOT_APPLICABLE,
                    Volume = Constants.NOT_APPLICABLE
                };

                StandardSeller seller = new StandardSeller()
                {
                    Item = actionItem
                };

                // forcing the market to change the price, which will trigger the seller
                // into selling mode
                marketPlace.CreatePriceChangeEvent(woodMarketItem, 75, MarketChangeTriggers.Buy);
                wait.WaitOne(TimeSpan.FromSeconds(1));
                Assert.IsTrue(sellerSold);
            }
        }

        [Test]
        public void SellerDoesNotSellAtPrice()
        {
            bool sellerSold = false;
            using (Semaphore wait = new Semaphore(0, 1, "SellerDoesNotSellAtPrice"))
            {
                EconomicItem woodItem = new EconomicItem(1, "1", "wood");
                MarketableItem woodMarketItem = new MarketableItem()
                {
                    Item = woodItem,
                    Price = 50,
                    Qty = 1000
                };

                TestMarketPlace marketPlace = new TestMarketPlace();

                marketPlace.QuantityChanged += args =>
                {
                    sellerSold = true;
                    wait.Release();
                };

                marketPlace.Items.Add(woodMarketItem.Item.Id, woodMarketItem);

                ServiceLocator.Current.RegisterMarket(marketPlace);
                MarketActionItem actionItem = new MarketActionItem()
                {
                    Item = woodItem,
                    ActionQty = 1,
                    Price = 60,
                    Qty = Constants.NOT_APPLICABLE,
                    Volume = Constants.NOT_APPLICABLE
                };

                StandardSeller seller = new StandardSeller()
                {
                    Item = actionItem
                };

                // forcing the market to change to a price lower, which the seller
                // will igore and not sell anything
                marketPlace.CreatePriceChangeEvent(woodMarketItem, 25, MarketChangeTriggers.Buy);
                wait.WaitOne(TimeSpan.FromSeconds(1));
                Assert.IsFalse(sellerSold);
            }
            
        }
    }
}