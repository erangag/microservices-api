namespace EventBus.Messages.Events
{
    public class DeskOrderEvent : IntegrationBaseEvent
    {
        // Properties
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
