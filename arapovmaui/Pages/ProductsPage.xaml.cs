using arapovmaui.Helpers;

using arapovmaui.Models;

using arapovmaui.Services;

namespace arapovmaui.Pages;

public partial class ProductsPage : ContentPage

{

    private List<Product> _products = new();

    private List<Supplier> _suppliers = new();

    public ProductsPage()

    {

        InitializeComponent();

    }

    protected override async void OnAppearing()

    {

        base.OnAppearing();

        SearchProduct.IsVisible =
            AppData.CurrentUser != null;

        UserLabel.Text =

            AppData.CurrentUser?.FullName ?? "Гость";

        if (AppData.CurrentUser == null)

            if (AppData.CurrentUser == null)

            {

                SortPicker.IsVisible = false;
                SupplierPicker.IsVisible = false;
                AddButton.IsVisible = false;
                OrdersButton.IsVisible = false;

            }

            else

            {

                SortPicker.IsVisible = true;

                AddButton.IsVisible =

                    AppData.CurrentUser.Role == "Администратор";
                OrdersButton.IsVisible =
                    AppData.CurrentUser?.Role == "Администратор";
            }

        _products = await ApiService.GetProducts();

        ProductsCollection.ItemsSource = _products;

        CountLabel.Text = $"Товаров: {_products.Count}";

        _suppliers = await ApiService.GetSuppliers();

        _suppliers.Insert(0, new Supplier

        {

            IdSupplier = null,

            Supplier1 = "Все поставщики"

        });

        SupplierPicker.ItemsSource = _suppliers;

        SupplierPicker.SelectedIndex = 0;

    }

    private async void AddButton_Clicked(object sender, EventArgs e)

    {

        await Navigation.PushAsync(new AddEditProductPage());

    }

    private void SearchProduct_TextChanged(object sender, TextChangedEventArgs e)

    {

        ApplyFilters();

    }

    private void SupplierPicker_SelectedIndexChanged(object sender, EventArgs e)

    {

        ApplyFilters();

    }

    private void ApplyFilters()
    {
        IEnumerable<Product> filtered = _products;

        string text = SearchProduct.Text?.ToLower() ?? "";

        filtered = filtered.Where(x =>
            x.ProductName.ToLower().Contains(text) ||
            x.Article.ToLower().Contains(text) ||
            x.Category.ToLower().Contains(text) ||
            x.Creator.ToLower().Contains(text) ||
            x.Supplier.ToLower().Contains(text) ||
            x.Description.ToLower().Contains(text) ||
            x.Measure.ToLower().Contains(text) ||
            x.Price.ToString().Contains(text) ||
            x.Discount.Contains(text) ||
            x.QuantityInStock.Contains(text));

        if (SupplierPicker.SelectedItem is Supplier supplier &&
            supplier.IdSupplier != null)
        {
            filtered = filtered.Where(x =>
                x.IdSupplier == supplier.IdSupplier);
        }

        switch (SortPicker.SelectedIndex)
        {
            case 1:

                filtered = filtered.OrderBy(x =>

                    int.TryParse(x.QuantityInStock, out var q)

                        ? q

                        : 0);

                break;

            case 2:

                filtered = filtered.OrderByDescending(x =>

                    int.TryParse(x.QuantityInStock, out var q)

                        ? q

                        : 0);

                break;
        }

        var result = filtered.ToList();

        ProductsCollection.ItemsSource = result;

        CountLabel.Text = $"Товаров: {result.Count}";
    }

    private void SortPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplyFilters();
    }

    private async void ExitButton_Clicked(object sender, EventArgs e)

    {

        AppData.CurrentUser = null;

        await Navigation.PopToRootAsync();

    }

    private async void ProductsCollection_SelectionChanged(

    object sender,

    SelectionChangedEventArgs e)

    {

        if (e.CurrentSelection.Count == 0)

            return;

        var product =

            e.CurrentSelection.FirstOrDefault() as Product;

        if (product == null)

            return;
        if (AppData.CurrentUser == null ||

    AppData.CurrentUser.Role != "Администратор")

        {

            await DisplayAlert(

                "Ошибка",

                "Редактирование доступно только администратору",

                "OK");

            return;

        }

        await Navigation.PushAsync(

            new AddEditProductPage(product));

        ProductsCollection.SelectedItem = null;

    }

    private async void OrdersButton_Clicked(object sender, EventArgs e)

    {

        await Navigation.PushAsync(

            new OrdersPage());

    }

}