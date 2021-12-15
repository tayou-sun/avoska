using CStuffControl.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
public class OrderRepository : IOrderRepository
{
    private AppDbContext appDbContext;
    public OrderRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public void Create(OrderDto order)
    {

        var t = new List<OrderProduct>();

        foreach (var product in order.Products)
        {
            var a1 = new OrderProduct()
            {
                Name = product.Name,
                Price = product.Price,
                Count = product.Count
            };

            t.Add(a1);
            appDbContext.OrderProducts.Add(a1);

        }
        appDbContext.SaveChanges();

        var orderToSave = new Order()
        {
            // public List<OrderProduct> Products{get;set;}
            Products = t,
            Address = order.Address,
            Name = order.Name,
            Phone = order.Phone,

            Comment = order.Comment,

            CreateDate = order.CreateDate,
            ChangeMode = order.ChangeMode,
            DeliveryMode = order.DeliveryMode
             
        };


        var a = appDbContext.Orders.ToList();
        appDbContext.Orders.Add(orderToSave);
        appDbContext.SaveChanges();
    }
}