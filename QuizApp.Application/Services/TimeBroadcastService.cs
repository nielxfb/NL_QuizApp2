using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using QuizApp.Infrastructure.SignalR;

namespace QuizApp.Application.Services;

public class TimeBroadcastService : BackgroundService
{
    private readonly IHubContext<TimeHub> _hubContext;

    public TimeBroadcastService(IHubContext<TimeHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var serverTime = DateTime.Now;
            await _hubContext.Clients.All.SendAsync("ReceiveTime", serverTime, stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}