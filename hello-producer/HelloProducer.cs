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
                TransactionalId = AppConfigs.TRANSACTION_ID
            };

            var producer = new ProducerBuilder<int, string>(config).Build();
            producer.InitTransactions(new TimeSpan(TimeSpan.TicksPerSecond));

            _log.Info("Starting First Transaction...");
            producer.BeginTransaction();
            try
            {
                for (int i = 0; i < AppConfigs.NUM_EVENTS; i++)
                {
                    await producer.ProduceAsync(
                        AppConfigs.TOPIC_NAME_1,
                        new Message<int, string>
                        {
                            Key = i,
                            Value = $"Simple message-T1-{i}"
                        });
                    await producer.ProduceAsync(
                        AppConfigs.TOPIC_NAME_2,
                        new Message<int, string>
                        {
                            Key = i,
                            Value = $"Simple message-T1-{i}"
                        });
                }
                _log.Info("Committing First Transaction...");
                producer.CommitTransaction();
            }
            catch (Exception ex)
            {
                _log.Info("Exception in First Transaction. Aborting...");
                producer.AbortTransaction();
                producer.Dispose();
                throw new InvalidOperationException(ex.Message);
            }

            _log.Info("Starting Second Transaction...");
            producer.BeginTransaction();
            try
            {
                for (int i = 0; i < AppConfigs.NUM_EVENTS; i++)
                {
                    await producer.ProduceAsync(
                        AppConfigs.TOPIC_NAME_1,
                        new Message<int, string>
                        {
                            Key = i,
                            Value = $"Simple message-T2-{i}"
                        });
                    await producer.ProduceAsync(
                        AppConfigs.TOPIC_NAME_2,
                        new Message<int, string>
                        {
                            Key = i,
                            Value = $"Simple message-T2-{i}"
                        });
                }
                _log.Info("Committing Second Transaction...");
                producer.AbortTransaction();
            }
            catch (Exception ex)
            {
                _log.Info("Exception in Second Transaction. Aborting...");
                producer.AbortTransaction();
                producer.Dispose();
                throw new InvalidOperationException(ex.Message);
            }

            _log.Info("Finished Sending Messages. Closing Producer.");
            producer.Dispose();
        }
    }
}
