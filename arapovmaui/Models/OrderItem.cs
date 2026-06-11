namespace arapovmaui.Models;

public class OrderItem

{

    public int IdOrder { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int IdUser { get; set; }

    public int IdStatusOrder { get; set; }

    public int IdDeliveryStation { get; set; }

    public string CodeReceive { get; set; } = "";

    public string Status { get; set; } = "";

    public string DeliveryStation { get; set; } = "";

}