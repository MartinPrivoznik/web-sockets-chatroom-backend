using sse_api.Core.Services.sse_notifications;
using sse_api.Core.Services.sse_services;
using sse_api.Services.sse_services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sse_api.Services.sse_services
{
    internal class LocalNotificationsService : NotificationsServiceBase, INotificationService
    {
        #region Constructor
        public LocalNotificationsService(INotificationsServerSentEventsService notificationsServerSentEventsService)
            : base(notificationsServerSentEventsService)
        { }
        #endregion

        #region Methods
        public Task SendNotificationAsync(string notification, bool alert)
        {
            return SendSseEventAsync(notification, alert);
        }
        #endregion
    }
}