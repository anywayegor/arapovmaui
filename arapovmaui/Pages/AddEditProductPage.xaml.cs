using arapovmaui.Models;
using arapovmaui.Services;
using arapovnapractice.Models;

namespace arapovmaui.Pages;

public partial class AddEditProductPage : ContentPage

{

    private FileResult? selectedImage;

    private Product? currentProduct;

    private List<Creator> creators = new();

    private List<Supplier> suppliers = new();

    private List<CategoryProduct> categories = new();

    private List<NameProduct> names = new();

    public AddEditProductPage()

    {

        InitializeComponent();
        Title = "Добавление товара";
        _ = LoadData();
        DeleteButton.IsVisible = false;

    }

    public AddEditProductPage(Product product)

    {

        InitializeComponent();
        Title = "Редактирование товара";
        _ = LoadData();

        currentProduct = product;

        NameProductPicker.Title = product.ProductName;

        ArticleEntry.Text = product.Article;

        DescriptionEntry.Text = product.Description;

        PriceEntry.Text = product.Price.ToString();

        DiscountEntry.Text = product.Discount;

        QuantityEntry.Text = product.QuantityInStock;

        MeasureEntry.Text = product.Measure;

        ProductImage.Source = product.Photo;

    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        // Возврат на предыдущую страницу
        await Navigation.PopAsync();
    }

    private async void SelectImage_Clicked(object sender, EventArgs e)

    {

        selectedImage = await FilePicker.PickAsync(new PickOptions

        {

            PickerTitle = "Выберите изображение"

        });

        if (selectedImage != null)

        {

            ProductImage.Source =

                ImageSource.FromFile(selectedImage.FullPath);

        }

    }

    private async void SaveButton_Clicked(object sender, EventArgs e)

    {
        if (!decimal.TryParse(PriceEntry.Text, out var price))
        {
            await DisplayAlert(
                "Ошибка",
                "Введите корректную цену",
                "OK");
            return;
        }

        if (price <= 0)
        {
            await DisplayAlert(
                "Ошибка",
                "Цена должна быть больше нуля",
                "OK");
            return;
        }

        if (!int.TryParse(QuantityEntry.Text, out var quantity))
        {
            await DisplayAlert(
                "Ошибка",
                "Введите корректное количество",
                "OK");
            return;
        }

        if (quantity < 0)
        {
            await DisplayAlert(
                "Ошибка",
                "Количество не может быть отрицательным",
                "OK");
            return;
        }

        if (currentProduct != null)

        {

            var request = new UpdateProductRequest

            {

                Article = ArticleEntry.Text ?? "",

                Measure = MeasureEntry.Text ?? "",
                
                Price = price,

                Discount = DiscountEntry.Text ?? "0",

                QuantityInStock =

                    QuantityEntry.Text ?? "0",

                Description =

                    DescriptionEntry.Text ?? ""

            };

            bool result =

                await ApiService.UpdateProduct(

                    currentProduct.IdProduct,

                    request);

            if (result)

            {

                await DisplayAlert(

                    "Успех",

                    "Товар сохранён",

                    "OK");

                await Navigation.PopAsync();

            }

            else

            {

                await DisplayAlert(

                    "Ошибка",

                    "Не удалось сохранить товар",

                    "OK");

            }

            return;

        }

        string photoName = "";

        if (selectedImage != null)
        {
            photoName =
                await ApiService.UploadImage(selectedImage)
                ?? "";
        }
        var addRequest = new AddProductRequest

            {

                Article = ArticleEntry.Text ?? "",

                IdNameProduct =

                    (NameProductPicker.SelectedItem as NameProduct)?.IdNameProduct ?? 0,

                Measure = MeasureEntry.Text ?? "",

                Price = decimal.TryParse(

                    PriceEntry.Text,

                    out var addPrice)

                        ? addPrice

                        : 0,

                IdSupplier =

                    (SupplierPicker.SelectedItem as Supplier)?.IdSupplier ?? 0,

                IdCreator =

                    (CreatorPicker.SelectedItem as Creator)?.IdCreator ?? 0,

                IdCategoryProduct =

                    (CategoryPicker.SelectedItem as CategoryProduct)?.IdCategoryProduct ?? 0,

                Discount = DiscountEntry.Text ?? "0",

                QuantityInStock = QuantityEntry.Text ?? "0",

                Description = DescriptionEntry.Text ?? "",

                Photo = photoName

        };

            bool addResult = await ApiService.AddProduct(addRequest);

            if (addResult)

            {

                await DisplayAlert(

                    "Успех",

                    "Товар добавлен",

                    "OK");

                await Navigation.PopAsync();

            }

        else

        {

            await DisplayAlert(

                "Ошибка",

                "Не удалось добавить товар",

                "OK");

        }

    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)

    {

        if (currentProduct == null)

            return;

        bool answer = await DisplayAlert(

            "Удаление",

            "Удалить товар?",

            "Да",

            "Нет");

        if (!answer)

            return;

        string error =
            await ApiService.DeleteProduct(
                currentProduct.IdProduct);

        if (string.IsNullOrEmpty(error))
        {
            await DisplayAlert(
                "Успех",
                "Товар удалён",
                "OK");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert(
                "Ошибка",
                error,
                "OK");
        }

    }

    private async Task LoadData()

    {

        creators = await ApiService.GetCreators();

        CreatorPicker.ItemsSource = creators;

        CreatorPicker.ItemDisplayBinding =

            new Binding(nameof(Creator.Creator1));

        suppliers = await ApiService.GetSuppliers();

        SupplierPicker.ItemsSource = suppliers;

        SupplierPicker.ItemDisplayBinding =

            new Binding(nameof(Supplier.Supplier1));

        categories = await ApiService.GetCategories();

        CategoryPicker.ItemsSource = categories;

        CategoryPicker.ItemDisplayBinding =

            new Binding(nameof(CategoryProduct.CategoryProduct1));

        names = await ApiService.GetNameProducts();

        NameProductPicker.ItemsSource = names;

        NameProductPicker.ItemDisplayBinding =

            new Binding(nameof(NameProduct.NameProduct1));

    }

}