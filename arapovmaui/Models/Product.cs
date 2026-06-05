namespace arapovmaui.Models;

public class Product
{
    public int IdProduct { get; set; }

    public string ProductName { get; set; } = "";

    public string Category { get; set; } = "";

    public string Description { get; set; } = "";

    public string Creator { get; set; } = "";

    public string Supplier { get; set; } = "";

    public decimal Price { get; set; }

    public string Measure { get; set; } = "";

    public int QuantityInStock { get; set; }

    public int Discount { get; set; }

    public string Photo { get; set; } = "photo.png.jpg";

    public bool HasGreenDiscount => Discount >= 15;

    public bool IsOutOfStock => QuantityInStock <= 0;

    public decimal FinalPrice =>
        Price - (Price * Discount / 100m);

    public string StockText =>
        QuantityInStock > 0
            ? $"В наличии: {QuantityInStock}"
            : "Нет в наличии";

    public string DiscountText =>
        $"{Discount}%";
}