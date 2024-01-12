namespace EventBus.Messages.Events
{
    public class EquityOrderEvent : IntegrationBaseEvent
    {
        // Properties
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
