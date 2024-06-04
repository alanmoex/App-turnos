using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public abstract class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    
}

