namespace Shared.Messaging.RabbitMQ.Messages;

public class AccessoryMessage
{
    public string AccessoryId { get; set; }
    public string? Description { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}