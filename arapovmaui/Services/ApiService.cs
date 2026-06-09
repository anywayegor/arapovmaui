using arapovmaui.Models;
using arapovnapractice.Models;
using System.Net.Http.Json;

namespace arapovmaui.Services;

public static class ApiService

{

    private static readonly HttpClient client = new()

    {

        BaseAddress = new Uri("http://localhost:5095/")

    };

    public static async Task<List<Product>> GetProducts()

    {

        try

        {

            var products =

                await client.GetFromJsonAsync<List<Product>>("api/Products");

            return products ?? new List<Product>();

        }

        catch

        {

            return new List<Product>();

        }

    }

    public static async Task<User?> Login(string login, string password)

    {

        try

        {

            var request = new LoginRequest

            {

                Login = login,

                Password = password

            };

            var response = await client.PostAsJsonAsync(

                "api/Users/login",

                request);

            if (!response.IsSuccessStatusCode)

                return null;

            var user =

                await response.Content.ReadFromJsonAsync<User>();

            return user;

        }

        catch

        {

            return null;

        }

    }

    public static async Task<List<Supplier>> GetSuppliers()

    {

        try

        {

            var suppliers =

                await client.GetFromJsonAsync<List<Supplier>>("api/Suppli" +
                "ers");

            return suppliers ?? new List<Supplier>();

        }

        catch

        {

            return new List<Supplier>();

        }

    }

    public static async Task<bool> UpdateProduct(

        int id,

        UpdateProductRequest request)

    {

        try

        {

            var response =

                await client.PutAsJsonAsync(

                    $"api/Products/{id}",

                    request);

            return response.IsSuccessStatusCode;

        }

        catch

        {

            return false;

        }

    
    }

    public static async Task<bool> DeleteProduct(int id)

    {

        try

        {

            var response =

                await client.DeleteAsync($"api/Products/{id}");

            return response.IsSuccessStatusCode;

        }

        catch

        {

            return false;

        }

    }

    public static async Task<bool> AddProduct(

     AddProductRequest request)

    {

        try

        {

            var response =

                await client.PostAsJsonAsync(

                    "api/Products",

                    request);

            return response.IsSuccessStatusCode;

        }

        catch

        {

            return false;

        }

    }

    public static async Task<List<Creator>> GetCreators()

    {

        try

        {

            var creators =

                await client.GetFromJsonAsync<List<Creator>>(

                    "api/Creators");

            return creators ?? new List<Creator>();

        }

        catch

        {

            return new List<Creator>();

        }

    }
    public static async Task<List<CategoryProduct>> GetCategories()

    {

        try

        {

            var categories =

                await client.GetFromJsonAsync<List<CategoryProduct>>(

                    "api/CategoryProducts");

            return categories ?? new List<CategoryProduct>();

        }

        catch

        {

            return new List<CategoryProduct>();

        }

    }

    public static async Task<List<NameProduct>> GetNameProducts()

    {

        try

        {

            var names =

                await client.GetFromJsonAsync<List<NameProduct>>(

                    "api/NameProducts");

            return names ?? new List<NameProduct>();

        }

        catch

        {

            return new List<NameProduct>();

        }

    }

    public static async Task<List<Product>> GetProducts(bool isAscending = true)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<Product>>($"products?isAscending={isAscending}");
            return response ?? new List<Product>();
        }
        catch
        {
            return new List<Product>();
        }
    }

    private static readonly HttpClient _httpClient = new HttpClient();
}