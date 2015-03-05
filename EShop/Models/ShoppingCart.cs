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
            var cartItem = _context.CartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                     && c.ProductId == product.Id);

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
            var cartItem = _context.CartItems.Single(
                cart => cart.CartId == ShoppingCartId
                        && cart.ProductId == id);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public List<CartItem> GetCartItems()
        {
            return _context.CartItems.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetTotal()
        {
            int? total = (from cartItems in _context.CartItems
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Quantity *
                              cartItems.Product.Price).Sum();

            return total ?? 0;
        }

        public int GetItems()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _context.CartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
    }
}
