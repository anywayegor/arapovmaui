using arapovmaui.Models;

using arapovmaui.Services;

namespace arapovmaui.Pages;

public partial class AddEditOrderPage : ContentPage

{

    private List<UserOrder> users = new();

    private List<StatusOrder> statuses = new();

    private List<DeliveryStation> stations = new();

    private OrderItem? currentOrder;



    public AddEditOrderPage()

    {

        InitializeComponent();
        Title = "Добавление заказа";

        _ = LoadData();

        DeleteButton.IsVisible = false;

    }

    public AddEditOrderPage(OrderItem order)

    {

        InitializeComponent();

        currentOrder = order;
        Title = "Редактирование заказа";

        OrderDatePicker.Date = order.OrderDate;

        DeliveryDatePicker.Date = order.DeliveryDate;

        CodeEntry.Text = order.CodeReceive;

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

        if (currentOrder != null)

        {

            UserPicker.SelectedItem =

                users.FirstOrDefault(x =>

                    x.IdUser == currentOrder.IdUser);

            StatusPicker.SelectedItem =

                statuses.FirstOrDefault(x =>

                    x.IdStatusOrder == currentOrder.IdStatusOrder);

            DeliveryStationPicker.SelectedItem =

                stations.FirstOrDefault(x =>

                    x.IdDeliveryStation == currentOrder.IdDeliveryStation);

        }

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



        bool result;

        if (currentOrder != null)

        {

            result = await ApiService.UpdateOrder(

                currentOrder.IdOrder,

                request);

        }

        else

        {

            result = await ApiService.AddOrder(request);

        }

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




    private async void DeleteButton_Clicked(

    object sender,

    EventArgs e)

    {

        if (currentOrder == null)

            return;

        bool answer = await DisplayAlert(

            "Удаление",

            "Удалить заказ?",

            "Да",

            "Нет");

        if (!answer)

            return;

        bool result =

            await ApiService.DeleteOrder(

                currentOrder.IdOrder);

        if (result)

        {

            await DisplayAlert(

                "Успех",

                "Заказ удалён",

                "OK");

            await Navigation.PopAsync();

        }

        else

        {

            await DisplayAlert(

                "Ошибка",

                "Не удалось удалить заказ",

                "OK");

        }

    }

    private async void BackButton_Clicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }
}