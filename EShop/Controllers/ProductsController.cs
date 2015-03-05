using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataModel;
using EShop.Models;
using Microsoft.AspNet.Identity;

namespace EShop.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly DbModel db = new DbModel();
        // GET: Products
        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = db.Products.ToList()
            };

            return View(model);
        }

        // POST: Products
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ProductListViewModel model)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(model.Name))
            {
                model.Products =
                    db.Products.Where(p => p.Name.Contains(model.Name));

                return View(model);
            }

            model.Products = db.Products.ToList();
            return View(model);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new ProductDetailViewModel
            {
                Name = product.Name,
                Price = product.Price,
                Weight = product.Weight,
                BoxItemsAmount = product.BoxItemsAmount,
                Stock = product.StockTotal
            };

            return View(model);
        }

        // POST: Products/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ProductDetailViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("AddToCart", new {controller = "ShoppingCart", id, quantity = model.Quantity});
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            model.Name = product.Name;
            model.Price = product.Price;
            model.Weight = product.Weight;
            model.BoxItemsAmount = product.BoxItemsAmount;
            model.Stock = product.StockTotal;

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
