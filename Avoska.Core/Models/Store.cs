using System;
using System.Collections.Generic;

public class Store{

    public Store() {
        Tags = new List<Tag>();
    }
    public int Id {get;set;}
    public List<Tag> Tags{get;set;}

   
    public string Name {get;set;}
  
}