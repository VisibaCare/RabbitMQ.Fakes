using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Fakes
{
    public class FakeConnection : IConnection
    {
        private readonly RabbitServer _server;

        public FakeConnection(RabbitServer server)
        {
            _server = server;
            Models = new List<FakeModel>();
            IsOpen = true;
        }

        public List<FakeModel> Models { get; private set; }

        public void Dispose()
        {
            Close();
        }

        public IModel CreateModel()
        {
            var model = new FakeModel(_server);
            Models.Add(model);

            return model;
        }

        public void Close()
        {
            Close(1, null, TimeSpan.Zero);
        }

        public void Close(ushort reasonCode, string reasonText)
        {
            Close(reasonCode, reasonText, TimeSpan.Zero);
        }

        public void Close(TimeSpan timeout)
        {
            Close(1, null, timeout);
        }

        public void Close(ushort reasonCode, string reasonText, TimeSpan timeout)
        {
            IsOpen = false;
            CloseReason = new ShutdownEventArgs(ShutdownInitiator.Library, reasonCode, reasonText);

            Models.ForEach(m=>m.Close());
        }

        public void Abort()
        {
            Abort(1, null, TimeSpan.Zero);
        }

        public void Abort(TimeSpan timeout)
        {
           Abort(1, null, timeout);
        }

        public void Abort(ushort reasonCode, string reasonText)
        {
            Abort(reasonCode, reasonText, TimeSpan.Zero);
        }

        public void Abort(ushort reasonCode, string reasonText, TimeSpan timeout)
        {
            IsOpen = false;
            CloseReason = new ShutdownEventArgs(ShutdownInitiator.Library,reasonCode,reasonText );

            this.Models.ForEach(m=>m.Abort());
        }

        public void UpdateSecret(string newSecret, string reason)
        {
        }

        public void HandleConnectionBlocked(string reason)
        {
            throw new NotImplementedException();
        }

        public void HandleConnectionUnblocked()
        {
            throw new NotImplementedException();
        }

        public string ClientProvidedName { get; }

#pragma warning disable CS0067 // Unused events (they're part of IConnection)

        public event EventHandler<CallbackExceptionEventArgs> CallbackException;
        public event EventHandler<EventArgs> RecoverySucceeded;
        public event EventHandler<ConnectionRecoveryErrorEventArgs> ConnectionRecoveryError;
        public event EventHandler<ConnectionBlockedEventArgs> ConnectionBlocked;
        public event EventHandler<ShutdownEventArgs> ConnectionShutdown;
        public event EventHandler<EventArgs> ConnectionUnblocked;

#pragma warning restore CS0067

        public ushort ChannelMax { get; set; }

        IDictionary<string, object> IConnection.ClientProperties
        {
            get { throw new NotImplementedException(); }
        }

        public uint FrameMax { get; set; }

        public TimeSpan Heartbeat { get; set; }

        public IDictionary<string, object> ClientProperties { get; set; }

        public IDictionary<string, object> ServerProperties { get; set; }

        public AmqpTcpEndpoint[] KnownHosts { get; set; }

        public ShutdownEventArgs CloseReason { get; set; }

        public bool IsOpen { get; set; }

        public bool AutoClose { get; set; }

        public IList<ShutdownReportEntry> ShutdownReport { get; set; }

        public AmqpTcpEndpoint Endpoint => throw new NotImplementedException();

        public IProtocol Protocol => throw new NotImplementedException();

        public int LocalPort => throw new NotImplementedException();

        public int RemotePort => throw new NotImplementedException();
    }
}