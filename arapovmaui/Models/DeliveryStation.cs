namespace arapovmaui.Models;

public class DeliveryStation

{

    public int IdDeliveryStation { get; set; }

    public int Index { get; set; }

    public string City { get; set; } = "";

    public string Street { get; set; } = "";

    public int NumberHouse { get; set; }

    public string FullAddress =>

        $"{City}, {Street}, {NumberHouse}";

}