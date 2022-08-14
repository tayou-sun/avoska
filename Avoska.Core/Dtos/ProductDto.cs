using System.Collections.Generic;
public class ProductDto
{

    public ProductDto()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Price { get; set; }
    public decimal? NewPrice { get; set; }
    public string TagName { get; set; }
    public int TagId { get; set; }
    public List<Feature> Features { get; set; }
    public string ImageUrl { get; set; }
}