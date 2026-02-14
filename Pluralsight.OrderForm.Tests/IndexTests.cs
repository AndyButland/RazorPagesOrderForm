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
        var pageModel = new IndexModel();
        pageModel.ModelState.AddModelError("Order.FirstName", "First Name is required");

        // Act
        var result = pageModel.OnPost();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.False(pageModel.ModelState.IsValid);
    }

    [Fact]
    public void OnPost_ValidModelWithMug_ReturnsPageWithValidationError()
    {
        // Arrange
        var pageModel = new IndexModel();
        pageModel.Order = new Order
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Product = "Mug",
            StreetAddress = "123 Main St",
            City = "London",
            PostalCode = "SW1A 1AA",
            AgreeToTerms = true
        };

        // Act
        var result = pageModel.OnPost();

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.False(pageModel.ModelState.IsValid);
        Assert.True(pageModel.ModelState.ContainsKey("Order.Product"));
    }

    [Fact]
    public void OnPost_ValidModelWithOtherProduct_RedirectsToConfirmation()
    {
        // Arrange
        var pageModel = new IndexModel();
        pageModel.Order = new Order
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Product = "T-Shirt",
            StreetAddress = "123 Main St",
            City = "London",
            PostalCode = "SW1A 1AA",
            AgreeToTerms = true
        };

        // Act
        var result = pageModel.OnPost();

        // Assert
        var redirectResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("OrderConfirmation", redirectResult.PageName);
    }
}
