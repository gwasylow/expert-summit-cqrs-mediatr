using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatrCQRS.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MediatrCQRS.Logic.Behaviours
{
    public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheable
        where TResponse : CQRSResponse
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CachingBehaviour<TRequest, TResponse>> _logger;

        public CachingBehaviour(IMemoryCache cache, ILogger<CachingBehaviour<TRequest, TResponse>> logger)
        {
            this._cache = cache;
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //pre-logic
            var requestName = request.GetType();
            _logger.LogInformation("Summ-it: {Request} is configured for caching.", requestName);

            // Check to see if the item is inside the cache
            TResponse response;
            if (_cache.TryGetValue(request.CacheKey, out response))
            {
                _logger.LogInformation("Summ-it: Returning cached value for {Request}.", requestName);
                return response;
            }

            // Item is not in the cache, execute request and add to cache
            _logger.LogInformation("Summ-it: {Request} Cache Key: {Key} is not inside the cache, executing request.", requestName, request.CacheKey);
            response = await next(); // End of pre-logic


            //post-logic
            _cache.Set(request.CacheKey, response);

            return response;
        }
    }
}
