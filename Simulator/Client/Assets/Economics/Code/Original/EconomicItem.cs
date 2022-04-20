namespace F500.Consumerism
{
    /// <summary>
    /// An item in the economy.  Instance is a aggregation and unique per inventory.
    /// This type does not contain information about construction of the item (in
    /// the case of producers) or price and quantity (in the case of buying, selling and
    /// marketing)
    /// </summary>
    public class EconomicItem : IEconomicItem
    {
        public int Id { get; private set; }
        public string SystemId { get; private set; }
        public string Name { get; private set; }

        public EconomicItem(int id, string systemId, string name)
        {
            Id = id;
            SystemId = systemId;
            Name = name;
        }
    }
}