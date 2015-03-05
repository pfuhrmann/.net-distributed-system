using System.Web;
using System.Web.Mvc;
using EShop.Models;
using Microsoft.AspNet.Identity;

namespace EShop
{
    public class BasketActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var username = HttpContext.Current.User.Identity.GetUserName();
            var basket = Basket.GetBasket(username);
            filterContext.Controller.ViewBag.BasketItems = basket.GetItems();
        }
    }
}
