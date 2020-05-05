using RabbitMQ.Client;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RabbitMQ.Fakes.models
{
    public class Exchange
    {
        public string Name { get; set; } 
        public string Type { get; set; } 
        public bool IsDurable { get; set; } 
        public bool AutoDelete { get; set; } 
        public IDictionary Arguments = new Dictionary<string, object>();

        public ConcurrentQueue<RabbitMessage> Messages = new ConcurrentQueue<RabbitMessage>();
        public ConcurrentDictionary<string,ExchangeQueueBinding> Bindings = new ConcurrentDictionary<string,ExchangeQueueBinding>();

        public void PublishMessage(RabbitMessage message)
        {
            this.Messages.Enqueue(message);

            if (string.IsNullOrWhiteSpace(message.RoutingKey))
            {
                foreach (var binding in Bindings)
                {
                    binding.Value.Queue.PublishMessage(message);
                }
            }
            else
            {
                foreach (var binding in MatchBindings(message.RoutingKey))
                {
                    binding.Queue.PublishMessage(message);
                }
            }
        }

        private IEnumerable<ExchangeQueueBinding> MatchBindings(string routingKey)
        {
            var matchingBindings = new List<ExchangeQueueBinding>();
            switch (this.Type)
            {
                case ExchangeType.Direct:
                    matchingBindings = Bindings
                        .Values
                        .Where(b => b.RoutingKey == routingKey).ToList();
                    break;
                case ExchangeType.Fanout:
                    matchingBindings = Bindings.Values.ToList();
                    break;
                case ExchangeType.Topic:
                    foreach (var binding in Bindings.Values)
                    {
                        var bindKeys = binding.RoutingKey.Split('.');
                        var msgKeys = routingKey.Split('.');
                        for (int i = 0; i < bindKeys.Count(); i++)
                        {
                            var bindKey = bindKeys[i];

                            if (bindKey == null)
                            {
                                break;
                            }
                            else if (bindKey == "#")
                            {
                                matchingBindings.Add(binding);
                                break; //match
                            }
                            else if (bindKey == "*")
                            {
                                if (msgKeys.Count() < i) // there must be a msg key
                                    break; //no match
                                else if (i == bindKeys.Length - 1)
                                {
                                    matchingBindings.Add(binding);
                                    break; //match
                                }
                                else
                                    continue; //potential match
                            }
                            else
                            {
                                if (msgKeys.Count() < i) // there must be a msg key
                                    break; //no match
                                var msgKey = msgKeys[i];
                                if (bindKey == msgKey)
                                    if (i == bindKeys.Length - 1)
                                    {
                                        matchingBindings.Add(binding);
                                        break; //match
                                    }
                                    else
                                        continue; //potential match
                                else
                                    break; //no match
                            }
                        }
                    }
                    break;
            }

            return matchingBindings;
        }
    }
}