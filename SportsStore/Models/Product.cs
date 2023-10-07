
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    public class Product
    {
        public long ProductId { get; set; } 
        
        [Column(TypeName ="varchar(100)"),Required(ErrorMessage ="Please Enter Product Name")]
        public string Name { get; set; }=String.Empty;
        
        [Column(TypeName = "decimal(8,2)"),Range(0.01,double.MaxValue,ErrorMessage ="Price must be Valuable")]
        public decimal Price { get; set; }
        
        [Column(TypeName ="varchar(300)")]
        public string Description { get; set; } = String.Empty;
        
        [Column(TypeName ="varchar(300)"),Required(ErrorMessage ="Please Enter Category")]
        public string Category { get; set; }=string.Empty;

    }
}
