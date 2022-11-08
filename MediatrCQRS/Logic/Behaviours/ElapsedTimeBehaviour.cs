using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatrCQRS.Logic.Behaviours
{
    public class ElapsedTimeBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<ElapsedTimeBehaviour<TRequest, TResponse>> _logger;

        public ElapsedTimeBehaviour(ILogger<ElapsedTimeBehaviour<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //pre-logic
            var timer = Stopwatch.StartNew();

            var requestName = request.GetType();

            _logger.LogInformation("Summ-it: xxx: {Request} is starting.", requestName);
            _logger.LogInformation($"Summ-it: xxx: Timer for request: {timer.ElapsedMilliseconds}ms");


            //execute pre-logic, once completed -> use next() method to pass the request to the next handler/layer
            var response = await next();

            //post logic 
            _logger.LogInformation("Summ-it: xxx: {Request} has finished in {Time}ms.", requestName, timer.ElapsedMilliseconds);

            timer.Stop();

            //return response to the next handler/layer
            return response;
        }
    }
}
