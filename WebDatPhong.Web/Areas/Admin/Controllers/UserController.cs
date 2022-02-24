using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ViewModel.Customers;
using ViewModel.Users;
using WebDatPhong.Service.Roles;
using WebDatPhong.Service.Users;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var users = userService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            users = users.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
            return View(users);
        }

        public ActionResult Create()
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var roles = roleService.GetAll();
            TempData["Roles"] = roles;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UserViewModel request)
        {
            var roles = roleService.GetAll();
            TempData["Roles"] = roles;
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            request.Password = GetMD5(request.Password);
            var response = this.userService.Create(request);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(request);
        }

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

        public ActionResult Delete(int Id)
        {
            var user = this.userService.GetUserById(Id);
            ViewBag.RoleName = this.roleService.GetRoleById(user.RoleId).RoleName;
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var user = this.userService.GetUserById(Id);
            ViewBag.RoleName = this.roleService.GetRoleById(user.RoleId).RoleName;
            var response = this.userService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }
            userLogin.Password = GetMD5(userLogin.Password);
            var response = this.userService.Login(userLogin);
            if (response.IsSuccessed)
            {
                var user = this.userService.GetAll().Where(s => s.UserName == userLogin.UserName).FirstOrDefault();
                Session["UserId"] = user.Id;
                Session["Username"] = user.UserName;
                Session["RoleId"] = user.RoleId;
                if (user.RoleId==1)
                {
                    return RedirectToAction("Index", "Databoard");
                }
                else
                {
                    return RedirectToAction("Index", "Intro");
                }
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(userLogin);
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["Username"] = null;
            Session["RoleId"] = null;
            return Redirect("/Admin/User/Login");
        }

        public ActionResult Edit(int Id)
        {
            var userFind = this.userService.GetUserById(Id);

            EditCustomerViewModel user = new EditCustomerViewModel() 
            {
                Id = userFind.Id,
                CustomerName = userFind.Name,
                Email = userFind.Email,
                Phone = userFind.Phone,
                Gender = userFind.Gender,
            };

            return View(user);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(EditCustomerViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = this.userService.Update(request);
            if (response.IsSuccessed)
            {
                TempData["Message"] = "Bạn đã cập nhật tài khoản thành công";
                return Redirect("/Admin/User/Edit/" + request.Id);
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(request);
        }

        public ActionResult EditPassword(int Id)
        {
            UpdatePasswordViewModel user = new UpdatePasswordViewModel();
            user.Id = Id;
            return View(user);
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
            var response = this.userService.UpdatePassword(request);
            if (response.IsSuccessed)
            {
                //return RedirectToAction("Detail","Customer",new {Id = request.Id });
                return RedirectToAction("Logout", "User");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(request);
        }
    }
}