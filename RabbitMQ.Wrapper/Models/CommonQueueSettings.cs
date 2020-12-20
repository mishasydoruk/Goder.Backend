using RabbitMQ.Client;

namespace RabbitMQ.Wrapper.Models
{
    public class CommonQueueSettings
    {
        public IModel Channel { get; set; }
        public string QueueName { get; set; }
    }
}
