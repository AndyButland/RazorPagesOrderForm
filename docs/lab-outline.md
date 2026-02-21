# Guided: Building an Order Form with ASP.NET Core 10 Razor Pages

## Concepts

1. Create a Razor Page with a strongly typed form model and implement page handlers to process GET and POST requests for an order submission workflow.
2. Apply Razor Pages model binding, server-side validation attributes, and inline validation messages to ensure submitted order data is accurate and complete.
3. Use Razor form tag helpers - including labels, inputs, selects, and validation helpers - to build a clean, user-friendly order form UI.
4. Implement a post/redirect sequence that processes valid orders and displays a confirmation page, ensuring the form does not resubmit on refresh.

## Audience Profile

Junior developers getting started with ASP.NET or more experienced developers transitioning to working with Razor Pages

## Abstract

In this code lab you'll learn how to use ASP.NET Razor Pages to build an interactive order form.  You'll represent the form data with a strongly typed model, using data annotations to define validation rules.

You will use tag helpers to render form controls in a consistent and accessible way, and implement page handlers to process GET and POST requests.

Finally, you'll see how to add custom server-side validation logic to handle business rules.

## Description

You have been tasked with completing the implementation of an order form for a technical conference. Attendees who have signed up are able to request a free item of merchandise that will be waiting for them when they arrive.

To do that you will need to address a number of requirements:

- Define a C# model to represent the information collected via the form.
- Decorate the model with data annotation attributes for validation, making fields mandatory and restricting values to sensible sizes.
- Present the form using tag helpers to render appropriate input controls for each field, including text boxes, text areas, checkboxes and drop-down lists.
- Provide client-side and server-side validation presenting any failures clearly to the attendee for correction.
- Implement a custom server-side validation rule, where you will simulate an "out of stock" item.
- Present a confirmation page using the post/redirect/get pattern to prevent accidental duplicate submissions.

The code lab will be set up to start with a customized version of the default "empty" project provided when starting a Razor Pages project with Visual Studio.  This will include basic styling so it looks presentable, and have references to the scripts needed for client-side validation.

It will have a single page with an empty area for the form, and stubs for the page handlers.

We'll ask the learner to create and decorate the model class, add the markup for the form to the page, and implement the page handlers.

As part of the lab, the user will create the confirmation page.