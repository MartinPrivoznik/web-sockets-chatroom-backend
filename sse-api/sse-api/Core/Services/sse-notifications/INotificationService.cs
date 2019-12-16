using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sse_api.Core.Services.sse_notifications
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string notification, bool alert);
    }
}
