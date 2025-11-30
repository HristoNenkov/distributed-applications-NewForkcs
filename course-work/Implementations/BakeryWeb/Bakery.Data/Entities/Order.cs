using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.Now;
        public string UserId { get; set; } = null!;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
