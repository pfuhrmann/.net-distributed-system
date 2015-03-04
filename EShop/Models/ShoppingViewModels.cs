using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models
{
    public class AddToBasketViewModel
    {

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public short Price { get; set; }

        public short Weight { get; set; }

        [Display(Name = "Items Amount")]
        public short BoxItemsAmount { get; set; }

        public int Stock { get; set; }

        [Required]
        public string Quantity { get; set; }
    }
}
