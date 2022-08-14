using System;
using System.Collections.Generic;

public class StatusOrder
{

    public StatusOrder()
    {

    }
    public int Id { get; set; }
    public Status Status { get; set; }
    public DateTime CreateDate { get; set; }
    public Order Order { get; set; }

}