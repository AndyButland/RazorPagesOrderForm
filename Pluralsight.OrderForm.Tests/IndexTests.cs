using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pluralsight.OrderForm.Models;
using Pluralsight.OrderForm.Pages;

namespace Pluralsight.OrderForm.Tests;

public class IndexTests
{
    [Fact]
    public void OnPost_InvalidModel_ReturnsPageWithValidationErrors()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public void OnPost_ValidModelWithMug_ReturnsPageWithValidationError()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public void OnPost_ValidModelWithOtherProduct_RedirectsToConfirmation()
    {
        // Arrange

        // Act

        // Assert
    }
}
