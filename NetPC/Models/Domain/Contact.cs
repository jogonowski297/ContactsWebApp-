using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetPC.Models.Domain
{

    [Table("Contacts")]
    public class Contact
    {

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Category { get; set; }
        public string? Subcategory { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBrith { get; set; }
        
    }
}
