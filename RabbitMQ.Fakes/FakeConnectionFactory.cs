using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace RabbitMQ.Fakes
{
    public class FakeConnectionFactory : IAsyncConnectionFactory, IConnectionFactory
    {
        public IConnection Connection { get; private set; }
        public RabbitServer Server { get; private set; }

        public FakeConnectionFactory():this(new RabbitServer())
        {
           
        }

        public FakeConnectionFactory(RabbitServer server)
        {
            Server = server;
        }

        public FakeConnectionFactory WithConnection(IConnection connection)
        {
            Connection = connection;
            return this;
        }

        public FakeConnectionFactory WithRabbitServer(RabbitServer server)
        {
            Server = server;
            return this;
        }

        public FakeConnection UnderlyingConnection
        {
            get { return (FakeConnection)Connection; }
        }

        public List<FakeModel> UnderlyingModel
        {
            get
            {
                var connection = UnderlyingConnection;
                if (connection == null)
                    return null;

                return connection.Models;
            }
        }

        public IConnection CreateConnection()
        {
            if (Connection == null)
                Connection = new FakeConnection(Server);

            return Connection;
        }

        public IConnection CreateConnection(string clientProvidedName) => CreateConnection();
        public IConnection CreateConnection(IList<string> hostnames) => CreateConnection();
        public IConnection CreateConnection(IList<string> hostnames, string clientProvidedName) => CreateConnection();
        public IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints) => CreateConnection();
        public IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName) => CreateConnection();


        public IAuthMechanismFactory AuthMechanismFactory(IList<string> mechanismNames)
        {
            throw new NotImplementedException();
        }

        public bool DispatchConsumersAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDictionary<string, object> ClientProperties { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ushort RequestedChannelMax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint RequestedFrameMax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan RequestedHeartbeat { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseBackgroundThreadsForIO { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string VirtualHost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Uri Uri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ClientProvidedName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan HandshakeContinuationTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan ContinuationTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}