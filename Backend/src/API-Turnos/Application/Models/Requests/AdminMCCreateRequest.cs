﻿

namespace Application;

public class AdminMCCreateRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int MedicalCenterId { get; set; }
}
