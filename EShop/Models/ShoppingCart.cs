using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;

namespace EShop.Models
{
    public class ShoppingCart
    {
        public const string CartSessionKey = "CartId";
        private readonly DbModel _context = new DbModel();
        private string ShoppingCartId { get; set; }

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
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

        public void RemoveFromCart(Product product)
        {
            // Get the cart
            var cartItem = _context.CartItems.Single(
                cart => cart.CartId == ShoppingCartId
                        && cart.ProductId == product.Id);

            var itemCount = 0;

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
                select (int?) cartItems.ItemPrice*
                       cartItems.Product.Price).Sum();

            return total ?? 0;
        }

        /*public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count*item.Album.Price);

                _context.OrderDetails.Add(orderDetail);
            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            _context.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }*/

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    var tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
    }
}
