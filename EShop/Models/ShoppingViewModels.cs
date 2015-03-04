using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models
{
    public class AddToBasketViewModel
    {
        private int _quantity = 1;

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public short Price { get; set; }

        public short Weight { get; set; }

        [Display(Name = "Items Amount")]
        public short BoxItemsAmount { get; set; }

        public int Stock { get; set; }

        [Required]
        [Range(1, 20)]
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
    }
}