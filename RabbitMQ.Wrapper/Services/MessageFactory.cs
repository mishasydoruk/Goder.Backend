using RabbitMQ.Client;
using RabbitMQ.Wrapper.Models;
using System;

namespace RabbitMQ.Wrapper.Services
{
    public class MessageFactory : IDisposable
    {
        private readonly ConnectionFactory _factory;
        private IConnection connection;
        private IModel channel;

        public MessageFactory(RabbitMQOptions rabbitMQSettings)
        {
            _factory = new ConnectionFactory() { 
                HostName = rabbitMQSettings.Hostname, 
                Port = rabbitMQSettings.Port, 
                Password = rabbitMQSettings.Password, 
                UserName = rabbitMQSettings.UserName,
                VirtualHost = rabbitMQSettings.VirtualHost
            };
        }

        public void Dispose()
        {
            connection.Dispose();
            channel.Dispose();
        }

        public CommonQueueSettings GetQueueSettings(string queueName)
        {
            connection = _factory.CreateConnection();
            channel = connection.CreateModel();

            return new CommonQueueSettings() { Channel = channel, QueueName = queueName };
        }

        public SenderSettings GetSenderSettings(string queueName, string message, bool persistent)
        {
            connection = _factory.CreateConnection();
            channel = this.connection.CreateModel();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = persistent;

            return new SenderSettings() { Channel = channel, Message = message, Properties = properties, QueueName = queueName };
        }
    }
}
