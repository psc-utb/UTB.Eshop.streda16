using System;
using System.Collections.Generic;
using UTB.Eshop.Web.Models.Entities;

namespace UTB.Eshop.Web.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public IList<CarouselItem> CarouselItems { get; set; }
        public IList<Product> Products { get; set; }
    }
}