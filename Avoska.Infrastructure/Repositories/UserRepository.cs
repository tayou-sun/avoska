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

        if (person == null)
        {
            var u = new User();
            u.Phone = phone;
            u.Password = password;

            appDbContext.Users.Add(u);
            appDbContext.SaveChanges();

            return u;
        }
        return null;

    }

    public User Get(string phone)
    {
        var person = appDbContext.Users.FirstOrDefault(x => x.Phone == phone);
        return person;
    }

    public User Update(UserChange user)
    {
        var result = appDbContext.Users.SingleOrDefault(b => b.Phone == user.OldPhone);
        if (result != null)
        {
            result.Name = user.Name;
            result.Phone = user.Phone;
            result.Address = user.Address;


            appDbContext.SaveChanges();


        }
        return result;
    }


    void IUserRepository.SaveToken(UserVerify user)
    {
        try
        {
            appDbContext.UserVerifies.Add(user);
            appDbContext.SaveChanges();
        }
        catch (Exception e)
        {
            var a = 1;
        }
    }

    UserVerify IUserRepository.Verify(User1 user)
    {
        var res = appDbContext.UserVerifies.FirstOrDefault(x => x.Code == user.Password && x.Phone == user.Phone && x.IsVerify == false);

        if (res != null)
        {
            res.IsVerify = true;
            appDbContext.SaveChanges();
        }
        return res;
    }
}