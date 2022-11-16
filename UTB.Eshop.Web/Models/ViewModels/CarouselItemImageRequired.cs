using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Web.Models.Entities;

namespace UTB.Eshop.Web.Models.ViewModels
{
    public class CarouselItemImageRequired : CarouselItem
    {
        [Required]
        public override IFormFile Image { get => base.Image; set => base.Image = value; }
    }
}
