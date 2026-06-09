namespace arapovmaui.Models;

public class Product

{

    public int IdProduct { get; set; }

    public int IdNameProduct { get; set; }

    public int IdSupplier { get; set; }

    public int IdCreator { get; set; }

    public int IdCategoryProduct { get; set; }

    public string Article { get; set; } = "";

    public string ProductName { get; set; } = "";

    public string Category { get; set; } = "";

    public string Description { get; set; } = "";

    public string Creator { get; set; } = "";

    public string Supplier { get; set; } = "";

    public decimal Price { get; set; }

    public string Measure { get; set; } = "";

    public string QuantityInStock { get; set; } = "";

    public string Discount { get; set; } = "";

    private string _photo = "";

    public string Photo

    {

        get

        {

            if (string.IsNullOrWhiteSpace(_photo)

                || _photo == "null"

                || _photo == "-"

                || _photo == "Нет")

            {

                return "http://localhost:5095/images/no-photo.png";

            }

            return $"http://localhost:5095/images/{_photo}";

        }

        set => _photo = value;

    }

    public bool HasGreenDiscount =>

        int.TryParse(Discount, out var d) && d >= 15;

    public bool IsOutOfStock =>

        int.TryParse(QuantityInStock, out var q) && q <= 0;

    public decimal FinalPrice

    {

        get

        {

            int.TryParse(Discount, out var d);

            return Price - (Price * d / 100m);

        }

    }

    public string StockText =>

        $"В наличии: {QuantityInStock}";

    public string DiscountText =>

        $"{Discount}%";

}