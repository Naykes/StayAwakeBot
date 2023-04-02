using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StayAwakeBot;

internal class BotService : IHostedService, IDisposable
{
    ILogger<BotService> _logger;
    IUserInterfaceService _userInterfaceService;
    (int, int) screen = (0, 0);
    Timer? _timer = null;

    public BotService(IUserInterfaceService userInterfaceService, ILogger<BotService> logger)
    {
        _userInterfaceService = userInterfaceService;
        _logger = logger;
        screen = _userInterfaceService.GetScreenResolution();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Screen detected {screen}");
        _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }

    private void DoWork(object? state) 
    {
        (int, int) position = _userInterfaceService.GetCursorPossition();
        _logger.LogInformation($"Cursor position {position}");

        if (position.Item1 < screen.Item1)
        {
            _logger.LogInformation("Changing current position");
            _userInterfaceService.MoveMouse(position.Item1 + 1, position.Item2);
        }
        else
        {
            _logger.LogInformation("Changing current position");
            _userInterfaceService.MoveMouse(position.Item1 - 1, position.Item2);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopped");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
    public void Dispose()
    {
        _timer?.Dispose();
    }
}
