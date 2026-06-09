namespace arapovmaui.Models;

public class AddProductRequest

{

    public string Article { get; set; } = "";

    public int IdNameProduct { get; set; }

    public string Measure { get; set; } = "";

    public decimal Price { get; set; }

    public int IdSupplier { get; set; }

    public int IdCreator { get; set; }

    public int IdCategoryProduct { get; set; }

    public string Discount { get; set; } = "";

    public string QuantityInStock { get; set; } = "";

    public string Description { get; set; } = "";

    public string Photo { get; set; } = "";

}