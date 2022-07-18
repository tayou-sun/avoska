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

    public Order Create(OrderDto order)
    {
        
        var preparePhone = order.UserLogin != null ? order.UserLogin.Replace(" ", "+") : "";
        var t = new List<OrderProduct>();

        var user = appDbContext.Users.FirstOrDefault(x => x.Phone == preparePhone);
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

            CreateDate = DateTime.Now,
            ChangeMode = order.ChangeMode,
            DeliveryMode = order.DeliveryMode,
            Code = order.Code,
            User = user

        };


        var a = appDbContext.Orders.ToList();
        appDbContext.Orders.Add(orderToSave);
        appDbContext.SaveChanges();

        return orderToSave;
    }

    public List<OrderDto> GetOrdersByUserPhone(string phone)
    {
        var preparePhone = phone.Replace(" ", "+");
        var orders = appDbContext.Orders
        .Include(x => x.Products)
        .Include(x => x.User)
        .Where(x => x.User.Phone == preparePhone).ToList();

        var orderDtos = orders.Select(x =>
        {
            var a = new OrderDto();
            a.Name = x.Name;
            a.Id = x.Id;
            a.Address = x.Address;
            a.CreateDate = x.CreateDate;
            a.Products = x.Products.Select(y => new OrderProductDto
            {
                ProductId = y.Id,
                Count = y.Count,
                Name = y.Name,
                Price = y.Price,
                Image =y.Image
                
            }).ToList();

            return a;
        }).ToList();

        /*    new OrderProductDto{
               ProductId = x.Id,
               Count = x.Count,
               Name = x.Name,
               Price = x.Price
           }).ToList();
    */
        return orderDtos;
    }
}