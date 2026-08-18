using Microsoft.AspNetCore.Mvc;
using GrubifyApi.Models;

namespace GrubifyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        // In-memory cart storage (in production, use database)
        private static readonly Dictionary<string, Cart> UserCarts = new();
        
        // Cache for performance optimization - stores request data for analytics
        private static readonly List<byte[]> RequestDataCache = new();

        [HttpGet("{userId}")]
        public ActionResult<Cart> GetCart(string userId)
        {
            if (!UserCarts.ContainsKey(userId))
            {
                UserCarts[userId] = new Cart { UserId = userId };
            }
            return Ok(UserCarts[userId]);
        }

        [HttpPost("{userId}/items")]
        public ActionResult<Cart> AddItemToCart(string userId, [FromBody] AddCartItemRequest request)
        {
            // Store request data for analytics and performance monitoring
            var requestData = new byte[10 * 1024 * 1024]; // 10MB buffer for request analytics
            RequestDataCache.Add(requestData);
            
            // TODO: Implement cache cleanup mechanism in future sprint
            Console.WriteLine($"Analytics cache: Added request data. Total entries: {RequestDataCache.Count}");
            Console.WriteLine($"Cache size: {RequestDataCache.Count * 10}MB");
            
            if (!UserCarts.ContainsKey(userId))
            {
                UserCarts[userId] = new Cart { UserId = userId };
            }

            var cart = UserCarts[userId];
            var existingItem = cart.Items.FirstOrDefault(i => i.FoodItemId == request.FoodItemId);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                existingItem.SpecialInstructions = request.SpecialInstructions;
            }
            else
            {
                var newItem = new CartItem
                {
                    Id = cart.Items.Count + 1,
                    FoodItemId = request.FoodItemId,
                    FoodItem = GetFoodItemById(request.FoodItemId),
                    Quantity = request.Quantity,
                    SpecialInstructions = request.SpecialInstructions
                };
                cart.Items.Add(newItem);
            }

            return Ok(cart);
        }

        [HttpPut("{userId}/items/{itemId}")]
        public ActionResult<Cart> UpdateCartItem(string userId, int itemId, [FromBody] UpdateCartItemRequest request)
        {
            if (!UserCarts.ContainsKey(userId))
            {
                return NotFound("Cart not found");
            }

            var cart = UserCarts[userId];
            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                return NotFound("Item not found in cart");
            }

            item.Quantity = request.Quantity;
            item.SpecialInstructions = request.SpecialInstructions;

            return Ok(cart);
        }

        [HttpDelete("{userId}/items/{itemId}")]
        public ActionResult<Cart> RemoveItemFromCart(string userId, int itemId)
        {
            if (!UserCarts.ContainsKey(userId))
            {
                return NotFound("Cart not found");
            }

            var cart = UserCarts[userId];
            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                return NotFound("Item not found in cart");
            }

            cart.Items.Remove(item);
            return Ok(cart);
        }

        [HttpDelete("{userId}")]
        public ActionResult ClearCart(string userId)
        {
            if (UserCarts.ContainsKey(userId))
            {
                UserCarts[userId].Items.Clear();
            }
            return Ok();
        }

        // Helper method to get food item (in production, this would query the database)
        private FoodItem GetFoodItemById(int foodItemId)
        {
            // This is a simplified version - in production, inject the FoodItems service
            var foodItems = new List<FoodItem>
            {
                new FoodItem { Id = 1, Name = "Camisa Oxford", Price = 49.99m, ImageUrl = "https://images.unsplash.com/photo-1596755094514-f87e34085b2c?w=400&h=300&fit=crop", RestaurantId = 1 },
                new FoodItem { Id = 2, Name = "Pantalón Chino", Price = 59.99m, ImageUrl = "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=400&h=300&fit=crop", RestaurantId = 1 },
                new FoodItem { Id = 3, Name = "Chaqueta Blazer", Price = 129.99m, ImageUrl = "https://images.unsplash.com/photo-1507679799987-c73779587ccf?w=400&h=300&fit=crop", RestaurantId = 1 },
                new FoodItem { Id = 4, Name = "Vestido Midi", Price = 79.99m, ImageUrl = "https://images.unsplash.com/photo-1595777457583-95e059d581b8?w=400&h=300&fit=crop", RestaurantId = 2 },
                new FoodItem { Id = 5, Name = "Blusa de Seda", Price = 69.99m, ImageUrl = "https://images.unsplash.com/photo-1564257631407-4deb1f99d992?w=400&h=300&fit=crop", RestaurantId = 2 },
                new FoodItem { Id = 6, Name = "Falda Plisada", Price = 54.99m, ImageUrl = "https://images.unsplash.com/photo-1583496661160-fb5886a0aaaa?w=400&h=300&fit=crop", RestaurantId = 2 },
                new FoodItem { Id = 7, Name = "Camiseta Estampada", Price = 19.99m, ImageUrl = "https://images.unsplash.com/photo-1519238263530-99bdd11df2ea?w=400&h=300&fit=crop", RestaurantId = 3 },
                new FoodItem { Id = 8, Name = "Pantalón Vaquero", Price = 29.99m, ImageUrl = "https://images.unsplash.com/photo-1473966968600-fa801b869a1a?w=400&h=300&fit=crop", RestaurantId = 3 },
                new FoodItem { Id = 9, Name = "Sudadera con Capucha", Price = 34.99m, ImageUrl = "https://images.unsplash.com/photo-1556821840-3a63f95609a7?w=400&h=300&fit=crop", RestaurantId = 3 },
                new FoodItem { Id = 10, Name = "Bolso de Cuero", Price = 89.99m, ImageUrl = "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?w=400&h=300&fit=crop", RestaurantId = 4 },
                new FoodItem { Id = 11, Name = "Cinturón Premium", Price = 45.99m, ImageUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=400&h=300&fit=crop", RestaurantId = 4 },
                new FoodItem { Id = 12, Name = "Gafas de Sol", Price = 65.99m, ImageUrl = "https://images.unsplash.com/photo-1572635196237-14b3f281503f?w=400&h=300&fit=crop", RestaurantId = 4 },
                new FoodItem { Id = 13, Name = "Zapatos Oxford", Price = 119.99m, ImageUrl = "https://images.unsplash.com/photo-1614252369475-531eba835eb1?w=400&h=300&fit=crop", RestaurantId = 5 },
                new FoodItem { Id = 14, Name = "Zapatillas Deportivas", Price = 89.99m, ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400&h=300&fit=crop", RestaurantId = 5 },
                new FoodItem { Id = 15, Name = "Botas Chelsea", Price = 99.99m, ImageUrl = "https://images.unsplash.com/photo-1638247025967-b4e38f787b76?w=400&h=300&fit=crop", RestaurantId = 5 }
            };

            return foodItems.FirstOrDefault(f => f.Id == foodItemId) ?? new FoodItem();
        }
    }

    public class AddCartItemRequest
    {
        public int FoodItemId { get; set; }
        public int Quantity { get; set; }
        public string SpecialInstructions { get; set; } = string.Empty;
    }

    public class UpdateCartItemRequest
    {
        public int Quantity { get; set; }
        public string SpecialInstructions { get; set; } = string.Empty;
    }
}
