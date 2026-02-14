# Guided: Building an Order Form with ASP.NET Core 10 Razor Pages

## Concepts

1. Create a Razor Page with a strongly typed form model and implement page handlers to process GET and POST requests for an order submission workflow.
2. Apply Razor Pages model binding, server-side validation attributes, and inline validation messages to ensure submitted order data is accurate and complete.
3. Use Razor form tag helpers—including labels, inputs, selects, and validation helpers—to build a clean, user-friendly order form UI.
4. Implement a post/redirect sequence that processes valid orders and displays a confirmation page, ensuring the form does not resubmit on refresh.

## Audience Profile

Junior developers getting started with ASP.NET or more experienced developers transitioning to working with Razor Pages

## Abstract

In this code lab you’ll learn how to use ASP.NET Razor Pages to build an interactive order form.  You'll represent the form data with a strongly typed model, using data annotations to define validation rules.

You will use tag helpers to render form controls in a consistent and accessible way, and implement page handlers to process GET and POST requests.

Finally, you'll see how to add custom server-side validation logic to handle business rules.

## Description

You have been tasked with completing the implementation of an order form for a technical conference. Attendees who have signed up are able to request a free item of merchandise that will be waiting for them when they arrive.

To do that you will need to address a number of requirements:

- Define a C# model to represent the information collected via the form.
- Decorate the model with data annotation attributes for validation, making fields mandatory and restricting values to sensible sizes.
- Present the form using tag helpers to render appropriate input controls for each field, including text boxes, text areas, checkboxes and drop-down lists.
- Provide client-side and server-side validation presenting any failures clearly to the attendee for correction.
- Implement a custom server-side validation rule, where you will simulate an “out of stock” item.
- Present a confirmation page using the post/redirect/get pattern to prevent accidental duplicate submissions.

The code lab will be set up to start with a customized version of the default “empty” project provided when starting a Razor Pages project with Visual Studio.  This will include basic styling so it looks presentable, and have references to the scripts needed for client-side validation.

It will have a single page with an empty area for the form, and stubs for the page handlers.

We’ll ask the learner to create and decorate the model class, add the markup for the form to the page, and implement the page handlers.

As part of the lab, the user will create the confirmation page.

## Steps

### Step 1: Define the Order Model

#### Task 1.1: Add properties to represent the form data

#### Task 1.2: Add a static list of available products

#### Task 1.3: Add data annotation attributes for validation

### Step 2: Build the Order Form

#### Task 2.1: Add text and email input fields using tag helpers

#### Task 2.2: Add a drop-down list for product selection

#### Task 2.3: Add textarea and checkbox fields

### Step 3: Handle Form Submission and Validation

#### Task 3.1: Bind the Order model to the page

#### Task 3.2: Implement the POST handler with model state validation

#### Task 3.3: Write a test to verify invalid submissions return the form

#### Task 3.4: Add validation summary and field-level validation messages

#### Task 3.5: Enable client-side validation

#### Task 3.6: Implement a custom server-side validation rule

#### Task 3.7: Write a test to verify the custom validation rule

### Step 4: Confirmation Page

#### Task 4.1: Create the order confirmation page

#### Task 4.2: Redirect to the confirmation page on successful submission

#### Task 4.3: Write a test to verify successful orders redirect to confirmation

