using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{

    public class SysAdminUpdateRequest
    {
        [StringLength(100, MinimumLength = 1)]
        public string? Name { get; set; }

        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        public string? Email { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }

    }
}