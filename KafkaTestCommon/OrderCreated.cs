namespace KafkaTestCommon
{
    public class OrderCreated
    {
        public Guid Id { get; set; }
        public string Customer { get; set; } = "";
        public decimal Total { get; set; }
    }
}
