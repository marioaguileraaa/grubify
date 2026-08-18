using Microsoft.AspNetCore.Mvc;
using GrubifyApi.Models;

namespace GrubifyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemsController : ControllerBase
    {
        private static readonly List<FoodItem> FoodItems = new()
        {
            // Moda Hombre items
            new FoodItem
            {
                Id = 1,
                Name = "Camisa Oxford",
                Description = "Camisa de algodón premium con cuello button-down, corte slim fit",
                Price = 49.99m,
                ImageUrl = "https://images.unsplash.com/photo-1596755094514-f87e34085b2c?w=400&h=300&fit=crop",
                Category = "Camisas",
                IsVegetarian = true,
                IsVegan = false,
                IsSpicy = false,
                RestaurantId = 1,
                PreparationTime = 20
            },
            new FoodItem
            {
                Id = 2,
                Name = "Pantalón Chino",
                Description = "Pantalón de algodón elástico con corte recto, disponible en varios colores",
                Price = 59.99m,
                ImageUrl = "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=400&h=300&fit=crop",
                Category = "Pantalones",
                IsVegetarian = false,
                IsVegan = false,
                IsSpicy = false,
                RestaurantId = 1,
                PreparationTime = 25
            },
            new FoodItem
            {
                Id = 3,
                Name = "Chaqueta Blazer",
                Description = "Blazer de lana mezclada con forro interior, ideal para ocasiones formales",
                Price = 129.99m,
                ImageUrl = "https://images.unsplash.com/photo-1507679799987-c73779587ccf?w=400&h=300&fit=crop",
                Category = "Chaquetas",
                IsVegetarian = true,
                IsVegan = false,
                IsSpicy = true,
                RestaurantId = 1,
                PreparationTime = 10
            },

            // Moda Mujer items
            new FoodItem
            {
                Id = 4,
                Name = "Vestido Midi",
                Description = "Vestido elegante de corte midi con estampado floral y cintura ajustada",
                Price = 79.99m,
                ImageUrl = "https://images.unsplash.com/photo-1595777457583-95e059d581b8?w=400&h=300&fit=crop",
                Category = "Vestidos",
                IsVegetarian = true,
                IsVegan = false,
                IsSpicy = false,
                RestaurantId = 2,
                PreparationTime = 15
            },
            new FoodItem
            {
                Id = 5,
                Name = "Blusa de Seda",
                Description = "Blusa de seda natural con detalle de lazo en el cuello",
                Price = 69.99m,
                ImageUrl = "https://images.unsplash.com/photo-1564257631407-4deb1f99d992?w=400&h=300&fit=crop",
                Category = "Blusas",
                IsVegetarian = false,
                IsVegan = false,
                IsSpicy = true,
                RestaurantId = 2,
                PreparationTime = 15
            },
            new FoodItem
            {
                Id = 6,
                Name = "Falda Plisada",
                Description = "Falda plisada de tela fluida, largo hasta la rodilla",
                Price = 54.99m,
                ImageUrl = "https://images.unsplash.com/photo-1583496661160-fb5886a0aaaa?w=400&h=300&fit=crop",
                Category = "Faldas",
                IsVegetarian = true,
                IsVegan = true,
                IsSpicy = false,
                RestaurantId = 2,
                PreparationTime = 20
            },

            // Moda Infantil items
            new FoodItem
            {
                Id = 7,
                Name = "Camiseta Estampada",
                Description = "Camiseta de algodón orgánico con diseños divertidos para niños",
                Price = 19.99m,
                ImageUrl = "https://images.unsplash.com/photo-1519238263530-99bdd11df2ea?w=400&h=300&fit=crop",
                Category = "Camisetas",
                IsVegetarian = true,
                IsVegan = false,
                IsSpicy = false,
                RestaurantId = 3,
                PreparationTime = 30
            },
            new FoodItem
            {
                Id = 8,
                Name = "Pantalón Vaquero",
                Description = "Vaqueros resistentes con cintura elástica y rodillas reforzadas",
                Price = 29.99m,
                ImageUrl = "https://images.unsplash.com/photo-1473966968600-fa801b869a1a?w=400&h=300&fit=crop",
                Category = "Pantalones",
                IsVegetarian = true,
                IsVegan = true,
                IsSpicy = false,
                RestaurantId = 3,
                PreparationTime = 25
            },
            new FoodItem
            {
                Id = 9,
                Name = "Sudadera con Capucha",
                Description = "Sudadera de felpa suave con capucha y bolsillo canguro",
                Price = 34.99m,
                ImageUrl = "https://images.unsplash.com/photo-1556821840-3a63f95609a7?w=400&h=300&fit=crop",
                Category = "Sudaderas",
                IsVegetarian = false,
                IsVegan = false,
                IsSpicy = false,
                RestaurantId = 3,
                PreparationTime = 10
            },

            // Accesorios items
            new FoodItem
            {
                Id = 10,
                Name = "Bolso de Cuero",
                Description = "Bolso de piel genuina con compartimentos interiores y cierre magnético",
                Price = 89.99m,
                ImageUrl = "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?w=400&h=300&fit=crop",
                Category = "Bolsos",
                IsVegetarian = false,
                IsVegan = false,
                IsSpicy = true,
                RestaurantId = 4,
                PreparationTime = 15
            },
            new FoodItem
            {
                Id = 11,
                Name = "Cinturón Premium",
                Description = "Cinturón de cuero italiano con hebilla en acabado plata",
                Price = 45.99m,
                ImageUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=400&h=300&fit=crop",
                Category = "Cinturones",
                IsVegetarian = false,
                IsVegan = false,
                IsSpicy = true,
                RestaurantId = 4,
                PreparationTime = 18
            },
            new FoodItem
            {
                Id = 12,
                Name = "Gafas de Sol",
                Description = "Gafas de sol con montura de acetato y lentes polarizadas UV400",
                Price = 65.99m,
                ImageUrl = "https://images.unsplash.com/photo-1572635196237-14b3f281503f?w=400&h=300&fit=crop",
                Category = "Gafas",
                IsVegetarian = true,
                IsVegan = false,
                IsSpicy = false,
                RestaurantId = 4,
                PreparationTime = 12
            },

            // Calzado items
            new FoodItem
            {
                Id = 13,
                Name = "Zapatos Oxford",
                Description = "Zapatos clásicos Oxford de piel pulida con suela de cuero",
                Price = 119.99m,
                ImageUrl = "https://images.unsplash.com/photo-1614252369475-531eba835eb1?w=400&h=300&fit=crop",
                Category = "Formal",
                IsVegetarian = false,
                IsVegan = false,
                IsSpicy = true,
                RestaurantId = 5,
                PreparationTime = 15
            },
            new FoodItem
            {
                Id = 14,
                Name = "Zapatillas Deportivas",
                Description = "Zapatillas ligeras con suela de amortiguación y tejido transpirable",
                Price = 89.99m,
                ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400&h=300&fit=crop",
                Category = "Deportivo",
                IsVegetarian = true,
                IsVegan = true,
                IsSpicy = false,
                RestaurantId = 5,
                PreparationTime = 5
            },
            new FoodItem
            {
                Id = 15,
                Name = "Botas Chelsea",
                Description = "Botas Chelsea de ante con elástico lateral y suela de goma",
                Price = 99.99m,
                ImageUrl = "https://images.unsplash.com/photo-1638247025967-b4e38f787b76?w=400&h=300&fit=crop",
                Category = "Casual",
                IsVegetarian = true,
                IsVegan = false,
                IsSpicy = false,
                RestaurantId = 5,
                PreparationTime = 20
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<FoodItem>> GetFoodItems()
        {
            return Ok(FoodItems);
        }

        [HttpGet("{id}")]
        public ActionResult<FoodItem> GetFoodItem(int id)
        {
            var foodItem = FoodItems.FirstOrDefault(f => f.Id == id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return Ok(foodItem);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public ActionResult<IEnumerable<FoodItem>> GetFoodItemsByRestaurant(int restaurantId)
        {
            var items = FoodItems.Where(f => f.RestaurantId == restaurantId).ToList();
            return Ok(items);
        }

        [HttpGet("category/{category}")]
        public ActionResult<IEnumerable<FoodItem>> GetFoodItemsByCategory(string category)
        {
            var items = FoodItems.Where(f => 
                f.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(items);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<FoodItem>> SearchFoodItems([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Ok(FoodItems);
            }

            var items = FoodItems.Where(f => 
                f.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                f.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                f.Category.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            
            return Ok(items);
        }

        [HttpGet("dietary")]
        public ActionResult<IEnumerable<FoodItem>> GetFoodItemsByDietaryPreference(
            [FromQuery] bool? isVegetarian = null,
            [FromQuery] bool? isVegan = null,
            [FromQuery] bool? isSpicy = null)
        {
            var items = FoodItems.AsQueryable();

            if (isVegetarian.HasValue)
                items = items.Where(f => f.IsVegetarian == isVegetarian.Value);

            if (isVegan.HasValue)
                items = items.Where(f => f.IsVegan == isVegan.Value);

            if (isSpicy.HasValue)
                items = items.Where(f => f.IsSpicy == isSpicy.Value);

            return Ok(items.ToList());
        }
    }
}
