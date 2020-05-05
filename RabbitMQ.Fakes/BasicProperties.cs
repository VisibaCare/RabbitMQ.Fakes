﻿using RabbitMQ.Client;
using System.Collections.Generic;

namespace RabbitMQ.Fakes
{
    public abstract class BasicPropertiesBase : IBasicProperties
    {
        public abstract string AppId { get; set; }
        public abstract string ClusterId { get; set; }
        public abstract string ContentEncoding { get; set; }
        public abstract string ContentType { get; set; }
        public abstract string CorrelationId { get; set; }
        public abstract byte DeliveryMode { get; set; }
        public abstract string Expiration { get; set; }
        public abstract IDictionary<string, object> Headers { get; set; }
        public abstract string MessageId { get; set; }

        public bool Persistent
        {
            get { return DeliveryMode == 2; }
            set { DeliveryMode = value ? (byte)2 : (byte)1; }
        }

        public abstract byte Priority { get; set; }
        public abstract string ReplyTo { get; set; }

        public PublicationAddress ReplyToAddress
        {
            get { return PublicationAddress.Parse(ReplyTo); }
            set { ReplyTo = value.ToString(); }
        }

        public abstract AmqpTimestamp Timestamp { get; set; }
        public abstract string Type { get; set; }
        public abstract string UserId { get; set; }

        IDictionary<string, object> IBasicProperties.Headers { get; set; } = new Dictionary<string, object>();

        public ushort ProtocolClassId { get; set; }

        public string ProtocolClassName { get; set; }

        public abstract void ClearAppId();

        public abstract void ClearClusterId();

        public abstract void ClearContentEncoding();

        public abstract void ClearContentType();

        public abstract void ClearCorrelationId();

        public abstract void ClearDeliveryMode();

        public abstract void ClearExpiration();

        public abstract void ClearHeaders();

        public abstract void ClearMessageId();

        public abstract void ClearPriority();

        public abstract void ClearReplyTo();

        public abstract void ClearTimestamp();

        public abstract void ClearType();

        public abstract void ClearUserId();

        public abstract bool IsAppIdPresent();

        public abstract bool IsClusterIdPresent();

        public abstract bool IsContentEncodingPresent();

        public abstract bool IsContentTypePresent();

        public abstract bool IsCorrelationIdPresent();

        public abstract bool IsDeliveryModePresent();

        public abstract bool IsExpirationPresent();

        public abstract bool IsHeadersPresent();

        public abstract bool IsMessageIdPresent();

        public abstract bool IsPriorityPresent();

        public abstract bool IsReplyToPresent();

        public abstract bool IsTimestampPresent();

        public abstract bool IsTypePresent();

        public abstract bool IsUserIdPresent();
    }
}

namespace RabbitMQ.Client.Framing
{
    /// <summary>Autogenerated type. AMQP specification content header properties for content class "basic"</summary>
    public sealed class BasicProperties : Fakes.BasicPropertiesBase
    {
        private string _contentType;
        private string _contentEncoding;
        private IDictionary<string, object> _headers;
        private byte _deliveryMode;
        private byte _priority;
        private string _correlationId;
        private string _replyTo;
        private string _expiration;
        private string _messageId;
        private AmqpTimestamp _timestamp;
        private string _type;
        private string _userId;
        private string _appId;
        private string _clusterId;

        private bool _contentType_present = false;
        private bool _contentEncoding_present = false;
        private bool _headers_present = false;
        private bool _deliveryMode_present = false;
        private bool _priority_present = false;
        private bool _correlationId_present = false;
        private bool _replyTo_present = false;
        private bool _expiration_present = false;
        private bool _messageId_present = false;
        private bool _timestamp_present = false;
        private bool _type_present = false;
        private bool _userId_present = false;
        private bool _appId_present = false;
        private bool _clusterId_present = false;

        public override string ContentType
        {
            get => _contentType;
            set
            {
                _contentType_present = value != null;
                _contentType = value;
            }
        }

        public override string ContentEncoding
        {
            get => _contentEncoding;
            set
            {
                _contentEncoding_present = value != null;
                _contentEncoding = value;
            }
        }

        public override IDictionary<string, object> Headers
        {
            get => _headers;
            set
            {
                _headers_present = value != null;
                _headers = value;
            }
        }

        public override byte DeliveryMode
        {
            get => _deliveryMode;
            set
            {
                _deliveryMode_present = true;
                _deliveryMode = value;
            }
        }

        public override byte Priority
        {
            get => _priority;
            set
            {
                _priority_present = true;
                _priority = value;
            }
        }

        public override string CorrelationId
        {
            get => _correlationId;
            set
            {
                _correlationId_present = value != null;
                _correlationId = value;
            }
        }

        public override string ReplyTo
        {
            get => _replyTo;
            set
            {
                _replyTo_present = value != null;
                _replyTo = value;
            }
        }

        public override string Expiration
        {
            get => _expiration;
            set
            {
                _expiration_present = value != null;
                _expiration = value;
            }
        }

        public override string MessageId
        {
            get => _messageId;
            set
            {
                _messageId_present = value != null;
                _messageId = value;
            }
        }

        public override AmqpTimestamp Timestamp
        {
            get => _timestamp;
            set
            {
                _timestamp_present = true;
                _timestamp = value;
            }
        }

        public override string Type
        {
            get => _type;
            set
            {
                _type_present = value != null;
                _type = value;
            }
        }

        public override string UserId
        {
            get => _userId;
            set
            {
                _userId_present = value != null;
                _userId = value;
            }
        }

        public override string AppId
        {
            get => _appId;
            set
            {
                _appId_present = value != null;
                _appId = value;
            }
        }

        public override string ClusterId
        {
            get => _clusterId;
            set
            {
                _clusterId_present = value != null;
                _clusterId = value;
            }
        }

        public override void ClearContentType() => _contentType_present = false;

        public override void ClearContentEncoding() => _contentEncoding_present = false;

        public override void ClearHeaders() => _headers_present = false;

        public override void ClearDeliveryMode() => _deliveryMode_present = false;

        public override void ClearPriority() => _priority_present = false;

        public override void ClearCorrelationId() => _correlationId_present = false;

        public override void ClearReplyTo() => _replyTo_present = false;

        public override void ClearExpiration() => _expiration_present = false;

        public override void ClearMessageId() => _messageId_present = false;

        public override void ClearTimestamp() => _timestamp_present = false;

        public override void ClearType() => _type_present = false;

        public override void ClearUserId() => _userId_present = false;

        public override void ClearAppId() => _appId_present = false;

        public override void ClearClusterId() => _clusterId_present = false;

        public override bool IsContentTypePresent() => _contentType_present;

        public override bool IsContentEncodingPresent() => _contentEncoding_present;

        public override bool IsHeadersPresent() => _headers_present;

        public override bool IsDeliveryModePresent() => _deliveryMode_present;

        public override bool IsPriorityPresent() => _priority_present;

        public override bool IsCorrelationIdPresent() => _correlationId_present;

        public override bool IsReplyToPresent() => _replyTo_present;

        public override bool IsExpirationPresent() => _expiration_present;

        public override bool IsMessageIdPresent() => _messageId_present;

        public override bool IsTimestampPresent() => _timestamp_present;

        public override bool IsTypePresent() => _type_present;

        public override bool IsUserIdPresent() => _userId_present;

        public override bool IsAppIdPresent() => _appId_present;

        public override bool IsClusterIdPresent() => _clusterId_present;
    }
}
