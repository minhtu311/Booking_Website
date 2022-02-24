using System.Linq;
using System.Web.Mvc;
using WebDatPhong.Service.RoomTypes;
using WebDatPhong.Service.Services;
using WebDatPhong.Service.Sliders;

namespace WebDatPhong.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoomTypeService roomTypeService;
        private readonly IServiceService serviceService;
        private readonly ISliderService sliderService;

        public HomeController(IRoomTypeService roomTypeService, IServiceService serviceService, ISliderService sliderService)
        {
            this.roomTypeService = roomTypeService;
            this.serviceService = serviceService;
            this.sliderService = sliderService;
        }

        public ActionResult Index()
        {
            var roomTypes = roomTypeService.GetAll().Where(s => s.Status == true).ToList();
            TempData["RoomTypes"] = roomTypes;
            var sliders = sliderService.GetAll();
            TempData["Sliders"] = sliders;
            var services = serviceService.Get4Service(4);
            TempData["Service"] = services;
            return View();
        }
    }
}