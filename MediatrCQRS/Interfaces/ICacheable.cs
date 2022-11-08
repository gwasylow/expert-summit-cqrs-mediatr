using System;
namespace MediatrCQRS.Interfaces
{
    public interface ICacheable
    {
        public string CacheKey { get; }
    }
}
