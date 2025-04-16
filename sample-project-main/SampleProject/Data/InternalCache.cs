using Microsoft.Extensions.Caching.Memory;

namespace Data
{
    public class InternalCache : MemoryCache, IInternalCache
    {
        public InternalCache() : base(new MemoryCacheOptions())
        {
        }
    }
}
