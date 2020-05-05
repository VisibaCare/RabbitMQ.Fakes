using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;

namespace RabbitMQ.Fakes.Tests
{
    public class QueueingBasicConsumer : DefaultBasicConsumer
    {
        public ConcurrentQueue<BasicDeliverEventArgs> Queue = new ConcurrentQueue<BasicDeliverEventArgs>();

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var message = new BasicDeliverEventArgs(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);

            Queue.Enqueue(message);
        }
    }
}
