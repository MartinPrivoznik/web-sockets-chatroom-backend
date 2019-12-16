using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sse_api.Core.Services.sse_services;
using sse_api.Data.Models;

namespace sse_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatroomServerSentEventsService _chatroomServerSentEventsService;

        public ChatRoomController(IChatroomServerSentEventsService chatroomServerSentEventsService)
        {
            _chatroomServerSentEventsService = chatroomServerSentEventsService;
        }

        // GET: api/ChatRoom
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ChatRoom/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChatRoom
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody]MessageType message)
        {
            if (!String.IsNullOrEmpty(message.Message))
            {
                await _chatroomServerSentEventsService.SendEventAsync(message.Message);
            }

            ModelState.Clear();

            return message.Message;
        }

        // PUT: api/ChatRoom/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
