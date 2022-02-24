using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebDatPhong.Service.Customers;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: Admin/Customer
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var customers = customerService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.CustomerName.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            customers = customers.OrderBy(x => x.Id).ToPagedList(page, pageSize);
            return View(customers);
        }

        public ActionResult Delete(int Id)
        {
            var customer = this.customerService.GetCustomerById(Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var customer = this.customerService.GetCustomerById(Id);
            var response = this.customerService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(customer);
        }
    }
}