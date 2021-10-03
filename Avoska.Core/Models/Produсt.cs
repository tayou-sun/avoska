using System.Collections.Generic;
public class Product {

    public Product(){
        Tags = new List<Tag>();
        Features = new List<Feature>();
    }

    public int Id {get;set;}
    public string Name {get;set;}

    public decimal Price {get;set;}
    public List<Tag> Tags {get;set;}
    public List<Feature> Features{get;set;}
    public  string ImageUrl { get; set; }
}