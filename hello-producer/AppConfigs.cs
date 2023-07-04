namespace hello_producer
{
    internal class AppConfigs
    {
        public const string APPLICATION_ID = "HelloProducer";
        public const string BOOTSTRAP_SERVERS = "localhost: 9092,localhost: 9093";
        public const string TOPIC_NAME_1 = "hello-producer-1";
        public const string TOPIC_NAME_2 = "hello-producer-2";
        public const int NUM_EVENTS = 2;
        public const string TRANSACTION_ID = "hello-producer-trans";
    }
}
