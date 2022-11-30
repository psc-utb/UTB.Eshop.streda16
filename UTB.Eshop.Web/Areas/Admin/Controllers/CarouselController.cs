using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Domain.Abstraction;
using UTB.Eshop.Web.Models.Database;
using UTB.Eshop.Web.Models.Entities;
using UTB.Eshop.Web.Models.Identity;
using UTB.Eshop.Web.Models.ViewModels;

namespace UTB.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        readonly EshopDbContext _eshopDbContext;
        IFileUpload _fileUpload;
        ICheckFileContent _checkFileContent;
        ICheckFileLength _checkFileLenght;
        public CarouselController(EshopDbContext eshopDbContext,
                                IFileUpload fileUpload,
                                ICheckFileContent checkFileContent,
                                ICheckFileLength checkFileLenght)
        {
            _eshopDbContext = eshopDbContext;
            _fileUpload = fileUpload;
            _checkFileContent = checkFileContent;
            _checkFileLenght = checkFileLenght;
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
        public async Task<IActionResult> Create(CarouselItemImageRequired carouselItem)
        {
            ModelState.Remove(nameof(CarouselItem.ImageSrc));
            if (ModelState.IsValid)
            {
                string contentType = "image";
                long maxLength = 5_000_000;
                if (_checkFileLenght.CheckFileLength(carouselItem.Image, maxLength))
                {
                    _fileUpload.ContentType = contentType;
                    _fileUpload.FileLength = maxLength;
                    carouselItem.ImageSrc = await _fileUpload.FileUploadAsync(carouselItem.Image, Path.Combine("img", "carousel"));

                    ModelState.Clear();
                    if (TryValidateModel(carouselItem))
                    {
                        _eshopDbContext.CarouselItems.Add(carouselItem);
                        _eshopDbContext.SaveChanges();
                        return RedirectToAction(nameof(Select));
                    }
                }
            }

            return View(carouselItem);
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
        public async Task<IActionResult> Edit(CarouselItem carouselItemFromForm)
        {
            CarouselItem carouselItem = _eshopDbContext.CarouselItems.FirstOrDefault(carItem => carItem.ID == carouselItemFromForm.ID);

            if (carouselItem != null)
            {
                ModelState.Remove(nameof(CarouselItem.ImageSrc));
                if (ModelState.IsValid)
                {
                    if (carouselItemFromForm.Image != null)
                    {
                        string contentType = "image";
                        long maxLength = 5_000_000;
                        if (_checkFileLenght.CheckFileLength(carouselItemFromForm.Image, maxLength))
                        {
                            _fileUpload.ContentType = contentType;
                            _fileUpload.FileLength = maxLength;
                            carouselItemFromForm.ImageSrc = await _fileUpload.FileUploadAsync(carouselItemFromForm.Image, Path.Combine("img", "carousel"));

                            ModelState.Clear();
                            if (TryValidateModel(carouselItemFromForm))
                            {
                                carouselItem.ImageSrc = carouselItemFromForm.ImageSrc;
                            }
                            else
                                return View(carouselItemFromForm);
                        }
                        else
                            return View(carouselItemFromForm);
                    }

                    carouselItem.ImageAlt = carouselItemFromForm.ImageAlt;

                    _eshopDbContext.SaveChanges();

                    return RedirectToAction(nameof(Select));
                }

                return View(carouselItemFromForm);
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
