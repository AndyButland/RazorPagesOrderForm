using System.ComponentModel.DataAnnotations;

namespace Pluralsight.OrderForm.Models;

public class Order
{
    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Product")]
    public string Product { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [Display(Name = "Address")]
    public string Address { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Comments")]
    public string? Comments { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the data collection policy.")]
    [Display(Name = "I agree to my data being collected to fulfill this request")]
    public bool AgreeToTerms { get; set; }

    public static List<string> AvailableProducts =>
    [
        "T-Shirt",
        "Mug",
        "Baseball Cap",
        "Hoodie",
        "Laptop Stickers",
        "Water Bottle",
        "Tote Bag"
    ];
}
