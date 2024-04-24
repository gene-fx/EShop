using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LogginBehavior<TRequest, TResponse>
        (ILogger<LogginBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START]\r\n"
                                 + $" Handle request = {typeof(TRequest).Name}\r\n"
                                 + $" Response = {typeof(TResponse).Name}\r\n"
                                 + $" RequestData = {request}");

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTanken = timer.Elapsed;
            if (timeTanken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE]\r\n"
                                + $"The request {typeof(TRequest).Name} took {timeTanken.Seconds} seconds.\r\n"
                                + "[PERFORMANCE]");
            }

            logger.LogInformation($" Handle = {typeof(TRequest).Name}\r\n"
                                 + " Response = {typeof(TResponse)}\r\n"
                                 + "[END]");
            return response;
        }
    }
}