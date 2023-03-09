using System.ComponentModel.DataAnnotations.Schema;

namespace NetPC.Models.Domain
{
    [Table("Subcategory")]
    public class Subcategory
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
