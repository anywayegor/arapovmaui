namespace arapovmaui.Models;

public class AddOrderRequest

{

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int IdDeliveryStation { get; set; }

    public int IdUser { get; set; }

    public string CodeReceive { get; set; } = "";

    public int IdStatusOrder { get; set; }

}