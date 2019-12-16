using Lib.AspNetCore.ServerSentEvents;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace sse_api.Services
{
    public class HeartbeatService : BackgroundService
    {
        #region Fields
        private const string HEARTBEAT_MESSAGE_FORMAT = "Heartbeat ({0} UTC)";

        private readonly IServerSentEventsService _serverSentEventsService;
        #endregion

        public HeartbeatService(IServerSentEventsService serverSentEventsService)
        {
            _serverSentEventsService = serverSentEventsService;
        }

        #region Methods
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _serverSentEventsService.SendEventAsync(String.Format(HEARTBEAT_MESSAGE_FORMAT, DateTime.UtcNow));

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
        #endregion

    }
}
