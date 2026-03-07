# Guided: Building an Order Form with ASP.NET Core 10 Razor Pages

## Step 1: Introduction and Setup

### Introduction to Razor Pages

**Razor Pages** is a page-based web UI framework built into ASP.NET. It was introduced as a simpler way of building web user interfaces compared to the full MVC pattern, especially for page-focused scenarios.

If you have complex routing needs or are building an API, the MVC pattern will give you more control. Razor Pages is a great fit when you have to build a page-centric UI with straightforward request and response flows. The two patterns can also co-exist in the same project.

Each "page" in Razor Pages actually consists of two files - a **Razor template** (with a `.cshtml` extension) and a C# class that defines the page's logic. Whereas with MVC you'll be jumping between folders to create and update controllers, models and views, for a Razor Page you just work with these two, co-located files.

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

Click on the **Web Browser** tab and then on the **Open in new browser tab** button. You will then see the web application running.

> Whenever you make changes to the code while working on the tasks, you need to stop and re-run the app so your changes take effect. You can do that by pressing `CTRL+C` in the terminal then running `dotnet run` again. If you don't need to see changes, you can also run `dotnet build`, just as a check that the code continues to compile.

Explore the files in the `Pluralsight.OrderForm` folder to see what makes up a Razor Pages application.

* In the `Models` folder, you will find `Order.cs`. This is a C# model that represents the order form. It currently contains a single property. You will add additional properties to this class to capture and validate the information required to fulfill an order.
* In the `Pages` folder, locate `Index.cshtml`. This Razor template renders the order form. It contains placeholder markup with TODO comments for each form field you will implement.
* In the `Pages` folder, you will also find `Index.cshtml.cs`. This file contains the associated code for the page. It includes two empty methods: `OnGet()` and `OnPost()`. These methods run when the page receives a GET request and a POST request, respectively. You will use them to customize the behavior of the page when it loads and when a form is submitted.

There are other files necessary for bootstrapping the web application and rendering the pages that you can review. None are essential to understand for the purposes of the lab nor will you be modifying them.

## Step 2: Define the Order Model

To render and process the order form, you first need a strongly typed representation of the data you'll collect. This is what has been started in the `Order.cs` file. Each piece of data will be defined as a property on this class.

Currently it contains a single `string` property for the user's first name.

Your stakeholders have confirmed that the following information needs to be collected:

- First name
- Last name
- Email address
- Selected merchandise product
- Address
- Comments
- Indication that the attendee has agreed to their data being collected

All information is required other than the comments.

### Task 2.1: Add properties to represent the form data

Open `Order.cs`.

Augment the `Order` class with the properties necessary to capture this information.

You should use the following property names and types:

- `LastName` as `string`
- `Email` as `string`
- `Product` as `string`
- `Address` as `string`
- `Comments` as `string?`
- `AgreeToTerms` as `bool`

---
Check:

```yaml
rules:
- id: task_2_1_lastname
  pattern-regex: public\s+string\s+LastName\s*\{\s*get\s*;\s*set\s*;\s*\}\s*=\s*(string\.Empty|"");
  message: Add a public property named `LastName` with a `string` type, initialized to an empty string.
  languages: [C#]
  severity: WARNING

- id: task_2_1_email
  pattern-regex: public\s+string\s+Email\s*\{\s*get\s*;\s*set\s*;\s*\}\s*=\s*(string\.Empty|"");
  message: Add a public property named `Email` with a `string` type, initialized to an empty string.
  languages: [C#]
  severity: WARNING

- id: task_2_1_product
  pattern-regex: public\s+string\s+Product\s*\{\s*get\s*;\s*set\s*;\s*\}\s*=\s*(string\.Empty|"");
  message: Add a public property named `Product` with a `string` type, initialized to an empty string.
  languages: [C#]
  severity: WARNING

- id: task_2_1_address
  pattern-regex: public\s+string\s+Address\s*\{\s*get\s*;\s*set\s*;\s*\}\s*=\s*(string\.Empty|"");
  message: Add a public property named `Address` with a `string` type, initialized to an empty string.
  languages: [C#]
  severity: WARNING

- id: task_2_1_comments
  pattern-regex: public\s+string\?\s+Comments\s*\{\s*get\s*;\s*set\s*;\s*\}
  message: Add a public property named `Comments` with a `string?` type.
  languages: [C#]
  severity: WARNING

- id: task_2_1_agreeterms
  pattern-regex: public\s+bool\s+AgreeToTerms\s*\{\s*get\s*;\s*set\s*;\s*\}
  message: Add a public property named `AgreeToTerms` with a `bool` type.
  languages: [C#]
  severity: WARNING
```

---

The model needs to define not just the information you collect from the form. You can also include any details you need to present to the user when they are filling in the form. Most form fields will accept free-text input, but for the product selection you should constrain the user's input to a set of valid choices.

In this scenario, the products available are a fixed set so you can add them directly to the model.

### Task 2.2: Add a static list of available products

In `Order.cs`, add a new `static` property to the model called `AvailableProducts` which should be of type `List<string>`.

This should return a hard-coded collection of the following product names:

- T-Shirt
- Mug
- Baseball Cap
- Hoodie
- Laptop Stickers
- Water Bottle
- Tote Bag

---
Check:

```yaml
rules:
- id: task_2_2_property
  pattern-regex: public\s+static\s+List<string>\s+AvailableProducts
  message: Add a public static property named `AvailableProducts` with a type of `List<string>`.
  languages: [C#]
  severity: WARNING

- id: task_2_2_tshirt
  pattern-regex: '"T-Shirt"'
  message: Add `"T-Shirt"` to the `AvailableProducts` list.
  languages: [C#]
  severity: WARNING

- id: task_2_2_mug
  pattern-regex: '"Mug"'
  message: Add `"Mug"` to the `AvailableProducts` list.
  languages: [C#]
  severity: WARNING

- id: task_2_2_baseballcap
  pattern-regex: '"Baseball Cap"'
  message: Add `"Baseball Cap"` to the `AvailableProducts` list.
  languages: [C#]
  severity: WARNING

- id: task_2_2_hoodie
  pattern-regex: '"Hoodie"'
  message: Add `"Hoodie"` to the `AvailableProducts` list.
  languages: [C#]
  severity: WARNING

- id: task_2_2_laptopstickers
  pattern-regex: '"Laptop Stickers"'
  message: Add `"Laptop Stickers"` to the `AvailableProducts` list.
  languages: [C#]
  severity: WARNING

- id: task_2_2_waterbottle
  pattern-regex: '"Water Bottle"'
  message: Add `"Water Bottle"` to the `AvailableProducts` list.
  languages: [C#]
  severity: WARNING

- id: task_2_2_totebag
  pattern-regex: '"Tote Bag"'
  message: Add `"Tote Bag"` to the `AvailableProducts` list.
  languages: [C#]
  severity: WARNING
```

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

### Task 2.3: Add data annotation attributes for validation

In `Order.cs`, add data validation attributes to all properties to ensure that they are constrained for valid input.

Apply the following attributes:

- `[Required]` to all properties other than `Comments` (which is optional) and `AvailableProducts` which is read-only as it's provided rather than collected information
- `[StringLength(*)]` to all properties that are defined as strings. Replace the `*` with the following maximum lengths, appropriate for each field:
    - `FirstName`, `LastName` - 50
    - `Email` - 100
    - `Address` - 200
    - `Comments` - 500
- `[EmailAddress]` to the `Email` field
- `[Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the data collection policy.")]` to the `AgreeToTerms` field. This will constrain the input to only `true`, which makes ticking the agreement box a required action for the form to be accepted.

---
Check:

```yaml
rules:
- id: task_2_3_required_firstname
  pattern-regex: \[Required\]\s*(\[[^\]]*\]\s*)*public\s+string\s+FirstName
  message: Add a `[Required]` attribute to the `FirstName` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_required_lastname
  pattern-regex: \[Required\]\s*(\[[^\]]*\]\s*)*public\s+string\s+LastName
  message: Add a `[Required]` attribute to the `LastName` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_required_email
  pattern-regex: \[Required\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Email
  message: Add a `[Required]` attribute to the `Email` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_required_product
  pattern-regex: \[Required\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Product
  message: Add a `[Required]` attribute to the `Product` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_required_address
  pattern-regex: \[Required\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Address
  message: Add a `[Required]` attribute to the `Address` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_stringlength_firstname
  pattern-regex: \[StringLength\(50\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+FirstName
  message: Add a `[StringLength(50)]` attribute to the `FirstName` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_stringlength_lastname
  pattern-regex: \[StringLength\(50\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+LastName
  message: Add a `[StringLength(50)]` attribute to the `LastName` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_stringlength_email
  pattern-regex: \[StringLength\(100\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Email
  message: Add a `[StringLength(100)]` attribute to the `Email` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_stringlength_address
  pattern-regex: \[StringLength\(200\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Address
  message: Add a `[StringLength(200)]` attribute to the `Address` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_stringlength_comments
  pattern-regex: \[StringLength\(500\)\]\s*(\[[^\]]*\]\s*)*public\s+string\?\s+Comments
  message: Add a `[StringLength(500)]` attribute to the `Comments` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_emailaddress
  pattern-regex: \[EmailAddress\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Email
  message: Add an `[EmailAddress]` attribute to the `Email` property.
  languages: [C#]
  severity: WARNING

- id: task_2_3_range_agreeterms
  pattern-regex: \[Range\(typeof\(bool\),\s*"true",\s*"true",\s*ErrorMessage\s*=\s*"You must agree to the data collection policy\."\)\]
  message: Add a `[Range]` attribute to the `AgreeToTerms` property that constrains it to true, with the specified error message.
  languages: [C#]
  severity: WARNING
```

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

### Task 2.4: Add data annotation attributes for friendlier display

In `Order.cs`, apply `[Display(Name = "...")]` data annotations to all properties, with the following friendly field names:

- `FirstName` - "First Name"
- `LastName` - "Last Name"
- `Email` - "Email Address"
- `Product` - "Product"
- `Address` - "Address"
- `Comments` - "Comments"
- `AgreeToTerms` - "I agree to my data being collected to fulfill this request"

---
Check:

```yaml
rules:
- id: task_2_4_display_firstname
  pattern-regex: \[Display\(Name\s*=\s*"First Name"\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+FirstName
  message: Add a `[Display(Name = "First Name")]` attribute to the `FirstName` property.
  languages: [C#]
  severity: WARNING

- id: task_2_4_display_lastname
  pattern-regex: \[Display\(Name\s*=\s*"Last Name"\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+LastName
  message: Add a `[Display(Name = "Last Name")]` attribute to the `LastName` property.
  languages: [C#]
  severity: WARNING

- id: task_2_4_display_email
  pattern-regex: \[Display\(Name\s*=\s*"Email Address"\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Email
  message: Add a `[Display(Name = "Email Address")]` attribute to the `Email` property.
  languages: [C#]
  severity: WARNING

- id: task_2_4_display_product
  pattern-regex: \[Display\(Name\s*=\s*"Product"\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Product
  message: Add a `[Display(Name = "Product")]` attribute to the `Product` property.
  languages: [C#]
  severity: WARNING

- id: task_2_4_display_address
  pattern-regex: \[Display\(Name\s*=\s*"Address"\)\]\s*(\[[^\]]*\]\s*)*public\s+string\s+Address
  message: Add a `[Display(Name = "Address")]` attribute to the `Address` property.
  languages: [C#]
  severity: WARNING

- id: task_2_4_display_comments
  pattern-regex: \[Display\(Name\s*=\s*"Comments"\)\]\s*(\[[^\]]*\]\s*)*public\s+string\?\s+Comments
  message: Add a `[Display(Name = "Comments")]` attribute to the `Comments` property.
  languages: [C#]
  severity: WARNING

- id: task_2_4_display_agreeterms
  pattern-regex: \[Display\(Name\s*=\s*"I agree to my data being collected to fulfill this request"\)\]\s*(\[[^\]]*\]\s*)*public\s+bool\s+AgreeToTerms
  message: Add a `[Display]` attribute with the name "I agree to my data being collected to fulfill this request" to the `AgreeToTerms` property.
  languages: [C#]
  severity: WARNING
```

---

That completes the model updates. You will now move on to build the order form itself.

## Step 3: Build the Order Form

By taking the time to define the `Order` model and decorate it with attributes, you can lean on **tag helpers** to handle most of the form rendering. Tag helpers are a Razor Pages feature that let you attach server-side behavior to HTML elements via attributes. Instead of mixing C# code into your markup, you add attributes to standard HTML elements.

For example, you can render an accessible `label` and `input` control inside a `<form>` element with the following markup:

```html
<label asp-for="Order.FirstName" class="form-label"></label>
<input asp-for="Order.FirstName" class="form-control" />
```

The `asp-for` attribute indicates the model property to bind to the element. Providing this binding allows ASP.NET to automatically populate HTML attributes like `value` and `maxlength`, which you can verify by viewing the page source once the form is complete.

### Task 3.1: Bind the Order model to the page

Currently the `Order` model is a standalone class, so you need to add it as a property to the Razor Page.

To do this, open `Index.cshtml.cs`.

Verify that a `using Pluralsight.OrderForm.Models;` directive is present at the top of the file, then add the following code inside the `IndexModel` class:

```csharp
[BindProperty]
public Order Order { get; set; } = new();
```

---
Check:

```yaml
rules:
- id: task_3_1_bindproperty
  pattern-regex: \[BindProperty\]\s*public\s+Order\s+Order\s*\{\s*get\s*;\s*set\s*;\s*\}\s*=\s*new\(\);
  message: Add a `[BindProperty]` attribute and a public `Order` property initialized to a new instance in `IndexModel`.
  languages: [C#]
  severity: WARNING
```

---

The `[BindProperty]` attribute enables **model binding**. ASP.NET will automatically populate this property with data from the form when it is submitted. With this done, the page now exposes the `Order` model as a bindable property that you can reference in tag helpers.

### Task 3.2: Add text and email input fields using tag helpers

Open `Index.cshtml`. Inside the `<form>` element, you will see placeholder `<div>` elements with TODO comments for each form field. Replace the TODO comment in each text field's `<div>` with a `label` and `input` element bound to the appropriate model property using tag helpers.

For example, the first name field should look like this when complete:

```html
  <div class="mb-3">
      <label asp-for="Order.FirstName" class="form-label"></label>
      <input asp-for="Order.FirstName" class="form-control" />
  </div>
```

Do the same for `LastName`, `Email`, and `Address`, adjusting the `asp-for` value to reference the appropriate property.

For the email address field, add a `type="email"` attribute to the `input` element.

---
Check:

```yaml
rules:
- id: task_3_2_input_firstname
  pattern-regex: <input\s[^>]*asp-for\s*=\s*"Order\.FirstName"
  message: Add an `<input>` element bound to `Order.FirstName` using the `asp-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_3_2_input_lastname
  pattern-regex: <input\s[^>]*asp-for\s*=\s*"Order\.LastName"
  message: Add an `<input>` element bound to `Order.LastName` using the `asp-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_3_2_input_email
  pattern-regex: <input\s[^>]*asp-for\s*=\s*"Order\.Email"
  message: Add an `<input>` element bound to `Order.Email` using the `asp-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_3_2_email_type
  pattern-regex: <input\s[^>]*asp-for\s*=\s*"Order\.Email"[^>]*type\s*=\s*"email"
  message: Set the `type` attribute to `"email"` on the `Email` input element.
  languages: [generic]
  severity: WARNING

- id: task_3_2_input_address
  pattern-regex: <input\s[^>]*asp-for\s*=\s*"Order\.Address"
  message: Add an `<input>` element bound to `Order.Address` using the `asp-for` tag helper.
  languages: [generic]
  severity: WARNING
```

---

The product selection should be presented as a drop-down list rather than a text box, to ensure the user can only select valid products. You'll use a `<select>` element bound to `Order.Product` via the `asp-for` tag helper. A second tag helper, `asp-items`, populates the drop-down with the available products from the model.

### Task 3.3: Add a drop-down list for product selection

Open `Index.cshtml`.

Replace the TODO comment inside the product selection `<div>` with the following markup:

```html
  <label asp-for="Order.Product" class="form-label"></label>
  <select asp-for="Order.Product" class="form-select" asp-items="@(new SelectList(Pluralsight.OrderForm.Models.Order.AvailableProducts.OrderBy(x => x)))">
      <option value="">-- Select a product --</option>
  </select>
```

---
Check:

```yaml
rules:
- id: task_3_3_select_product
  pattern-regex: <select\s[^>]*asp-for\s*=\s*"Order\.Product"
  message: Add a `<select>` element bound to `Order.Product` using the `asp-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_3_3_asp_items
  pattern-regex: asp-items\s*=\s*"@\(new SelectList\(Pluralsight\.OrderForm\.Models\.Order\.AvailableProducts
  message: Populate the product `<select>` element with a `SelectList` of `AvailableProducts` using the `asp-items` tag helper.
  languages: [generic]
  severity: WARNING
```

---

There are two more fields you need to add to the form: the free text comments and the agreement for data processing. For these you'll use a `<textarea>` and an `<input type="checkbox">` element respectively, again bound with the model using tag helpers.

### Task 3.4: Add textarea and checkbox fields

Open `Index.cshtml`.

Following the same pattern you used in the previous task, replace the TODO comment inside the comments `<div>` with a `label` and input element bound to `Order.Comments`. For this field, use a `<textarea>` element instead of `<input>`, with `class="form-control"` and a `rows="3"` attribute.

Then replace the TODO comment inside the agree to terms `<div>`. This field uses a checkbox, so add an `<input>` bound to `Order.AgreeToTerms` with `class="form-check-input"`, followed by a `<label>` bound to the same property with `class="form-check-label"`.

---
Check:

```yaml
rules:
- id: task_3_4_textarea_comments
  pattern-regex: <textarea\s[^>]*asp-for\s*=\s*"Order\.Comments"
  message: Add a `<textarea>` element bound to `Order.Comments` using the `asp-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_3_4_input_agreeterms
  pattern-regex: <input\s[^>]*asp-for\s*=\s*"Order\.AgreeToTerms"
  message: Add an `<input>` element bound to `Order.AgreeToTerms` using the `asp-for` tag helper.
  languages: [generic]
  severity: WARNING
```

---

If you re-run the web application now with `dotnet run` you should see all the form fields presented with friendly and accessible labels.

## Step 4: Handle Form Submission and Validation

With the form presentation complete, you now need to handle what happens when the user submits it. You don't want to blindly accept the input — you need to ensure the validation rules defined on the model are enforced.

ASP.NET provides a robust validation pipeline that works on both the server and the client, and you will implement both.

Why is it necessary to validate both on the server and the client? **Server-side validation** is essential. It runs inside your page handler and is the only validation you can fully trust, since a user can always bypass the browser. **Client-side validation** gives the user a better experience by providing immediate feedback without a round trip to the server.

However, this doesn't mean you need to duplicate effort. The validation works by reading the data annotation attributes you already added to the model, so you get consistent rules in both places without having to maintain logic in two places (or in two languages).

You'll start with server-side validation which requires the wiring up of model binding in your page code file.

### Task 4.1: Implement the POST handler with model state validation

Open the `Index.cshtml.cs` file.

First, change the return type of the `OnPost()` method from `void` to `IActionResult`. This interface allows the method to return different types of responses, such as re-rendering the page or redirecting to another URL.

Then add logic inside the method body. Check if `ModelState.IsValid` is false. If it is, return `Page()` to re-render the form so the user can correct their input. If validation passes, return `Redirect("/")` as a temporary placeholder for the success path.

---
Check:

```yaml
rules:
- id: task_4_1_return_type
  pattern-regex: public\s+IActionResult\s+OnPost\s*\(
  message: Change the return type of the `OnPost` method to `IActionResult`.
  languages: [C#]
  severity: WARNING

- id: task_4_1_modelstate_check
  pattern-regex: if\s*\(\s*(!ModelState\.IsValid|ModelState\.IsValid\s*==\s*false|ModelState\.IsValid\s+is\s+false)\s*\)
  message: Add a check for `ModelState.IsValid` inside the `OnPost` method.
  languages: [C#]
  severity: WARNING

- id: task_4_1_return_page
  pattern-regex: return\s+Page\(\)
  message: Return `Page()` when model state validation fails to re-render the form.
  languages: [C#]
  severity: WARNING
```

---

When the form is posted, ASP.NET automatically validates the bound model against its data annotation attributes and records the results in `ModelState`. The code checks `ModelState.IsValid`, and if validation fails, calls `Page()` to re-render the current page, preserving the form data and any validation errors.

The updates to `Index.cshtml.cs` provide the security you need, but it's not very friendly for the user as they can't currently see which particular piece of information they provided is invalid.

To fix that, you will add tag helpers to the form that surface validation errors to the user.

ASP.NET provides two tag helpers for presenting validation errors, one for presenting an overall summary and one for field-level details.

### Task 4.2: Add validation summary and field-level validation messages

Open `Index.cshtml`. Replace the validation summary TODO comment at the top of the form with the following tag helper:

```html
<div asp-validation-summary="ModelOnly" class="text-danger"></div>
```

The value `ModelOnly` will prevent the repetition of field level validation messages in the summary. You can set this to `All` if you prefer to see them here too.

Then add field level validation messages to each form field.

You do this by adding a `span` element with an `asp-validation-for` attribute after the input element inside each field's `<div>`. For example, the full markup for the first name field will be:

```html
<div class="mb-3">
    <label asp-for="Order.FirstName" class="form-label"></label>
    <input asp-for="Order.FirstName" class="form-control" />
    <span asp-validation-for="Order.FirstName" class="text-danger"></span>
</div>
```

Add this `span` inside the `div` for all fields, adjusting the value of the `asp-validation-for` to match the model property for which you want to display validation errors.

---
Check:

```yaml
rules:
- id: task_4_2_validation_summary
  pattern-regex: asp-validation-summary\s*=\s*"ModelOnly"
  message: Add a validation summary element set to `ModelOnly` at the top of the form.
  languages: [generic]
  severity: WARNING

- id: task_4_2_validation_firstname
  pattern-regex: asp-validation-for\s*=\s*"Order\.FirstName"
  message: Add a validation message element for the `FirstName` field using the `asp-validation-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_4_2_validation_lastname
  pattern-regex: asp-validation-for\s*=\s*"Order\.LastName"
  message: Add a validation message element for the `LastName` field using the `asp-validation-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_4_2_validation_email
  pattern-regex: asp-validation-for\s*=\s*"Order\.Email"
  message: Add a validation message element for the `Email` field using the `asp-validation-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_4_2_validation_product
  pattern-regex: asp-validation-for\s*=\s*"Order\.Product"
  message: Add a validation message element for the `Product` field using the `asp-validation-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_4_2_validation_address
  pattern-regex: asp-validation-for\s*=\s*"Order\.Address"
  message: Add a validation message element for the `Address` field using the `asp-validation-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_4_2_validation_comments
  pattern-regex: asp-validation-for\s*=\s*"Order\.Comments"
  message: Add a validation message element for the `Comments` field using the `asp-validation-for` tag helper.
  languages: [generic]
  severity: WARNING

- id: task_4_2_validation_agreeterms
  pattern-regex: asp-validation-for\s*=\s*"Order\.AgreeToTerms"
  message: Add a validation message element for the `AgreeToTerms` field using the `asp-validation-for` tag helper.
  languages: [generic]
  severity: WARNING
```

---

Re-run the web application with `dotnet run`. If you submit the form with invalid data, you should see it re-displayed with the appropriate validation messages.

As discussed earlier, client-side validation provides a better experience by surfacing errors immediately. This feature is powered by the jQuery unobtrusive validation library, but for your requirements, you only need to reference the appropriate files.

The default ASP.NET Razor Pages project includes a **partial view** - a reusable fragment of Razor markup - for this: `_ValidationScriptsPartial.cshtml`.

### Task 4.3: Enable client-side validation

Open `Index.cshtml`. At the bottom of the file, you will see a `@section Scripts` block with a TODO comment. Replace the TODO comment with the following partial view reference:

```html
  <partial name="_ValidationScriptsPartial" />
```

The `@section` directive injects content into a named placeholder defined in the layout page. This partial view includes the jQuery unobtrusive validation scripts.

---
Check:

```yaml
rules:
- id: task_4_3_section_scripts
  pattern-regex: \@section\s+Scripts
  message: Add a `Scripts` section block to the bottom of `Index.cshtml`.
  languages: [generic]
  severity: WARNING

- id: task_4_3_validation_partial
  pattern-regex: <partial\s+name\s*=\s*"_ValidationScriptsPartial"
  message: Include the `_ValidationScriptsPartial` partial view inside the `Scripts` section.
  languages: [generic]
  severity: WARNING
```

---

Re-run the application and verify now that the feedback on invalid entries is provided immediately, without a server round trip.

You now have validation in place for required fields, string lengths and agreement to terms. Sometimes you need more complex validation, and you will implement one such rule now.

If one of the products proves particularly popular, the inventory could run out. You'll simulate this by encoding a custom validation rule in the server-side logic.

### Task 4.4: Implement a custom server-side validation rule

Open the `Index.cshtml.cs` file again.

Just before the existing `if (!ModelState.IsValid)` check, add code that tests whether `Order.Product` equals `"Mug"`. If it does, use `ModelState.AddModelError()` to add an error against the key `"Order.Product"` with the message `"Sorry, all mugs have been given away."`

It's important that this code runs before the `ModelState.IsValid` check. The added error will then cause that check to fail and the form will be re-displayed with the error message.

---
Check:

```yaml
rules:
- id: task_4_4_mug_check
  pattern-regex: Order\.Product\s*==\s*"Mug"
  message: Add a check that compares the selected product to `"Mug"` in the `OnPost` method.
  languages: [C#]
  severity: WARNING

- id: task_4_4_mug_error
  pattern-regex: AddModelError\(\s*"Order\.Product"\s*,\s*"Sorry, all mugs have been given away\."
  message: Add a model error for the `Product` field with the message "Sorry, all mugs have been given away." when Mug is selected.
  languages: [C#]
  severity: WARNING
```

---

With this in place, if the "Mug" is selected, an additional model error is added, making the model state invalid. As the error is associated with the `Order.Product` field, it will be displayed next to that field, indicating that the user must choose another item.

Re-run the application with `dotnet run` and verify that selecting "Mug" and submitting the form displays the error next to the product field.

## Step 5: Provide a Confirmation Page

The final requirement is to display a confirmation page when the user submits a valid order.

### Task 5.1: Create the order confirmation page

Create two new files in the `Pages` folder named `OrderConfirmation.cshtml` and `OrderConfirmation.cshtml.cs`.

Populate `OrderConfirmation.cshtml.cs` with the following code to define the page model:

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

Now build the Razor template in `OrderConfirmation.cshtml` yourself, using what you have learned from working with `Index.cshtml`. The page should:

- Start with the `@page` directive and set the model to `OrderConfirmationModel`
- Set `ViewData["Title"]` to `"Order Confirmation"`
- Display a thank-you heading and a message confirming the order was submitted
- Include a link back to the `Index` page using the `asp-page` tag helper, so the user can place another order

---
Checks:

```yaml
rules:
- id: task_5_1_cshtml_page_directive
  pattern-regex: \@page
  message: Add the `@page` directive to the top of `OrderConfirmation.cshtml`.
  languages: [generic]
  severity: WARNING

- id: task_5_1_cshtml_model
  pattern-regex: \@model\s+OrderConfirmationModel
  message: Set the model to `OrderConfirmationModel` in `OrderConfirmation.cshtml`.
  languages: [generic]
  severity: WARNING
```

```yaml
rules:
- id: task_5_1_cs_class
  pattern-regex: public\s+class\s+OrderConfirmationModel\s*:\s*PageModel
  message: Create an `OrderConfirmationModel` class that inherits from `PageModel`.
  languages: [C#]
  severity: WARNING
```

---

Now you need to update the POST handler to redirect to the confirmation page after a valid submission.

You'll do this using a common pattern known as **post/redirect/get**. This three-stage process incorporates a form post, a redirect and a fresh page load. This pattern provides valuable protection against duplicate submissions (a page refresh of the confirmation screen will simply re-display the page; it won't trigger a second form submission).

### Task 5.2: Redirect to the confirmation page on successful submission

Open `Index.cshtml.cs` and replace the last line of the `OnPost()` method with:

```csharp
return RedirectToPage("OrderConfirmation");
```

---
Check:

```yaml
rules:
- id: task_5_2_redirect
  pattern-regex: return\s+RedirectToPage\(\s*"OrderConfirmation"\s*\)
  message: Return a redirect to the `OrderConfirmation` page at the end of the `OnPost` method.
  languages: [C#]
  severity: WARNING
```

---

Run the application again and verify that submitting a valid order displays the confirmation page.

# Step 6: Summing Up

That completes the code lab. You have built a functional order form using ASP.NET Razor Pages.

You defined a strongly typed model with data annotation attributes to enforce validation rules, used tag helpers to render form controls that are synchronized with the model, and implemented a page handler to process the form submission.

You added both client-side and server-side validation to ensure data integrity, including a custom business rule.

Finally, you applied the post/redirect/get pattern to prevent duplicate submissions and present a clean confirmation page.

These techniques form the foundation for building page-centric form workflows in ASP.NET.


-----------------------------------

*** REMOVED CONTENT ***

The following test-related tasks and copy were removed because unit tests don't run well in the lab environment. The test project code remains in the repository.

---

### Removed from Step 1 (after the file exploration list):

There is also a `Pluralsight.OrderForm.Tests` folder that contains some stub unit tests in the `IndexTests.cs` file. You'll be extending these tests to verify that your changes correctly implement the required business logic.

---

### Removed from Step 4 (was between Task 4.1 and the validation summary task):

It's good practice to add automated tests to verify this logic, which you'll do now.

### Task 4.2: Write a test to verify invalid submissions return the form

Complete a unit test verifying that an invalid form submission returns the user to the page to correct their input. A stub test exists in `IndexTests` called `OnPost_InvalidModel_ReturnsPageWithValidationErrors`.

Replace the existing comments in the body of the test with the following code:

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

To run the tests you will first need to stop the running web application by pressing `CTRL+C` in your terminal window.

Open a second terminal window and run the following to verify that the test passes.

```
cd Pluralsight.OrderForm.Tests
dotnet test
```

You should see the following result:

```
Test summary: total: 3, failed: 0, succeeded: 3
```

---
Check:

```yaml
rules:
- id: task_4_2_addmodelerror
  pattern-regex: AddModelError\(\s*"Order\.FirstName"\s*,\s*"First Name is required"\s*\)
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING

- id: task_4_2_assert_pagetype
  pattern-regex: Assert\.IsType<PageResult>\(\s*result\s*\)
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING

- id: task_4_2_assert_invalid
  pattern-regex: Assert\.False\(\s*pageModel\.ModelState\.IsValid\s*\)
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING
```

---

### Removed from Step 4 (was after Task 4.5, the Mug rule):

As well as testing this manually via `dotnet run`, you can write a unit test to verify the validation rule.

### Task 4.6: Write a test to verify the custom validation rule

Open `IndexTests.cs` and find the `OnPost_ValidModelWithMug_ReturnsPageWithValidationError` test.

Replace the body of the test with the following code:

```csharp
  // Arrange
  var pageModel = new IndexModel();
  pageModel.Order = new Order
  {
      FirstName = "John",
      LastName = "Doe",
      Email = "john@example.com",
      Product = "Mug",
      Address = "123 Main St",
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

---
Check:

```yaml
rules:
- id: task_4_6_product_mug
  pattern-regex: Product\s*=\s*"Mug"
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING

- id: task_4_6_assert_page
  pattern-regex: Assert\.IsType<PageResult>\(\s*result\s*\)
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING

- id: task_4_6_assert_invalid
  pattern-regex: Assert\.False\(\s*pageModel\.ModelState\.IsValid\s*\)
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING

- id: task_4_6_assert_containskey
  pattern-regex: Assert\.True\(\s*pageModel\.ModelState\.ContainsKey\(\s*"Order\.Product"\s*\)\s*\)
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING
```

---

### Removed from Step 5 (was after Task 5.2):

Finally, complete the test suite by writing a test for the redirection logic.

### Task 5.3: Write a test to verify successful orders redirect to confirmation

Open `IndexTests.cs` and find the `OnPost_ValidModelWithOtherProduct_RedirectsToConfirmation` test.

Replace the body of the test with the following code:

```csharp
  // Arrange
  var pageModel = new IndexModel();
  pageModel.Order = new Order
  {
      FirstName = "John",
      LastName = "Doe",
      Email = "john@example.com",
      Product = "T-Shirt",
      Address = "123 Main St",
      AgreeToTerms = true
  };

  // Act
  var result = pageModel.OnPost();

  // Assert
  var redirectResult = Assert.IsType<RedirectToPageResult>(result);
  Assert.Equal("OrderConfirmation", redirectResult.PageName);
```

---
Check:

```yaml
rules:
- id: task_5_3_assert_redirect_type
  pattern-regex: Assert\.IsType<RedirectToPageResult>\(\s*result\s*\)
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING

- id: task_5_3_assert_pagename
  pattern-regex: Assert\.Equal\(\s*"OrderConfirmation"\s*,\s*redirectResult\.PageName\s*\)
  message: Semgrep found a match
  languages: [C#]
  severity: WARNING
```

---
