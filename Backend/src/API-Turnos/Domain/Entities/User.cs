using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public abstract class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Password { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Email { get; set; }

    [Column(TypeName = "nvarchar(20)")]
    public string UserType { get; set; }

}

