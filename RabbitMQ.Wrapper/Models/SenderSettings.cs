using RabbitMQ.Client;

namespace RabbitMQ.Wrapper.Models
{
    public class SenderSettings
    {
        public IModel Channel { get; set; }
        public IBasicProperties Properties { get; set; }
        public string QueueName { get; set; }
        public string Message { get; set; }
    }
}
