using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatPhong.Service.Services;

namespace WebDatPhong.Web.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }

        // GET: Service
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var listService = serviceService.GetAll();
            listService = listService.OrderBy(x => x.Id).ToPagedList(page, pageSize);
            return View(listService);
        }

        public ActionResult Detail(int Id)
        {
            var Service = serviceService.GetServiceById(Id);
            return View(Service);
        }
    }
}