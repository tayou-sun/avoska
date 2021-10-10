using System.Collections.Generic;

public class Tag
{
    public Tag(){
        Products = new List<Product>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
      public string Code { get; set; }

    public string ImageUrl { get; set; }
    public Tag Parent { get; set; }

    public List<Product> Products {get;set;}
}