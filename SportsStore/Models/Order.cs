using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models;

public class Order
{
    [BindNever] // mean that this property not mapped from the http request // and if the http request contains orderid that not mapped to it
    public int OrderId { get; set; }
    public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

    [Required(ErrorMessage ="Please Enter Name")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage ="Please Enter Address")]
    public string? AddressLine { get; set; }
    
    [Required(ErrorMessage ="Please Enter your City")]
    public string? City { get; set; }

    [Required(ErrorMessage ="Please Enter your Region")]
    public string? Region { get; set; }

    [Required(ErrorMessage ="Please Enter your Postal Code"), DisplayName("Zip Code")]
    public string? PostalCode { get; set; }

    [Required(ErrorMessage ="Please Enter your Phone Number"), MaxLength(11), MinLength(11) ]
    public string? Phone { get; set; }

    public bool? GiftCard { get; set; } = false;

    [BindNever]
    public bool Shipped { get; set; }






}
