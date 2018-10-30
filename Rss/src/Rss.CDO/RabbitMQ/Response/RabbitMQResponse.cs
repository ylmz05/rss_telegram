using Newtonsoft.Json;
using Rss.CDO.RabbitMQ.Model;

namespace Rss.CDO.RabbitMQ.Response
{
    public class RabbitMQResponse
    {
        public Payload Payload { get; set; }

        private RabbitMQResponse(string payload)
        {
            Payload = JsonConvert.DeserializeObject<Payload>(payload);
        }

        public static RabbitMQResponse Create(string payload)
        {
            return new RabbitMQResponse(payload);
        }
    }
}
