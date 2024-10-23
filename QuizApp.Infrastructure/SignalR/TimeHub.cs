using Microsoft.AspNetCore.SignalR;

namespace QuizApp.Infrastructure.SignalR;

public class TimeHub : Hub
{
    public async Task SendTime(DateTime serverTime)
    {
        await Clients.All.SendAsync("ReceiveTime", serverTime);
    }
}