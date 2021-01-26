using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive
{
    class Receive
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() {
                Uri=new Uri("amqp://guest:guest@localhost:5762")
             };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "demo-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = new {nameof="product", message="Hello!"};
                var body=Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("","demo-queue",null,body);
            }
        }
    }
}
