using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataModel;

namespace EShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly DbModel _context = new DbModel();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = _context.Orders.Include(o => o.Customer).Include(o => o.DestinationWarehouse);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
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
