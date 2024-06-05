namespace Domain.Entities;

public class Specialty
{
    public int Id { get; set;}
    public string Name { get; set;}
    public List<Medic>? Medics { get; set;}

    public Specialty(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
