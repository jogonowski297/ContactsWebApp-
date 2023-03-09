using Microsoft.AspNetCore.Identity;

namespace NetPC.Data
{
    public class ApplicationUser: IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBrith { get; set; }
        public string? Category { get; set; }
        public string? Subcategory { get; set; }
    }
}
