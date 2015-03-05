﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataModel;

namespace EShop.Models
{
    public class ProductDetailViewModel
    {
        private int _quantity = 1;
        public string Name { get; set; }

        public int ProductId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public short Price { get; set; }

        public short Weight { get; set; }

        [Display(Name = "Items Amount")]
        public short BoxItemsAmount { get; set; }

        public int Stock { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Mimimum quantity is 1")]
        [StockLevelValidation("ProductId", false)]
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
    }

    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            Products = new List<Product>();
        }

        public virtual IEnumerable<Product> Products { get; set; }

        public string Name { get; set; }
    }

    public class BasketViewModel
    {
        public List<BasketItem> BasketItems { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal BasketTotal { get; set; }
    }
}