using CStuffControl.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class StatusRepository : IStatusRepository
{
    private AppDbContext appDbContext;
    public StatusRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }



    public string GetDetailById(string phone)
    {
        var preparePhone = phone;//.Replace(" ", "+");
        var orders = this.appDbContext.StatusOrder
        .Include(x => x.Order)
        .ThenInclude(x => x.User)
        .Include(x => x.Status)
        .Where(x => x.Order.User.Phone == preparePhone).ToList();

        if (orders.Count == 0)
            return null;

        var sortedOrders = orders?.OrderByDescending(x => x.Id).ToList().First();


        return sortedOrders.Status == null || sortedOrders.Status.Id == 4 ? null : sortedOrders.Status.Name;
    }
/*     void SetStatus(int orderId, int statusId)
    {
        appDbContext.StatusOrder.Add(new StatusOrder(){
            Status = appDbContext.Status.FirstOrDefault(x=>x.Id == statusId),
            CreateDate = DateTime.Now,
            Order = appDbContext.Orders.FirstOrDefault(x=>x.Id == orderId)
        });
        appDbContext.SaveChanges();
    } */

    Order IStatusRepository.SetStatus(int orderId, int statusId)
    {
        var order = appDbContext.Orders.FirstOrDefault(x=>x.Id == orderId);
           appDbContext.StatusOrder.Add(new StatusOrder(){
            Status = appDbContext.Status.FirstOrDefault(x=>x.Id == statusId),
            CreateDate = DateTime.Now,
            Order = order
        });
        appDbContext.SaveChanges();

        return order;
    }
}