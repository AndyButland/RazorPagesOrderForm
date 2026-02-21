# Guided: Building an Order Form with ASP.NET Core 10 Razor Pages

## Step 0: Introduction and Setup

### Introduction to Razor Pages

**Razor Pages** is a page-based web UI framework built into ASP.NET. It was introduced as a simpler way of building web user interfaces compared to the full MVC pattern, especially for page-focused scenarios.

If you have complex routing needs or are building an API, the MVC pattern will give you more control. Razor Pages is a great fit when you have to build a page-centric UI with straightforward request and response flows. The two patterns can also co-exist in the same project.

Each "page" in Razor Pages actually consists of two files - a Razor template (with a `.cshtml` extension) and a C# class that defines the page's logic. Whereas with MVC you'll be jumping between folders to create and update controllers, models and views, for a Razor Page you just work with these two, co-located files.

Combining presentation and logic in one place still gives you everything you need to build dynamic features, as you'll see in this lab.

### Lab Scenario

In this lab you will build an order form for a fictitious technical conference, where attendees can select the free item of merchandise they prefer.

A basic starter project has been prepared for you to augment with form fields, validation, processing logic and confirmation display.

In the project folder `Pluralsight.OrderForm`, there is an ASP.NET Razor Pages application that you will work on to add these features.

### Working with the Project

Run the following command in the terminal to access the application folder and run it:

```
cd Pluralsight.OrderForm
dotnet run
```

> Whenever you make changes to the code while working on the tasks, you need to stop and re-run the app so your changes take effect. You can do that by pressing `CTRL+C` in the terminal then running `dotnet run` again. If you don't need to see changes, you can also run `dotnet build`, just as a check that the code continues to compile.

Explore the files in the `Pluralsight.OrderForm` folder to see what makes up a Razor Pages application.

- In `Models` you will find `Order.cs`. This is a C# model that will represent the order form. It currently contains a single property. You will add properties to this class to capture and validate the information needed to fulfill the order.
- In `Pages` there is a file `Index.cshtml`. This is the Razor template used to present the order form.  Currently it contains just some boilerplate HTML, which you will be extending to present the form fields.
- Co-located with this file is `Index.cshtml.cs`. This is the associated code file for the page.  You'll see two empty methods: `OnGet()` and `OnPost()`. These run when the page receives a GET request and a POST request, respectively. These methods allow you to customize the behavior of the page when it loads and when a form is submitted.

There are other files necessary for bootstrapping the web application and rendering the pages that you can review. None are essential to understand for the purposes of the lab nor will you be modifying them.

There is also a `Pluralsight.OrderForm.Tests` folder that contains some stub unit tests in the `IndexTests.cs` file. You'll be extending these tests to verify that your changes correctly implement the required business logic.

## Step 1: Define the Order Model

To render and process the order form, you first need a strongly typed representation of the data you'll collect. This is what has been started in the `Order.cs` file. Each piece of data will be defined as a property on this class.

Currently it contains a single `string` property for the user's first name.

Your stakeholders have confirmed that the following information needs to be collected:

- First name
- Last name
- Email address
- Selected merchandise product
- Street address
- City
- Post or zip code
- Comments
- Indication that the attendee has agreed to their data being collected

All information is required other than the comments.

### Task 1.1: Add properties to represent the form data

Augment the `Order` class with the properties necessary to capture this information.  You should use the following property names and types:

- `LastName` as `string`
- `Email` as `string`
- `Product` as `string`
- `StreetAddress` as `string`
- `City` as `string`
- `PostalCode` as `string`
- `Comments` as `string?` (as this property is optional, you use `string?` (a nullable string) to indicate that the value can be null.)
- `AgreeToTerms` as `bool`

---

The model needs to define not just the information you collect from the form. You can also include any details you need to present to the user when they are filling in the form. Most form fields will accept free-text input, but for the product selection you should constrain the user's input to a set of valid choices.

In this scenario, the products available are a fixed set so you can add them directly to the model.

### Task 1.2: Add a static list of available products

Add a new `static` property to the model called `AvailableProducts` which should be of type `List<string>`. This should return a hard-coded collection of the following product names:

- T-Shirt
- Mug
- Baseball Cap
- Hoodie
- Laptop Stickers
- Water Bottle
- Tote Bag

---

You have now added all the necessary properties to the model class. However, you can go further by encoding validation constraints into the model.

You know some properties are required and some are optional. The email address should conform to a valid email format rather than accepting any arbitrary string.

In a real-world application, the collected information would be stored in a database with field size limits. Adding these constraints to the model ensures invalid data is rejected before it reaches storage.

All these constraints can be provided in the form of **data annotations**. Before adding them, verify that `Order.cs` includes a `using System.ComponentModel.DataAnnotations;` directive at the top of the file, as this namespace is required for the validation attributes.

For example, you can ensure the `FirstName` property is required and limited to a maximum of 50 characters by decorating the property like this:

```csharp
[Required]
[StringLength(50)]
public string FirstName { get; set; } = string.Empty;
```

### Task 1.3: Add data annotation attributes for validation

Add data validation attributes to all properties to ensure that they are constrained for valid input. You should add the following:

- `[Required]` to all properties other than `Comments` (which is optional) and `AvailableProducts` which is read-only as it's provided rather than collected information
- `[StringLength(*)]` to all properties that are defined as strings. Replace the `*` with the following maximum lengths, appropriate for each field:
    - `FirstName`, `LastName` - 50
    - `Email` - 100
    - `StreetAddress` - 200
    - `City` - 100
    - `PostalCode` - 20
    - `Comments` - 500
- `[EmailAddress]` to the `Email` field
- `[Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the data collection policy.")]` to the `AgreeToTerms` field. This will constrain the input to only `true`, which makes ticking the agreement box a required action for the form to be accepted.

---

Finally, you can give each property a friendlier display label for the form. When the user is completing the form, you would prefer they see properly presented field names like "First Name" (rather than "FirstName" which is the name of the property in code).

You can apply that with another data annotation attribute: `[Display(Name = "...")]`.

For example, your fully annotated `FirstName` property can look like this:

```csharp
[Required]
[StringLength(50)]
[Display(Name = "First Name")]
public string FirstName { get; set; } = string.Empty;
```

### Task 1.4: Add data annotation attributes for friendlier display

Apply `[Display(Name = "...")]` data annotations to all properties, with the following friendly field names:

- `FirstName` - "First Name"
- `LastName` - "Last Name"
- `Email` - "Email Address"
- `Product` - "Product"
- `StreetAddress` - "Street Address"
- `City` - "City"
- `PostalCode` - "Post/Zip Code"
- `Comments` - "Comments"
- `AgreeToTerms` - "I agree to my data being collected to fulfill this request"

---

That completes the model updates. You will now move on to build the order form itself.

## Step 2: Build the Order Form

By taking the time to define the `Order` model and decorate it with attributes, you can lean on **tag helpers** to handle most of the form rendering. Tag helpers are a Razor Pages feature that let you attach server-side behavior to HTML elements via attributes. Instead of mixing C# code into your markup, you add attributes to standard HTML elements.

For example, you can render an accessible `label` and `input` control inside a `<form>` element with the following markup:

```html
<label asp-for="Order.FirstName" class="form-label"></label>
<input asp-for="Order.FirstName" class="form-control" />
```

The `asp-for` attribute indicates the model property to bind to the element. Providing this binding allows ASP.NET to automatically populate HTML attributes like `value` and `maxlength`, which you can verify by viewing the page source once the form is complete.

### Task 2.1: Bind the Order model to the page

Currently the `Order` model is a standalone class, so you need to add it as a property to the Razor Page. To do this, open `Index.cshtml.cs`. Verify that a `using Pluralsight.OrderForm.Models;` directive is present at the top of the file, then add the following code inside the `IndexModel` class:

```csharp
  [BindProperty]
  public Order Order { get; set; } = new();
```

---

The `[BindProperty]` attribute enables **model binding**. ASP.NET will automatically populate this property with data from the form when it is submitted. With this done, the page now exposes the `Order` model as a bindable property that you can reference in tag helpers.

### Task 2.2: Add text and email input fields using tag helpers

Now add tag helpers for all the text and email input fields to the `Index.cshtml` file, inside the `<form>` element. Each one should use markup similar to the following, which binds the model's `FirstName` property to an `input` control.

```html
  <div class="mb-3">
      <label asp-for="Order.FirstName" class="form-label"></label>
      <input asp-for="Order.FirstName" class="form-control" />
  </div>
```

Place the markup for each field in sequence. All fields will use the same markup, adjusted to reference the appropriate field. For example, the last name field is bound to `Order.LastName`.

For the email address field, add a `type="email"` attribute to the `input` element.

---

The product selection should be presented as a drop-down list rather than a text box, to ensure the user can only select valid products. You'll use a `<select>` element bound to `Order.Product` via the `asp-for` tag helper. A second tag helper, `asp-items`, populates the drop-down with the available products from the model.

### Task 2.3: Add a drop-down list for product selection

Add the following markup to the form to render the product selection:

```html
  <div class="mb-3">
      <label asp-for="Order.Product" class="form-label"></label>
      <select asp-for="Order.Product" class="form-select" asp-items="@(new SelectList(Pluralsight.OrderForm.Models.Order.AvailableProducts.OrderBy(x => x)))">
          <option value="">-- Select a product --</option>
      </select>
  </div>
```

---

There are two more fields you need to add to the form: the free text comments and the agreement for data processing. For these you'll use a `<textarea>` and an `<input type="checkbox">` element respectively, again bound with the model using tag helpers.

### Task 2.4: Add textarea and checkbox fields

Add this markup for the two remaining fields, just above the submit `<button>` element.

```html
  <div class="mb-3">
      <label asp-for="Order.Comments" class="form-label"></label>
      <textarea asp-for="Order.Comments" class="form-control" rows="3"></textarea>
  </div>

  <div class="mb-3 form-check">
      <input asp-for="Order.AgreeToTerms" class="form-check-input" />
      <label asp-for="Order.AgreeToTerms" class="form-check-label"></label>
  </div>
```

---

If you re-run the web application now with `dotnet run` you should see all the form fields presented with friendly and accessible labels.

## Step 3: Handle Form Submission and Validation

With the form presentation complete, you now need to handle what happens when the user submits it. You don't want to blindly accept the input — you need to ensure the validation rules defined on the model are enforced.

ASP.NET provides a robust validation pipeline that works on both the server and the client, and you will implement both.

Why is it necessary to validate both on the server and the client? **Server-side validation** is essential. It runs inside your page handler and is the only validation you can fully trust, since a user can always bypass the browser. **Client-side validation** gives the user a better experience by providing immediate feedback without a round trip to the server.

However, this doesn't mean you need to duplicate effort. The validation works by reading the data annotation attributes you already added to the model, so you get consistent rules in both places without having to maintain logic in two places (or in two languages).

You'll start with server-side validation which requires the wiring up of model binding in your page code file.

### Task 3.1: Implement the POST handler with model state validation

Open the `Index.cshtml.cs` file. First, change the return type of the `OnPost()` method from `void` to `IActionResult`. This interface allows the method to return different types of responses, such as re-rendering the page or redirecting to another URL. Then add the following code inside the method body.

```csharp
  if (!ModelState.IsValid)
  {
      return Page();
  }

  // Process the order here (e.g. save to database).

  return Redirect("/");
```

---

When the form is posted, ASP.NET automatically validates the bound model against its data annotation attributes and records the results in `ModelState`. The code checks `ModelState.IsValid`, and if validation fails, calls `Page()` to re-render the current page, preserving the form data and any validation errors.

It's good practice to add automated tests to verify this logic, which you'll do now.

### Task 3.2: Write a test to verify invalid submissions return the form

Complete a unit test verifying that an invalid form submission returns the user to the page to correct their input. A stub test exists in `IndexTests` called `OnPost_InvalidModel_ReturnsPageWithValidationErrors`.  Replace the existing comments in the body of the test with the following code:

```csharp
  // Arrange
  var pageModel = new IndexModel();
  pageModel.ModelState.AddModelError("Order.FirstName", "First Name is required");

  // Act
  var result = pageModel.OnPost();

  // Assert
  Assert.IsType<PageResult>(result);
  Assert.False(pageModel.ModelState.IsValid);
```

Open a second console window and run the following to verify that the test passes.

```
cd Pluralsight.OrderForm.Tests
dotnet test
```

You should see the following result:

```
Test summary: total: 3, failed: 0, succeeded: 3
```

---

The updates to `Index.cshtml.cs` provide the security you need, but it's not very friendly for the user as they can't currently see which particular piece of information they provided is invalid.

To fix that, you will add tag helpers to the form that surface validation errors to the user.

ASP.NET provides two tag helpers for presenting validation errors, one for presenting an overall summary and one for field-level details.

### Task 3.3: Add validation summary and field-level validation messages

First add the overall summary by adding the following tag helper right at the top of the form, above the fields and just inside the `<form>` element:

```html
<div asp-validation-summary="ModelOnly" class="text-danger"></div>
```

The value `ModelOnly` will prevent the repetition of field level validation messages in the summary. You can set this to `All` if you prefer to see them here too.

Then add field level validation messages to each form field.

You do this by adding an additional `span` element with an `asp-validation-for` attribute. For example, the full markup for the first name field will be:

```html
  <div class="mb-3">
      <label asp-for="Order.FirstName" class="form-label"></label>
      <input asp-for="Order.FirstName" class="form-control" />
      <span asp-validation-for="Order.FirstName" class="text-danger"></span>
  </div>
```

Add this `span` inside the `div` for all fields, adjusting the value of the `asp-validation-for` to match the model property for which you want to display validation errors.

---

Re-run the web application with `dotnet run`. If you submit the form with invalid data, you should see it re-displayed with the appropriate validation messages.

As discussed earlier, client-side validation provides a better experience by surfacing errors immediately. This feature is powered by the jQuery unobtrusive validation library, but for your requirements, you only need to reference the appropriate files.

The default ASP.NET Razor Pages project includes a **partial view** - a reusable fragment of Razor markup - for this: `_ValidationScriptsPartial.cshtml`.

### Task 3.4: Enable client-side validation

To add client-side validation, add the following code to the bottom of the `Index.cshtml` file. The `@section` directive injects content into a named placeholder defined in the layout page:

```html
@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

---

Re-run the application and verify now that the feedback on invalid entries is provided immediately, without a server round trip.

You now have validation in place for required fields, string lengths and agreement to terms. Sometimes you need more complex validation, and you will implement one such rule now.

If one of the products proves particularly popular, the inventory could run out. You'll simulate this by encoding a custom validation rule in the server-side logic.

### Task 3.5: Implement a custom server-side validation rule

To do this, open the `Index.cshtml.cs` file again and add the following code just before the existing `if (!ModelState.IsValid)` line.

```csharp
  if (Order.Product == "Mug")
  {
      ModelState.AddModelError("Order.Product", "Sorry, all mugs have been given away.");
  }
```

---

With this in place, if the "Mug" is selected, an additional model error is added, making the model state invalid. As the error is associated with the `Order.Product` field, it will be displayed next to that field, indicating that the user must choose another item.

As well as testing this manually via `dotnet run`, you can write a unit test to verify the validation rule.

### Task 3.6: Write a test to verify the custom validation rule

Open `IndexTests.cs` and find the `OnPost_ValidModelWithMug_ReturnsPageWithValidationError` test.  Replace the body of the test with the following code:

```csharp
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
```

Run `dotnet test` in the second console window to verify the tests pass.

## Step 4: Provide a Confirmation Page

The final requirement is to display a confirmation page when the user submits a valid order.

### Task 4.1: Create the order confirmation page

Create two new files in the `Pages` folder named `OrderConfirmation.cshtml` and `OrderConfirmation.cshtml.cs`.

Add the following markup and Razor code to `OrderConfirmation.cshtml`:

```html
@page
@model OrderConfirmationModel
@{
    ViewData["Title"] = "Order Confirmation";
}

<div class="text-center">
    <h1 class="display-4">Thank You!</h1>
    <p class="lead">Your order has been submitted successfully.</p>
    <a asp-page="Index" class="btn btn-primary mt-3">Place Another Order</a>
</div>
```

And populate `OrderConfirmation.cshtml.cs` with:

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pluralsight.OrderForm.Pages;

public class OrderConfirmationModel : PageModel
{
    public void OnGet()
    {
    }
}
```

---

Now you need to update the POST handler to redirect to the confirmation page after a valid submission.

You'll do this using a common pattern known as **post/redirect/get**. This three-stage process incorporates a form post, a redirect and a fresh page load. This pattern provides valuable protection against duplicate submissions (a page refresh of the confirmation screen will simply re-display the page; it won't trigger a second form submission).

### Task 4.2: Redirect to the confirmation page on successful submission

Open `Index.cshtml.cs` and replace the last line of the `OnPost()` method with:

```csharp
return RedirectToPage("OrderConfirmation");
```

---

Run the application again and verify that submitting a valid order displays the confirmation page.

Finally, complete the test suite by writing a test for the redirection logic.

### Task 4.3: Write a test to verify successful orders redirect to confirmation

Open `IndexTests.cs` and find the `OnPost_ValidModelWithOtherProduct_RedirectsToConfirmation` test.  Replace the body of the test with the following code:

```csharp
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
```

That completes the code lab. You have built a functional order form using ASP.NET Razor Pages.

You defined a strongly typed model with data annotation attributes to enforce validation rules, used tag helpers to render form controls that are synchronized with the model, and implemented a page handler to process the form submission.

You added both client-side and server-side validation to ensure data integrity, including a custom business rule.

Finally, you applied the post/redirect/get pattern to prevent duplicate submissions and present a clean confirmation page.

These techniques form the foundation for building page-centric form workflows in ASP.NET.


