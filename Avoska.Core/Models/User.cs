using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
      [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Login { get; set; }
    public string Phone { get; set; }

    public string Password { get; set; }
    public string Name { get; set; }

     public string Address { get; set; }
}