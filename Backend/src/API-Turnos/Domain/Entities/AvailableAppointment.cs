
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class AvailableAppointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public Medic Medic { get; set; }

    public AvailableAppointment()
    {
    }
    public AvailableAppointment(DateTime dateTime, Medic medic)
    {
        DateTime = dateTime;
        Medic = medic;
    }
}