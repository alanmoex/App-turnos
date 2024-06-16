using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Specialty
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set;}
    public string Name { get; set;}

    // Constructor sin parámetros necesario para EF
    public Specialty()
    {
    }
    public Specialty( string name)
    {
        
        Name = name;
    }
}
