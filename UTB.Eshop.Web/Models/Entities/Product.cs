using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UTB.Eshop.Web.Models.Entities
{
    [Table(nameof(Product))]
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}