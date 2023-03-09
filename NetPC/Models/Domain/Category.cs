using System.ComponentModel.DataAnnotations.Schema;

namespace NetPC.Models.Domain
{
    [Table("Category")]
    public class Category
    {

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
