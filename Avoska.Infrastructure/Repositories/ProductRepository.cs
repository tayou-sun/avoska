using CStuffControl.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private AppDbContext appDbContext;
    public ProductRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public void Create(Product product)
    {
        var p1 = new Product()
        {
            Name = "product1",
            ImageUrl = "https://avoska-dostavka.ru/avoska_images/%D0%9D%D0%B0%D1%85%D0%BE%D0%B4%D0%BA%D0%B0/%D0%9D%D0%BE%D0%B2%D1%8B%D0%B5%D0%9A%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D0%B8%D0%B8/%D0%90%D1%80%D0%B1%D1%83%D0%B7%D1%8B.png"
        };

        var p2 = new Product()
        {
            Name = "product2",
            ImageUrl = "https://avoska-dostavka.ru/avoska_images/%D0%9D%D0%B0%D1%85%D0%BE%D0%B4%D0%BA%D0%B0/%D0%9D%D0%BE%D0%B2%D1%8B%D0%B5%D0%9A%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D0%B8%D0%B8/%D0%90%D1%80%D0%B1%D1%83%D0%B7%D1%8B.png"
        };

        var t1 = new Tag()
        {
            Name = "tag1",
            ImageUrl = "https://avoska-dostavka.ru/avoska_images/%D0%9D%D0%B0%D1%85%D0%BE%D0%B4%D0%BA%D0%B0/%D0%9D%D0%BE%D0%B2%D1%8B%D0%B5%D0%9A%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D0%B8%D0%B8/%D0%90%D1%80%D0%B1%D1%83%D0%B7%D1%8B.png"

        };

        var t2 = new Tag()
        {
            Name = "tag2",
            ImageUrl = "https://avoska-dostavka.ru/avoska_images/%D0%9D%D0%B0%D1%85%D0%BE%D0%B4%D0%BA%D0%B0/%D0%9D%D0%BE%D0%B2%D1%8B%D0%B5%D0%9A%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D0%B8%D0%B8/%D0%90%D1%80%D0%B1%D1%83%D0%B7%D1%8B.png"

        };



        appDbContext.Tags.Add(t1);
        appDbContext.Tags.Add(t2);

        appDbContext.SaveChanges();

        p1.Tags.Add(t1);
        p2.Tags.Add(t1);

        appDbContext.Products.Add(p1);
        appDbContext.Products.Add(p2);

        appDbContext.SaveChanges();
    }

    public IEnumerable<Product> GetProducts() => appDbContext.Products.ToList();


    public IEnumerable<Product> GetProductsByTagId(int tagId) => appDbContext.Products
        .Include(x => x.Tags)
       //.Where(x => x.Tags.Select(x => x.Id).Contains(tagId))
      .Where(x => x.Tags.Where(x=>x.Parent !=null).Select(x => x.Parent.Id).Contains(tagId))
        .ToList();


}