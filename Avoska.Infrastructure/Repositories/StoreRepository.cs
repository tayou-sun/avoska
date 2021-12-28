using CStuffControl.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class StoreRepository : IStoreRepository
{
    private AppDbContext appDbContext;
    public StoreRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

   

    public IEnumerable<Store> GetStores()
    {

        var rooms = appDbContext.Stores.Include(x=>x.Tags).ToList();
        return rooms.ToList();
    }





}