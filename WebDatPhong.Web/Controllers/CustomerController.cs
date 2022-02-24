using AutoMapper;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using ViewModel.Customers;
using WebDatPhong.Service.Customers;

namespace WebDatPhong.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(CustomerViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            request.Password = GetMD5(request.Password);
            var response = this.customerService.Create(request);
            if (response.IsSuccessed)
            {
                return RedirectToAction("Login", "Customer");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(request);
        }

        public ActionResult Login(string returnUrl)
        {
            //ViewBag.OldUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel customerLogin, string returnUrl)
        {
            //ViewBag.OldUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(customerLogin);
            }
            customerLogin.Password = GetMD5(customerLogin.Password);
            var response = this.customerService.Login(customerLogin);
            if (response.IsSuccessed)
            {
                var customer = this.customerService.GetAll().Where(s => s.UserName == customerLogin.UserName).FirstOrDefault();
                Session["CustomerId"] = customer.Id;
                Session["CustomerUsername"] = customer.UserName;
                if (Session["Cart"] == null)
                {
                    return Redirect("/");
                }
                return RedirectToAction("Create", "Booking");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(customerLogin);
        }

        public ActionResult Logout(string returnUrl)
        {
            Session["CustomerId"] = null;
            Session["CustomerUsername"] = null;
            return Redirect("/");
        }

        public ActionResult Edit(int Id)
        {
            var customer = this.customerService.GetCustomerById(Id);

            EditCustomerViewModel customer1 = Mapper.Map<EditCustomerViewModel>(customer);

            return View(customer1);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(EditCustomerViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = this.customerService.Update(request);
            if (response.IsSuccessed)
            {
                //return RedirectToAction("Detail","Customer",new {Id = request.Id });
                TempData["Message"] = "Quý khách đã cập nhật tài khoản thành công";
                return Redirect("/Customer/Edit/"+request.Id);
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(request);
        }

        public ActionResult EditPassword(int Id)
        {
            UpdatePasswordViewModel customer = new UpdatePasswordViewModel();
            customer.Id = Id;
            return View(customer);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditPassword(UpdatePasswordViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            request.NewPassword = GetMD5(request.NewPassword);
            request.OldPassword = GetMD5(request.OldPassword);
            var response = this.customerService.UpdatePassword(request);
            if (response.IsSuccessed)
            {
                //return RedirectToAction("Detail","Customer",new {Id = request.Id });
                return RedirectToAction("Logout", "Customer");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(request);
        }

        //public ActionResult Detail(int Id)
        //{
        //    var customer = this.customerService.GetCustomerById(Id);

        //    return View(customer);
        //}

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}