﻿using Microsoft.Extensions.Configuration;
using sse_api.Core.Services.sse_notifications;
using sse_api.Core.Services.sse_services;
using sse_api.Services.sse_services.Bases;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sse_api.Services.sse_services
{
    internal class RedisNotificationsService : NotificationsServiceBase, INotificationService
    {
        #region Fields
        private const string CONNECTION_MULTIPLEXER_CONFIGURATION_KEY = "RedisConnectionMultiplexerConfiguration";

        private const string NOTIFICATIONS_CHANNEL = "NOTIFICATIONS";
        private const string ALERTS_CHANNEL = "ALERTS";

        private ConnectionMultiplexer _redis;
        #endregion

        #region Constructor
        public RedisNotificationsService(INotificationsServerSentEventsService notificationsServerSentEventsService, IConfiguration configuration)
            : base(notificationsServerSentEventsService)
        {
            _redis = ConnectionMultiplexer.Connect(configuration.GetValue<String>(CONNECTION_MULTIPLEXER_CONFIGURATION_KEY));

            ISubscriber subscriber = _redis.GetSubscriber();
            subscriber.Subscribe(NOTIFICATIONS_CHANNEL, async (channel, message) => { await SendSseEventAsync(message, false); });
            subscriber.Subscribe(ALERTS_CHANNEL, async (channel, message) => { await SendSseEventAsync(message, true); });
        }
        #endregion

        #region Methods
        public Task SendNotificationAsync(string notification, bool alert)
        {
            ISubscriber subscriber = _redis.GetSubscriber();

            return subscriber.PublishAsync(alert ? ALERTS_CHANNEL : NOTIFICATIONS_CHANNEL, notification);
        }
        #endregion
    }
}
