namespace VL.Common.Caching.MemoryCache
{
    public class MemoryCacheBuilder : ICacheBuilder
    {
        public MemoryCacheBuilder() { }

        public override ICacheProvider GetInstance(string regionName="")
        {
            if (string.IsNullOrWhiteSpace(regionName))
            {
                return new MemoryCacheProvider(regionName);
            }
            else
            {
                return new MemoryCacheProvider(DefaultRegionName);
            }
        }
    }
}
