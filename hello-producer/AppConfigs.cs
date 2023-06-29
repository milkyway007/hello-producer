namespace hello_producer
{
    internal class AppConfigs
    {
        public const string APPLICATION_ID = "HelloProducer";
        public const string BOOTSTRAP_SERVERS = "localhost: 9092,localhost: 9093";
        public const string TOPIC_NAME = "hello-producer-topic";
        public const int NUM_EVENTS = 1000000;
    }
}
