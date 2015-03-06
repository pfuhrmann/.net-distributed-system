using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataModel;
using EShop.Models;

namespace EShop.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly DbModel _context = new DbModel();

        // GET: Products
        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _context.Products.ToList()
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
                    _context.Products.Where(p => p.Name.Contains(model.Name));

                return View(model);
            }

            model.Products = _context.Products.ToList();
            return View(model);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new ProductDetailViewModel
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Weight = product.Weight,
                BoxItemsAmount = product.BoxItemsAmount,
                Stock = product.StockLeft
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
                return RedirectToAction("AddToBasket", new {controller = "Basket", id, quantity = model.Quantity});
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            model.ProductId = product.Id;
            model.Name = product.Name;
            model.Price = product.Price;
            model.Weight = product.Weight;
            model.BoxItemsAmount = product.BoxItemsAmount;
            model.Stock = product.StockLeft;

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
