using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Specialty
{
    [Key]
    public int Id { get; set;}
    public string Name { get; set;}
    public List<Medic>? Medics { get; set;}

    public Specialty( string name)
    {
        
        Name = name;
    }
}
