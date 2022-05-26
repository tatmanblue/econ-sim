using System;
using System.Threading;
using F500.Consumerism;
using NUnit.Framework;
using TatmanGames.Consumerism;
using TatmanGames.Consumerism.Core;
using TatmanGames.Consumerism.Interfaces;
using UnityEditor.VersionControl;

namespace ConsumerismTests
{
    public class TestMarketPlace : MarketPlace
    {
        public void CreatePriceChangeEvent(IMarketableItem item, decimal newPrice, MarketChangeTriggers type = MarketChangeTriggers.System)
        {
            FirePriceChangeEvent(item, newPrice, type);
        }
    }
   
    public class BuyerTests
    {
        [Test]
        public void BuyerBuysAtPrice()
        {
            bool buyerBought = false;
            using (Semaphore wait = new Semaphore(0, 1, "BuyerBuysAtPrice"))
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
                    buyerBought = true;
                    wait.Release();
                };

                marketPlace.Items.Add(woodMarketItem.Item.Id, woodMarketItem);

                ServiceLocator.Current.RegisterMarket(marketPlace);
                MarketActionItem actionItem = new MarketActionItem()
                {
                    Item = woodItem,
                    ActionQty = 1,
                    Price = 30,
                    Qty = Constants.NOT_APPLICABLE,
                    Volume = Constants.NOT_APPLICABLE
                };

                StandardBuyer buyer = new StandardBuyer()
                {
                    Item = actionItem
                };

                // forcing the market to change the price, which will trigger the buyer
                // into buying mode
                marketPlace.CreatePriceChangeEvent(woodMarketItem, 25, MarketChangeTriggers.Sell);
                wait.WaitOne(TimeSpan.FromSeconds(1));
                Assert.IsTrue(buyerBought);
            }
        }

        [Test]
        public void BuyerDoesNotByAtPrice()
        {
            bool buyerBought = false;
            using (Semaphore wait = new Semaphore(0, 1, "BuyerDoesNotByAtPrice"))
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
                    buyerBought = true;
                    wait.Release();
                };

                marketPlace.Items.Add(woodMarketItem.Item.Id, woodMarketItem);

                ServiceLocator.Current.RegisterMarket(marketPlace);
                MarketActionItem actionItem = new MarketActionItem()
                {
                    Item = woodItem,
                    ActionQty = 1,
                    Price = 10,
                    Qty = Constants.NOT_APPLICABLE,
                    Volume = Constants.NOT_APPLICABLE
                };

                StandardBuyer buyer = new StandardBuyer()
                {
                    Item = actionItem
                };

                // forcing the market to change a price higher than it will buy at,
                // thus causing the buyer to ignore the price change event
                marketPlace.CreatePriceChangeEvent(woodMarketItem, 45, MarketChangeTriggers.Sell);
                wait.WaitOne(TimeSpan.FromSeconds(1));
                Assert.IsFalse(buyerBought);
            }
        }
    }
}