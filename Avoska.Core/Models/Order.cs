using System;
using System.Collections.Generic;

public class Order
{

    public Order()
    {
        Products = new List<OrderProduct>();
    }
    public int Id { get; set; }
    public List<OrderProduct> Products { get; set; }

    public string Address { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }

    public string Comment { get; set; }
    public string Code { get; set; }

    public DateTime CreateDate { get; set; }

    public string Instagram { get; set; }
    public int? DeliveryMode { get; set; }
    public int? ChangeMode { get; set; }

    public User User { get; set; }

    public List<StatusOrder> StatusOrders {get;set;}
}