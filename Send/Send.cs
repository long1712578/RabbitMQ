using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send
{
    class Send
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

                var consumer=new EventingBasicConsumer(channel);
                consumer.Received+=(sender,e)=>{
                    var body=e.Body.ToArray();
                    var message=Encoding.UTF8.GetString(body);
                    console.WriteLine(message);
                }
            }
        }
    }
}
