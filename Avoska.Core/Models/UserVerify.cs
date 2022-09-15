using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("UserVerify")]
public class UserVerify
{
     public int Id { get; set; }
    public string Phone { get; set; }
    public long Code { get; set; }

    public bool IsVerify { get; set; }

}