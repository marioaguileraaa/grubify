using Microsoft.AspNetCore.Mvc;
using GrubifyApi.Models;

namespace GrubifyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private static readonly List<Restaurant> Restaurants = new()
        {
            new Restaurant
            {
                Id = 1,
                Name = "Moda Hombre",
                Description = "Colección exclusiva de moda masculina con las últimas tendencias",
                ImageUrl = "https://images.unsplash.com/photo-1490578474895-699cd4e2cf59?w=800&h=600&fit=crop",
                CuisineType = "Hombre",
                Rating = 4.8,
                DeliveryTime = "2-4 días",
                DeliveryFee = 3.99m,
                MinimumOrder = 25.00m,
                IsOpen = true,
                Address = "Planta 2, Gran Vía 32, Madrid"
            },
            new Restaurant
            {
                Id = 2,
                Name = "Moda Mujer",
                Description = "Diseños elegantes y contemporáneos para la mujer moderna",
                ImageUrl = "https://images.unsplash.com/photo-1469334031218-e382a71b716b?w=800&h=600&fit=crop",
                CuisineType = "Mujer",
                Rating = 4.9,
                DeliveryTime = "2-4 días",
                DeliveryFee = 3.99m,
                MinimumOrder = 25.00m,
                IsOpen = true,
                Address = "Planta 3, Gran Vía 32, Madrid"
            },
            new Restaurant
            {
                Id = 3,
                Name = "Moda Infantil",
                Description = "Ropa cómoda y divertida para los más pequeños de la casa",
                ImageUrl = "https://images.unsplash.com/photo-1519238263530-99bdd11df2ea?w=800&h=600&fit=crop",
                CuisineType = "Niños",
                Rating = 4.7,
                DeliveryTime = "2-4 días",
                DeliveryFee = 2.99m,
                MinimumOrder = 15.00m,
                IsOpen = true,
                Address = "Planta 4, Gran Vía 32, Madrid"
            },
            new Restaurant
            {
                Id = 4,
                Name = "Accesorios",
                Description = "Complementos premium: bolsos, cinturones, gafas y más",
                ImageUrl = "https://images.unsplash.com/photo-1523170335258-f5ed11844a49?w=800&h=600&fit=crop",
                CuisineType = "Accesorios",
                Rating = 4.6,
                DeliveryTime = "1-3 días",
                DeliveryFee = 2.49m,
                MinimumOrder = 20.00m,
                IsOpen = true,
                Address = "Planta 1, Gran Vía 32, Madrid"
            },
            new Restaurant
            {
                Id = 5,
                Name = "Calzado",
                Description = "Zapatos de calidad para cada ocasión: formal, casual y deportivo",
                ImageUrl = "https://images.unsplash.com/photo-1549298916-b41d501d3772?w=800&h=600&fit=crop",
                CuisineType = "Calzado",
                Rating = 4.5,
                DeliveryTime = "2-5 días",
                DeliveryFee = 4.99m,
                MinimumOrder = 30.00m,
                IsOpen = true,
                Address = "Planta 1, Gran Vía 32, Madrid"
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurants()
        {
            return Ok(Restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurant(int id)
        {
            var restaurant = Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpGet("cuisine/{cuisineType}")]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurantsByCuisine(string cuisineType)
        {
            var restaurants = Restaurants.Where(r => 
                r.CuisineType.Equals(cuisineType, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(restaurants);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Restaurant>> SearchRestaurants([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Ok(Restaurants);
            }

            var restaurants = Restaurants.Where(r => 
                r.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                r.CuisineType.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                r.Description.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            
            return Ok(restaurants);
        }
    }
}
