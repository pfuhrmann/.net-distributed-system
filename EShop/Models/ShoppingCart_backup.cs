using System.Collections.Generic;
using System.Linq;

namespace DataModel
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public List<CartItem> Items { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }

        public int CartPrice
        {
            get { return Items.Sum(i => i.ItemPrice); }
        }

        public void AddItem(CartItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(CartItem item)
        {
            Items.Remove(item);
        }
    }
}
