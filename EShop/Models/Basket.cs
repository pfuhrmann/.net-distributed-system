﻿using System.Collections.Generic;
using System.Linq;
using DataModel;

namespace EShop.Models
{
    public class Basket
    {
        private readonly DbModel _context = new DbModel();
        private string BasketId { get; set; }

        public static Basket GetBasket(string username)
        {
            var basket = new Basket();
            basket.BasketId = username;
            return basket;
        }

        public void AddToBasket(Product product, int quantity)
        {
            // Check if basket item already exists
            // with given product ID
            var basketItem = GetBasketItem(product.Id);

            if (basketItem == null)
            {
                // Create a new basket item
                basketItem = new BasketItem
                {
                    ProductId = product.Id,
                    BasketId = BasketId,
                    Quantity = quantity
                };
                _context.BasketItems.Add(basketItem);
            }
            else
            {
                // Item already exists
                basketItem.Quantity += quantity;
            }

            _context.SaveChanges();
        }

        public void RemoveFromBasket(int id)
        {
            var basketItem = GetBasketItem(id);
            if (basketItem != null)
            {
                _context.BasketItems.Remove(basketItem);
                _context.SaveChanges();
            }
        }

        public void Update(List<BasketItem> baksetItems)
        {
            // Loop through basket items and update each
            foreach (var item in baksetItems)
            {
                var basketItem = GetBasketItem(item.ProductId);
                if (basketItem == null)
                {
                    basketItem = new BasketItem
                    {
                        ProductId = item.ProductId,
                        BasketId = BasketId,
                        Quantity = item.Quantity
                    };
                    _context.BasketItems.Add(basketItem);
                }
                else
                {
                    // Item already exists
                    if (item.Quantity > 0)
                    {
                        basketItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        _context.BasketItems.Remove(basketItem);
                    }
                }
            }

            _context.SaveChanges();
        }

        public int GetTotalPrice()
        {
            // Get price of each item in the basket and sum them up
            var total = (from basketItems in _context.BasketItems
                where basketItems.BasketId == BasketId
                select (int?) basketItems.Quantity*
                       basketItems.Product.Price).Sum();

            return total ?? 0;
        }

        public int GetItems()
        {
            // Get the count of each item in the basket and sum them up
            var count = (from basketItems in _context.BasketItems
                where basketItems.BasketId == BasketId
                select (int?) basketItems.Quantity).Sum();

            return count ?? 0;
        }

        public BasketItem GetBasketItem(int productId)
        {
            return _context.BasketItems.SingleOrDefault(
                c => c.BasketId == BasketId
                     && c.ProductId == productId);
        }

        public List<BasketItem> GetBasketItems()
        {
            return _context.BasketItems.Where(
                bi => bi.BasketId == BasketId).ToList();
        }
    }
}
