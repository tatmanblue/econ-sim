using System.Collections.Generic;
using F500.Consumerism;
using TatmanGames.Consumerism;
using TatmanGames.Consumerism.Interfaces;

namespace  TatmanGames.Consumerism.Core
{
    /// <summary>
    /// When the quantity changes adjust the price by
    /// PriceChange * WhenQtyChange
    ///
    /// This responder takes no consideration for economic or market availability
    /// </summary>
    public class LinearPriceResponder : IMarketResponder
    {
        public decimal AdjustPriceBy { get; private set; }
        public decimal WhenQtyChangesBy { get; private set; }

        public LinearPriceResponder(decimal adjustPriceBy = 1, decimal whenQtyChangesBy = 1)
        {
            AdjustPriceBy = adjustPriceBy;
            WhenQtyChangesBy = whenQtyChangesBy;
        }

        public PriceChangedResponse ComputePrice(ComputePriceAdjustmentData args)
        {
            PriceChangedResponse response = new PriceChangedResponse()
            {
                Item = args.Item,
                Trigger = args.Trigger
            };

            if (args.Trigger == MarketChangeTriggers.Error || args.Trigger == MarketChangeTriggers.System)
                return response;
            
            if (args.Quantity < WhenQtyChangesBy)
                return response;

            int modifer = Constants.PRICE_GOES_UP;
            if (args.Trigger == MarketChangeTriggers.Sell)
                modifer = Constants.PRICE_GOES_DOWN;
            
            decimal priceAdjustment = WhenQtyChangesBy * args.Quantity * modifer;
            response.PriceAdjustment = priceAdjustment;
            response.NotApplicable = false;
            return response;
        }
    }
}