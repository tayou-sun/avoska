using CStuffControl.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private AppDbContext appDbContext;
    public UserRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public User Create(string phone, string password)
    {
        var person = appDbContext.Users.FirstOrDefault(x => x.Phone == phone);
        
        if (person == null ){
        var u = new User();
        u.Phone = phone;
        u.Password = password;

        appDbContext.Users.Add(u);
        appDbContext.SaveChanges();

        return u;
        }
        return null;

    }

    public User Get(string phone, string password)
    {
        var person = appDbContext.Users.FirstOrDefault(x => x.Phone == phone && x.Password == password);
        return person;
    }

    public User Update(User user)
    {
        var result = appDbContext.Users.SingleOrDefault(b => b.Phone == user.Phone);
        if (result != null)
        {
            result.Name = user.Name;
            result.Phone = user.Phone;
            result.Address = user.Address;

            
            appDbContext.SaveChanges();

           
        }
         return result;
    }
}