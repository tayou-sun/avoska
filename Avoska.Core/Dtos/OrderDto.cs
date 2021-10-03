using System;
using System.Collections.Generic;

public class OrderDto
{
    public int Id { get; set; }
    public List<OrderProductDto> Products { get; set; }

    public string Address { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }

    public string Comment { get; set; }

    public DateTime CreateDate { get; set; }
}