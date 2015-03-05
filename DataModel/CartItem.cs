﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        [StockLevelValidation("ProductId")]
        public int Quantity { get; set; }

        public string CartId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
