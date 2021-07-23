using Asm.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asm.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "Order ID")]
        public int orderID { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public String productName { get; set; }

        [DisplayName("Photo")]
        public string photo { get; set; }

        [Required]
        [DisplayName("Quantity")]
        [Range(0, 9999999999)]
        public int quantity { get; set; }
        [Required]
        [DisplayName("Total")]
        [Range(0, 9999999999)]
        public double total { get; set; }
        [Required]
        [DisplayName("Date Order")]
        public DateTime? dateOrder { get; set; }

        [ForeignKey("DataUser")]
        [DisplayName("User Id")]
        [Required]
        public String UserID { get; set; }
        public DataUser DataUser { get; set; }
    }
}
