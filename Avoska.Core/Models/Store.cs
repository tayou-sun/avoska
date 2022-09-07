using System;
using System.Collections.Generic;

public class Store
{

    public Store()
    {
        Tags = new List<Tag>();
    }
    public int Id { get; set; }
    public List<Tag> Tags { get; set; }

    public string Name { get; set; }
    public int? MinSum { get; set; }
    public int? DeliveryPrice { get; set; }
    public int? StartTime { get; set; }
    public int? StopTime { get; set; }
}