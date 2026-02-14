using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pluralsight.OrderForm.Models;

namespace Pluralsight.OrderForm.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public Order Order { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (Order.Product == "Mug")
        {
            ModelState.AddModelError("Order.Product", "Sorry, all mugs have been given away.");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Process the order here (e.g., save to database).

        return RedirectToPage("OrderConfirmation");
    }
}
