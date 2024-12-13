using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebData.Model;

namespace WebMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7147/api/Product");
            if (response.IsSuccessStatusCode)
            {
                string Data = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(Data);
            }
            return View(products);
        }
    }
}
