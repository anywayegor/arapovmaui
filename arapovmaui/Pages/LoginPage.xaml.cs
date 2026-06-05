using arapovmaui.Helpers;
using arapovmaui.Models;

namespace arapovmaui.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        AppData.CurrentUser = new User
        {
            IdUser = 1,
            FirstName = "Иван",
            LastName = "Иванов",
            Patronumic = "Иванович",
            Role = "Менеджер"
        };

        await Navigation.PushAsync(new ProductsPage());
    }

    private async void GuestButton_Clicked(object sender, EventArgs e)
    {
        AppData.CurrentUser = null;

        await Navigation.PushAsync(new ProductsPage());
    }
}