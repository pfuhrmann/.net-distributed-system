using System.Collections.Generic;
using System.Linq;
using DataModel;

namespace EShop.Models
{
    public class ShoppingCart
    {
        private readonly DbModel _context = new DbModel();
        private string ShoppingCartId { get; set; }

        public static ShoppingCart GetCart(string username)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = username;
            return cart;
        }

        public void AddToCart(Product product, int quantity)
        {
            // Check if cart item already exists
            // with given product ID
            var cartItem = GetCartItem(product.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                    ProductId = product.Id,
                    CartId = ShoppingCartId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                // Item already exists
                cartItem.Quantity += quantity;
            }

            _context.SaveChanges();
        }

        public void RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = GetCartItem(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void Update(List<CartItem> cartItems)
        {
            // Loop through basket items and update each
            foreach (var item in cartItems)
            {
                var cartItem = GetCartItem(item.ProductId);
                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new CartItem
                    {
                        ProductId = item.ProductId,
                        CartId = ShoppingCartId,
                        Quantity = item.Quantity
                    };
                    _context.CartItems.Add(cartItem);
                }
                else
                {
                    // Item already exists
                    if (item.Quantity > 0)
                    {
                        cartItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        _context.CartItems.Remove(cartItem);
                    }
                }
            }

            _context.SaveChanges();
        }

        public int GetTotalPrice()
        {
            // Get price of each item in the basket and sum them up
            var total = (from cartItems in _context.CartItems
                where cartItems.CartId == ShoppingCartId
                select (int?) cartItems.Quantity*
                       cartItems.Product.Price).Sum();

            return total ?? 0;
        }

        public int GetItems()
        {
            // Get the count of each item in the basket and sum them up
            var count = (from cartItems in _context.CartItems
                where cartItems.CartId == ShoppingCartId
                select (int?) cartItems.Quantity).Sum();

            return count ?? 0;
        }

        public CartItem GetCartItem(int productId)
        {
            return _context.CartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                     && c.ProductId == productId);
        }

        public List<CartItem> GetCartItems()
        {
            return _context.CartItems.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
    }
}
