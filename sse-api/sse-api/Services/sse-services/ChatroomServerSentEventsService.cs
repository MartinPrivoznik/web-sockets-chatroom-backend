using Lib.AspNetCore.ServerSentEvents;
using sse_api.Core.Services.sse_services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sse_api.Services.sse_services
{
    public class ChatroomServerSentEventsService : ServerSentEventsService, IChatroomServerSentEventsService
    {
        public ChatroomServerSentEventsService()
        {
            ChangeReconnectIntervalAsync(5000);
        }
    }
}
