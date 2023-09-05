
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    public class Product
    {
        public long ProductId { get; set; } 
        
        [Column(TypeName ="varchar(100)"),Required]
        public string Name { get; set; }=String.Empty;
        
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        

        [Column(TypeName ="varchar(300)")]
        public string Description { get; set; } = String.Empty;
        public string Category { get; set; }=string.Empty;

    }
}
