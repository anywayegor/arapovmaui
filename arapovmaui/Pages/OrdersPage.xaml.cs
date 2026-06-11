using arapovmaui.Models;

using arapovmaui.Services;

namespace arapovmaui.Pages;

public partial class OrdersPage : ContentPage

{

    private List<OrderItem> orders = new();

    public OrdersPage()

    {

        InitializeComponent();

    }

    protected override async void OnAppearing()

    {

        base.OnAppearing();

        orders = await ApiService.GetOrders();

        OrdersCollection.ItemsSource = orders;

    }

    private async void AddOrderButton_Clicked(

    object sender,

    EventArgs e)

    {

        await Navigation.PushAsync(

            new AddEditOrderPage());

    }

    private async void OrdersCollection_SelectionChanged(

    object sender,

    SelectionChangedEventArgs e)

    {

        if (e.CurrentSelection.Count == 0)

            return;

        var order =

            e.CurrentSelection.FirstOrDefault() as OrderItem;

        if (order == null)

            return;

        await DisplayAlert(

            "Заказ",

            $"Заказ №{order.IdOrder}",

            "OK");

        OrdersCollection.SelectedItem = null;

    }
}