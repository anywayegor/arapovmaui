namespace arapovnapractice.Models;

public class UpdateProductRequest

{

    public string Article { get; set; } = "";

    public string Measure { get; set; } = "";

    public decimal Price { get; set; }

    public string Discount { get; set; } = "";

    public string QuantityInStock { get; set; } = "";

    public string Description { get; set; } = "";

}