using arapovmaui.Models;

namespace arapovmaui.Services;

public static class TestDataService
{
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product
            {
                IdProduct = 1,
                ProductName = "Ноутбук",
                Category = "Техника",
                Description = "Игровой ноутбук",
                Creator = "ASUS",
                Supplier = "DNS",
                Price = 50000,
                Measure = "шт",
                QuantityInStock = 10,
                Discount = 20,
                Photo = "photo.png.jpg"
            },

            new Product
            {
                IdProduct = 2,
                ProductName = "Мышь",
                Category = "Техника",
                Description = "Беспроводная мышь",
                Creator = "Logitech",
                Supplier = "DNS",
                Price = 1500,
                Measure = "шт",
                QuantityInStock = 0,
                Discount = 0,
                Photo = "photo.png.jpg"
            },

            new Product
            {
                IdProduct = 3,
                ProductName = "Клавиатура",
                Category = "Техника",
                Description = "Механическая",
                Creator = "A4Tech",
                Supplier = "DNS",
                Price = 3500,
                Measure = "шт",
                QuantityInStock = 5,
                Discount = 10,
                Photo = "photo.png.jpg"
            }
        };
    }
}