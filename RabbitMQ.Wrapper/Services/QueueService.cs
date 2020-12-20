using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Wrapper.Models;
using System.Text;

namespace RabbitMQ.Wrapper.Services
{
    public class QueueService
    {
        public delegate void MessageHandler(string message, ulong deliveryTag);
        public event MessageHandler ReceiveMessage;
        public event MessageHandler SendMessage;

        public void ListenQueue(CommonQueueSettings queueSettings, bool autoAck)
        {
            var consumer = new EventingBasicConsumer(queueSettings.Channel);

            queueSettings.Channel.BasicConsume(queue: queueSettings.QueueName,
                                 autoAck: autoAck,
                                 consumer: consumer);

            consumer.Received += getMessage;
        }

        public void DeclareQueue(CommonQueueSettings queueSettings)
        {
            queueSettings.Channel.QueueDeclare(queue: queueSettings.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            queueSettings.Channel.BasicQos(0, 1, false);
        }

        public void SendMessageToQueue(SenderSettings senderSettings)
        {
            var body = Encoding.UTF8.GetBytes(senderSettings.Message);

            senderSettings.Channel.BasicPublish(exchange: "",
                                 routingKey: senderSettings.QueueName,
                                 basicProperties: senderSettings.Properties,
                                 body: body);

            //SendMessage(senderSettings.Message, 0);
        }

        public void ReleaseRequest(IModel channel, ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        private void getMessage(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var deliveryTag = ea.DeliveryTag;

            ReceiveMessage(message, deliveryTag);
        }
    }
}
