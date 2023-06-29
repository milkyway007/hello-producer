using Confluent.Kafka;
using log4net;

namespace hello_producer
{
    internal class HelloProducer
    {
        private static readonly ILog _log = LogManager.GetLogger("default");

        public static async Task ProduceAsync()
        {
            _log.Info("Creating Kafka Producer");

            var config = new ProducerConfig {
                BootstrapServers = AppConfigs.BOOTSTRAP_SERVERS,
                ClientId = AppConfigs.APPLICATION_ID,
            };

            var producer = new ProducerBuilder<int, string>(config).Build();
            _log.Info("Start Sending Messages");
            for (int i = 0; i < AppConfigs.NUM_EVENTS; i++)
            {
                await producer.ProduceAsync(
                    AppConfigs.TOPIC_NAME,
                    new Message<int, string> {
                        Key = i,
                        Value = $"Simple message-{i}"
                    });
            }

            _log.Info("Finished Sending Messages. Closing Producer.");
            producer.Dispose();
        }
    }
}
