using System.Collections.Generic;
public class ProductDto
{

    public ProductDto()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Parent { get; set; }

    public decimal Price { get; set; }
    public decimal? NewPrice { get; set; }
    public string TagName { get; set; }
    public string Description { get; set; }
    public int TagId { get; set; }
    public StoreDto Store {get;set;}
    public List<Feature> Features { get; set; }
    public string ImageUrl { get; set; }

    public List<Option> Options { get; set; }
}