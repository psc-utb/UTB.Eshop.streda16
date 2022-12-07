using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Web.Models.Identity;

namespace UTB.Eshop.Web.Models.Entities
{
    [Table(nameof(Order))]
    public class Order
    {

        [Key]
        [Required]
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTimeCreated { get; protected set; }

        [StringLength(25)]
        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public IList<OrderItem> OrderItems { get; set; }

    }
}
