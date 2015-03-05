using System.Web;
using System.Web.Mvc;
using DataModel;
using EShop.Models;
using Microsoft.AspNet.Identity;

namespace EShop
{
    public class ShoppingCartActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string username = HttpContext.Current.User.Identity.GetUserName();
            var cart = ShoppingCart.GetCart(username);
            filterContext.Controller.ViewBag.CartItems = cart.GetItems();
        }
    }
}
