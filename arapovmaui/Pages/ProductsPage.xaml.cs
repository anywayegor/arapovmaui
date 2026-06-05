using arapovmaui.Helpers;
using arapovmaui.Models;
using arapovmaui.Services;

namespace arapovmaui.Pages;

public partial class ProductsPage : ContentPage
{
    private List<Product> _products;

    public ProductsPage()
    {
        InitializeComponent();

        _products = TestDataService.GetProducts();

        ProductsCollection.ItemsSource = _products;

        UserLabel.Text =
            AppData.CurrentUser?.FullName ?? "Гость";

        CountLabel.Text = $"Товаров: {_products.Count}";
    }

    private void SearchProduct_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = e.NewTextValue?.ToLower() ?? "";

        var filtered = _products
            .Where(x => x.ProductName.ToLower().Contains(text))
            .ToList();

        ProductsCollection.ItemsSource = filtered;

        CountLabel.Text = $"Товаров: {filtered.Count}";
    }

    private void SortPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (SortPicker.SelectedIndex)
        {
            case 0:
                ProductsCollection.ItemsSource = _products;
                break;

            case 1:
                ProductsCollection.ItemsSource =
                    _products.OrderBy(x => x.Price).ToList();
                break;

            case 2:
                ProductsCollection.ItemsSource =
                    _products.OrderByDescending(x => x.Price).ToList();
                break;
        }
    }

    private async void ExitButton_Clicked(object sender, EventArgs e)
    {
        AppData.CurrentUser = null;

        await Navigation.PopToRootAsync();
    }
}