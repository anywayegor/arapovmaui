using arapovmaui.Helpers;

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

        AddOrderButton.IsVisible =

            AppData.CurrentUser?.Role == "Администратор";


    }

    private async void AddOrderButton_Clicked(

        object sender,

        EventArgs e)

    {

        await Navigation.PushAsync(

            new AddEditOrderPage());

    }

    private async void OrderTapped(

        object sender,

        TappedEventArgs e)

    {

        if (sender is Frame frame &&

            frame.BindingContext is OrderItem order)

        {

            await Navigation.PushAsync(

                new AddEditOrderPage(order));

        }

    }

    private async void BackButton_Clicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }

}