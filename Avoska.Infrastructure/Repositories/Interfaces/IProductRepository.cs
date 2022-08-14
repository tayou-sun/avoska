using System.Collections.Generic;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    IEnumerable<Product> GetProductsByTagId(int tagId);

    void Create(Product product);

    IEnumerable<ProductDto> GetProductsByChildTagId(int tagId, int mode);

    IEnumerable<Product> GetProductsByName(string name, int sort);

    ProductDto GetDetailById(int id);

     IEnumerable<ProductDto> GetSaleProducts();
}