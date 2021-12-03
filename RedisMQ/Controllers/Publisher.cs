using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedProject1;
using StackExchange.Redis;

namespace RedisMQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {

        [HttpPost]
        public void SendRedisMessage(string message)
        {
            using var redis = ConnectionMultiplexer
                .Connect(new ConfigurationOptions
                {
                    EndPoints = {"local:6379"},
                    DefaultDatabase = 1,
                    AllowAdmin = true,
                    Ssl = false,
                    ConnectTimeout = 6000,
                });
            var sub = redis.GetSubscriber();
            var test = new MyTestObject
            {
                Id = 10,
                Message = message
            };

            var json = JsonSerializer.Serialize(test);
            
            sub.Publish("myTestChannel", json);
        }
    }
}