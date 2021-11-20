using System.Collections.Generic;

public interface IProductRepository {
    IEnumerable<Product> GetProducts ();
    IEnumerable<Product> GetProductsByTagId (int tagId);

    void Create(Product product);

     IEnumerable<ProductDto> GetProductsByChildTagId(int tagId);

     IEnumerable<Product> GetProductsByName(string name);
}