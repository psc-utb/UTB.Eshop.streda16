using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Web.Models.Database;
using UTB.Eshop.Web.Models.Entities;

namespace UTB.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        public IActionResult Select()
        {
            List<CarouselItem> carouselItems = DatabaseFake.CarouselItems;
            return View(carouselItems);
        }
    }
}
