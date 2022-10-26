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
        readonly EshopDbContext _eshopDbContext;
        public CarouselController(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }

        public IActionResult Select()
        {
            List<CarouselItem> carouselItems = _eshopDbContext.CarouselItems.ToList();
            return View(carouselItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarouselItem carouselItem)
        {
            _eshopDbContext.CarouselItems.Add(carouselItem);
            _eshopDbContext.SaveChanges();
            return RedirectToAction(nameof(Select));
        }

        public IActionResult Edit(int ID)
        {
            CarouselItem carouselItem = _eshopDbContext.CarouselItems.FirstOrDefault(carItem => carItem.ID == ID);

            if (carouselItem != null)
            {
                return View(carouselItem);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(CarouselItem carouselItemFromForm)
        {
            CarouselItem carouselItem = _eshopDbContext.CarouselItems.FirstOrDefault(carItem => carItem.ID == carouselItemFromForm.ID);

            if (carouselItem != null)
            {
                carouselItem.ImageSrc = carouselItemFromForm.ImageSrc;
                carouselItem.ImageAlt = carouselItemFromForm.ImageAlt;
                
                _eshopDbContext.SaveChanges();

                return RedirectToAction(nameof(Select));
            }

            return NotFound();
        }

        public IActionResult Delete(int ID)
        {
            CarouselItem carouselItem = _eshopDbContext.CarouselItems.FirstOrDefault(carItem => carItem.ID == ID);
        
            if (carouselItem != null)
            {
                _eshopDbContext.CarouselItems.Remove(carouselItem);
                _eshopDbContext.SaveChanges();

                return RedirectToAction(nameof(Select));
            }

            return NotFound();
        }
    }
}
