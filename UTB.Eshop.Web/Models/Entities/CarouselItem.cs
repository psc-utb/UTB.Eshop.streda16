using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UTB.Eshop.Web.Models.Entities
{
    [Table("CarouselItem")]
    public class CarouselItem
    {
        [Key]
        public int ID { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageSrc { get; set; }
        public string ImageAlt { get; set; }
    }
}
