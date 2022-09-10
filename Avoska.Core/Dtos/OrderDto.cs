using System;
using System.Collections.Generic;

public class OrderDto
{
    public OrderDto()
    {
        Products = new List<OrderProductDto>();
    }
    public int Id { get; set; }
    public List<OrderProductDto> Products { get; set; }

    public string Address { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Image { get; set; }
    public string Comment { get; set; }

    public DateTime CreateDate { get; set; }
    public int? Total { get; set; }

    public string Instagram { get; set; }
    public int? DeliveryMode { get; set; }
    public int? ChangeMode { get; set; }
    public string Code { get; set; }

    public string UserLogin { get; set; }

    public string Status { get; set; }
    public int? StatusId { get; set; }
}