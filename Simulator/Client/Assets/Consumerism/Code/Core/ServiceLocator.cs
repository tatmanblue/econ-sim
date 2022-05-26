using System.Collections.Generic;
using TatmanGames.Consumerism.Interfaces;

namespace F500.Consumerism
{
    public class ServiceLocator
    {
        private List<IMarketPlace> markets = new List<IMarketPlace>();
        private List<IMarketResponder> responders = new List<IMarketResponder>();

        public static ServiceLocator Current { get; private set; } = new ServiceLocator();
        public static void Initiailze(){}

        public void RegisterMarket(IMarketPlace market)
        {
            markets.Add(market);
        }

        public void RegisterMarketResponder(IMarketResponder responder)
        {
            responders.Add(responder);
        }

        public List<IMarketPlace> GetMarkets()
        {
            // hmmm....do we need to worry about deep copy?
            return new List<IMarketPlace>(markets.ToArray());
        }

        public List<IMarketResponder> GetResponders()
        {
            return new List<IMarketResponder>(responders.ToArray());
        }
    }
}