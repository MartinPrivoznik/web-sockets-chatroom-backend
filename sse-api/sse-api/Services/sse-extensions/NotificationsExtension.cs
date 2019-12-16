using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sse_api.Core.Services.sse_notifications;
using sse_api.Services.sse_services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sse_api.Services.sse_extensions
{
    internal static class NotificationsExtension
    {
        #region Fields
        private const string NOTIFICATIONS_SERVICE_TYPE_CONFIGURATION_KEY = "NotificationsService";
        private const string NOTIFICATIONS_SERVICE_TYPE_LOCAL = "Local";
        private const string NOTIFICATIONS_SERVICE_TYPE_REDIS = "Redis";
        #endregion

        #region Methods
        public static IServiceCollection AddNotificationsService(this IServiceCollection services, IConfiguration configuration)
        {
            string notificationsServiceType = configuration.GetValue(NOTIFICATIONS_SERVICE_TYPE_CONFIGURATION_KEY, NOTIFICATIONS_SERVICE_TYPE_LOCAL);

            if (notificationsServiceType.Equals(NOTIFICATIONS_SERVICE_TYPE_LOCAL, StringComparison.InvariantCultureIgnoreCase))
            {
                //
                services.AddTransient<INotificationService, LocalNotificationsService>();
            }
            else if (notificationsServiceType.Equals(NOTIFICATIONS_SERVICE_TYPE_REDIS, StringComparison.InvariantCultureIgnoreCase))
            {
                //
                services.AddSingleton<INotificationService, RedisNotificationsService>();
            }
            else
            {
                throw new NotSupportedException($"Not supported {nameof(INotificationService)} type.");
            }

            return services;
        }
        #endregion
    }
}