﻿using Lib.AspNetCore.ServerSentEvents;
using sse_api.Core.Services.sse_services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sse_api.Services.sse_services.Bases
{
    internal abstract class NotificationsServiceBase
    {
        #region Fields
        private INotificationsServerSentEventsService _notificationsServerSentEventsService;
        #endregion

        #region Constructor
        protected NotificationsServiceBase(INotificationsServerSentEventsService notificationsServerSentEventsService)
        {
            _notificationsServerSentEventsService = notificationsServerSentEventsService;
        }
        #endregion

        #region Methods
        protected Task SendSseEventAsync(string notification, bool alert)
        {
            return _notificationsServerSentEventsService.SendEventAsync(new ServerSentEvent   //Browser alert
            {
                Type = alert ? "alert" : null,
                Data = new List<string>(notification.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
            });
        }
        #endregion
    }
}