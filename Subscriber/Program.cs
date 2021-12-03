using System;
using System.Text.Json;
using SharedProject1;
using StackExchange.Redis;

namespace Subscriber
{
    internal static class Program
    {
        public static void Main()
        {
            using var redis = ConnectionMultiplexer
                .Connect(new ConfigurationOptions
                {
                    EndPoints = { "local:6379" },
                    DefaultDatabase = 1,
                    AllowAdmin = true,
                    Ssl = false,
                    ConnectTimeout = 6000,
                });
            var sub = redis.GetSubscriber();    
            
            sub.Subscribe("myTestChannel", (channel, message) => {
                Console.WriteLine(message);
            });

            Console.ReadKey();
        }
    }
}