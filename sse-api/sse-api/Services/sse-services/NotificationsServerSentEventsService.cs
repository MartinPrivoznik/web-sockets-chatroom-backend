using Lib.AspNetCore.ServerSentEvents;
using sse_api.Core.Services.sse_services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sse_api.Services.sse_services
{
    internal class NotificationsServerSentEventsService : ServerSentEventsService, INotificationsServerSentEventsService
    {
        public NotificationsServerSentEventsService()
        {
            ChangeReconnectIntervalAsync(5000);
        }
    }
}
