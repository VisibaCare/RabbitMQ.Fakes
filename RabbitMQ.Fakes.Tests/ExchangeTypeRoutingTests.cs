using NUnit.Framework;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Fakes.Tests
{
    [TestFixture]
    class ExchangeTypeRoutingTests
    {
        [Test]
        public void ExchangeTopic_hastag_wildcard_only()
        {
            // Arrange
            var node = new RabbitServer();
            var model = new FakeModel(node);

            model.ExchangeDeclare("my_exchange", ExchangeType.Topic);
            model.QueueDeclarePassive("my_queue");
            model.ExchangeBind("my_queue", "my_exchange", "#");

            var message = "hello world!";
            var encodedMessage = Encoding.ASCII.GetBytes(message);

            // Act
            model.BasicPublish("my_exchange", "a.b.c", new BasicProperties(), encodedMessage);

            // Assert
            Assert.That(node.Queues["my_queue"].Messages.Count, Is.EqualTo(1));
            Assert.That(node.Queues["my_queue"].Messages.First().Body, Is.EqualTo(encodedMessage));
        }

        [Test]
        public void ExchangeTopic_1_bindings_1_matches_star_wildcard()
        {
            // Arrange
            var node = new RabbitServer();
            var model = new FakeModel(node);

            model.ExchangeDeclare("my_exchange", ExchangeType.Topic);
            model.QueueDeclarePassive("my_queue");
            model.ExchangeBind("my_queue", "my_exchange", "apple.*");

            var message = "hello world!";
            var encodedMessage = Encoding.ASCII.GetBytes(message);

            // Act
            model.BasicPublish("my_exchange", "apple.blue", new BasicProperties(), encodedMessage);

            // Assert
            Assert.That(node.Queues["my_queue"].Messages.Count, Is.EqualTo(1));
            Assert.That(node.Queues["my_queue"].Messages.First().Body, Is.EqualTo(encodedMessage));
        }

        [Test]
        public void ExchangeTopic_3_bindings_1_matches_star_wildcard()
        {
            // Arrange
            var node = new RabbitServer();
            var model = new FakeModel(node);

            model.ExchangeDeclare("my_exchange", ExchangeType.Topic);
            model.QueueDeclarePassive("my_queue1");
            model.ExchangeBind("my_queue1", "my_exchange", "apple.*");
            model.QueueDeclarePassive("my_queue2");
            model.ExchangeBind("my_queue2", "my_exchange", "pear.*");
            model.QueueDeclarePassive("my_queue3");
            model.ExchangeBind("my_queue3", "my_exchange", "*.red");

            var message = "hello world!";
            var encodedMessage = Encoding.ASCII.GetBytes(message);

            // Act
            model.BasicPublish("my_exchange", "apple.red", new BasicProperties(), encodedMessage);

            // Assert
            Assert.That(node.Queues["my_queue1"].Messages.Count, Is.EqualTo(1));
            Assert.That(node.Queues["my_queue1"].Messages.First().Body, Is.EqualTo(encodedMessage));

            Assert.IsEmpty(node.Queues["my_queue2"].Messages);

            Assert.That(node.Queues["my_queue3"].Messages.Count, Is.EqualTo(1));
            Assert.That(node.Queues["my_queue3"].Messages.First().Body, Is.EqualTo(encodedMessage));
        }

        [Test]
        public void ExchangeTopic_1_bindings_0_matches_star_wildcard_nomatch()
        {
            // Arrange
            var node = new RabbitServer();
            var model = new FakeModel(node);

            model.ExchangeDeclare("my_exchange", ExchangeType.Topic);
            model.QueueDeclarePassive("my_queue");
            model.ExchangeBind("my_queue", "my_exchange", "apple.*");

            var message = "hello world!";
            var encodedMessage = Encoding.ASCII.GetBytes(message);

            // Act
            model.BasicPublish("my_exchange", "pear.red", new BasicProperties(), encodedMessage);

            Assert.IsEmpty(node.Queues["my_queue"].Messages);
        }

        [Test]
        public void ExchangeTopic_hashtag_wildcard_more_keys_in_message()
        {
            // Arrange
            var node = new RabbitServer();
            var model = new FakeModel(node);

            model.ExchangeDeclare("my_exchange", ExchangeType.Topic);
            model.QueueDeclarePassive("my_queue");
            model.ExchangeBind("my_queue", "my_exchange", "apple.*.#");

            var message = "hello world!";
            var encodedMessage = Encoding.ASCII.GetBytes(message);

            // Act
            model.BasicPublish("my_exchange", "apple.red.blue.green", new BasicProperties(), encodedMessage);

            Assert.That(node.Queues["my_queue"].Messages.Count, Is.EqualTo(1));
            Assert.That(node.Queues["my_queue"].Messages.First().Body, Is.EqualTo(encodedMessage));
        }

        [Test]
        public void ExchangeTopic_hashtag_wildcard_same_amount_of_keys()
        {
            // Arrange
            var node = new RabbitServer();
            var model = new FakeModel(node);

            model.ExchangeDeclare("my_exchange", ExchangeType.Topic);
            model.QueueDeclarePassive("my_queue");
            model.ExchangeBind("my_queue", "my_exchange", "apple.*.#");

            var message = "hello world!";
            var encodedMessage = Encoding.ASCII.GetBytes(message);

            // Act
            model.BasicPublish("my_exchange", "apple.red", new BasicProperties(), encodedMessage);

            Assert.That(node.Queues["my_queue"].Messages.Count, Is.EqualTo(1));
            Assert.That(node.Queues["my_queue"].Messages.First().Body, Is.EqualTo(encodedMessage));
        }
    }
}