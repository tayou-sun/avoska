using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;


[Table("UserVerify")]
public class UserVerify
{
    public int Id { get; set; }
    public string Phone { get; set; }
    public long Code { get; set; }

    public bool IsVerify { get; set; }

    public DateTime CreateDate { get; set; }

    public int? MessageId { get; set; }

}