using MediatR;
using Serilog;
using System.Diagnostics;

namespace Business.Behaviors;

public class PerformanceLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;
    private readonly Stopwatch _stopwatch;

    public PerformanceLoggingBehavior(ILogger logger)
    {
        _stopwatch = new();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        _logger.Information($"Handling {name}.");

        _stopwatch.Start();
        var response = await next();
        _stopwatch.Stop();

        if (_stopwatch.ElapsedMilliseconds <= 3000) return response;

        _logger.Warning("Long Running Request: {name} ({ElapsedMilliseconds} milliseconds).",
            name, _stopwatch.ElapsedMilliseconds, request);

        return response;
    }
}
