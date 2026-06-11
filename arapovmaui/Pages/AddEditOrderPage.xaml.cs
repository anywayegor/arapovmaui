using arapovmaui.Models;

using arapovmaui.Services;

namespace arapovmaui.Pages;

public partial class AddEditOrderPage : ContentPage

{

    private List<UserOrder> users = new();

    private List<StatusOrder> statuses = new();

    private List<DeliveryStation> stations = new();

    public AddEditOrderPage()

    {

        InitializeComponent();

        _ = LoadData();

    }

    private async Task LoadData()

    {

        users = await ApiService.GetUsers();

        UserPicker.ItemsSource = users;

        UserPicker.ItemDisplayBinding =

            new Binding(nameof(UserOrder.FullName));

        statuses = await ApiService.GetStatuses();

        StatusPicker.ItemsSource = statuses;

        StatusPicker.ItemDisplayBinding =

            new Binding(nameof(StatusOrder.StatusOrder1));

        stations = await ApiService.GetDeliveryStations();

        DeliveryStationPicker.ItemsSource = stations;

        DeliveryStationPicker.ItemDisplayBinding =

            new Binding(nameof(DeliveryStation.FullAddress));

    }

    private async void SaveButton_Clicked(

      object sender,

      EventArgs e)

    {
        if (UserPicker.SelectedItem == null)

        {

            await DisplayAlert("Ошибка", "Выберите пользователя", "OK");

            return;

        }

        if (StatusPicker.SelectedItem == null)

        {

            await DisplayAlert("Ошибка", "Выберите статус", "OK");

            return;

        }

        if (DeliveryStationPicker.SelectedItem == null)

        {

            await DisplayAlert("Ошибка", "Выберите пункт выдачи", "OK");

            return;

        }

        var request = new AddOrderRequest

        {

            OrderDate = OrderDatePicker.Date,

            DeliveryDate = DeliveryDatePicker.Date,

            IdUser =

                (UserPicker.SelectedItem as UserOrder)?.IdUser ?? 0,

            IdStatusOrder =

                (StatusPicker.SelectedItem as StatusOrder)?.IdStatusOrder ?? 0,

            IdDeliveryStation =

                (DeliveryStationPicker.SelectedItem as DeliveryStation)?.IdDeliveryStation ?? 0,

            CodeReceive = CodeEntry.Text ?? ""

        };



        bool result =

            await ApiService.AddOrder(request);

        if (result)

        {

            await DisplayAlert(

                "Успех",

                "Заказ добавлен",

                "OK");

            await Navigation.PopAsync();

        }

        else

        {

            await DisplayAlert(

                "Ошибка",

                "Не удалось добавить заказ",

                "OK");

        }

    }

}