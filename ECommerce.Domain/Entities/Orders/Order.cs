using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order(string buyerEmail, 
                     OrderAddress shipToAddress,
                     ICollection<OrderItem> items,
                     DeliveryMethod deliveryMethod,
                     decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            Items = items;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }

        private Order()
        {
            
        }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string BuyerEmail { get; set; } = default!;
        public OrderAddress ShipToAddress { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal SubTotal { get; set; }
        public int DeliveryMethodId { get; set; }
        public decimal GetTotal()
            =>SubTotal + (DeliveryMethod?.Price ?? 0) ;
    }
}
