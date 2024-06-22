using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MedicalCenter
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set;}
    public List<Appointment> Appointments { get; set;} = new List<Appointment>();
    public List<Medic> Medics{ get; set;} = new List<Medic>();

    // Constructor sin parámetros necesario para EF
    public MedicalCenter()
    {
    }

    public MedicalCenter(string name)
    {
        Name = name;
    }

}
