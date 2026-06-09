using arapovmaui.Helpers;

using arapovmaui.Services;

namespace arapovmaui.Pages;

public partial class LoginPage : ContentPage

{

    public LoginPage()

    {

        InitializeComponent();

    }

    private async void LoginButton_Clicked(object sender, EventArgs e)

    {

        var user = await ApiService.Login(

            LoginEntry.Text ?? "",

            PasswordEntry.Text ?? "");

        if (user == null)

        {

            await DisplayAlert(

                "Ошибка",

                "Неверный логин или пароль",

                "OK");

            return;

        }

        AppData.CurrentUser = user;

        await Navigation.PushAsync(new ProductsPage());

    }

    private async void GuestButton_Clicked(object sender, EventArgs e)

    {

        AppData.CurrentUser = null;

        await Navigation.PushAsync(new ProductsPage());

    }

}