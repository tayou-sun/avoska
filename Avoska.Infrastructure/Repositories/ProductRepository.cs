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
      .Where(x => x.Tags.Where(x => x.Parent != null).Select(x => x.Parent.Id).Contains(tagId))

        .ToList()
        .Where(x => x.Name != "Чай зеленый Tess Flirt клубника-белый персик 2 г х 25 шт").ToList();



    public IEnumerable<ProductDto> GetProductsByChildTagId(int tagId, int mode, int page = 0)
    {

        var a = appDbContext.Products
.Include(x => x.Tags)
.ThenInclude(x => x.Parent)
.ThenInclude(x => x.Store)
//.Where(x => x.Tags.Select(x => x.Id).Contains(tagId))
.Where(x => x.Tags.Select(x => x.Id).Contains(tagId) && x.IsAvailable).Select(x => new ProductDto()
{
    Name = x.Name,
    Price = x.Price,
    NewPrice = x.NewPrice,
    ImageUrl = x.ImageUrl,
    TagName = x.Tags[0].Name,
    TagId = x.Tags[0].Id,
    Store = x.Tags[0].Parent.Store != null
                ? (new StoreDto()
                {
                    Id = x.Tags[0].Parent.Store.Id,
                    Name = x.Tags[0].Parent.Store.Name,
                    DeliveryPrice = x.Tags[0].Parent.Store.DeliveryPrice,
                    MinSum = x.Tags[0].Parent.Store.MinSum,
                    StopTime = x.Tags[0].Parent.Store.StopTime,
                    StartTime = x.Tags[0].Parent.Store.StartTime,
                })
                : null,
    Id = x.Id,
    Options = x.Options
})
        .OrderBy(x => x.Price)
        .Skip(page * 20)
        .Take(20).
        ToList();

        /*  var b = a.Select(x => new ProductDto()
         {
             Name = x.Name,
             Price = x.Price,
             NewPrice = x.NewPrice,
             ImageUrl = x.ImageUrl,
             TagName = x.Tags[0].Name,
             TagId = x.Tags[0].Id,
             Store = x.Tags[0].Parent?.Store != null
                 ? new StoreDto() { Id = x.Tags[0].Parent?.Store.Id, Name = x.Tags[0].Parent?.Store.Name }
                 : null,
             Id = x.Id,
             Options = x.Options
         })
         .OrderBy(x => x.Price)
         .Skip(page * 20)
         .Take(20)
         .ToList(); */

        /*  if (mode == 1)
         {
             b = b.OrderBy(x => x.Price).ToList();
         }
         else if (mode == 2)
         {
             b = b.OrderByDescending(x => x.Price).ToList();
         }
  */


        return a;
    }


    public IEnumerable<Product> GetProductsByName(string name, int sort = -1)
    {


        var a = appDbContext.Products.Where(x => name != null && x.Name.ToLower().Contains(name.ToLower())).ToList();

        if (sort == 0)
        {
            a = a.OrderBy(x => x.Price).ToList();
        }
        else if (sort == 1)
        {
            a = a.OrderByDescending(x => x.Price).ToList();
        }
        return a;
    }

    public ProductDto GetDetailById(int id)
    {
        var product = appDbContext.Products.Include(x => x.Features).Include(x => x.Tags).ThenInclude(x => x.Parent).ThenInclude(x => x.Store).FirstOrDefault(x => x.Id == id);

        var productDto = new ProductDto()
        {
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            TagName = product.Tags[0].Name,
            TagId = product.Tags[0].Id,
            Id = product.Id,
            Parent = product.Tags[0].Parent.Id,
            NewPrice = product.NewPrice,
            Options = product.Options,
            Store = product.Tags[0].Parent?.Store != null
                ? new StoreDto() {
                     Id = product.Tags[0].Parent.Store.Id,
                    Name = product.Tags[0].Parent.Store.Name,
                    DeliveryPrice = product.Tags[0].Parent.Store.DeliveryPrice,
                    MinSum = product.Tags[0].Parent.Store.MinSum,
                    StopTime = product.Tags[0].Parent.Store.StopTime,
                    StartTime = product.Tags[0].Parent.Store.StartTime,
                 }
                : null,

            Description = product.Description
        };
        return productDto;
    }

    public IEnumerable<ProductDto> GetSaleProducts()
    {

        var a = appDbContext.Products
.Include(x => x.Tags)
//.Where(x => x.Tags.Select(x => x.Id).Contains(tagId))
.Where(x => x.NewPrice != null).ToList();

        var b = a.Select(x => new ProductDto()
        {
            Name = x.Name,
            Price = x.Price,
            NewPrice = x.NewPrice,
            ImageUrl = x.ImageUrl,
            TagName = x.Tags[0].Name,
            TagId = x.Tags[0].Id,
            Id = x.Id
        })
        .ToList();

        b = b.OrderByDescending(x => x.Price).ToList();

        return b;
    }



    /*  ProductDto IProductRepository.GetDetailById(int id)
     {
         throw new NotImplementedException();
     } */
}