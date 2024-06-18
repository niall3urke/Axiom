# Axiom

Axiom is a control library for Winforms applications that helps create modern, user-friendly interfaces inspired by the Bulma CSS framework.

## What is it

Axiom is a library designed to enhance Winforms applications with more modern and visually appealing controls. It aims to provide developers with tools to create interfaces that are both functional and attractive.

## Compatibility

Axiom can be included in Winforms applications built on the .NET Framework.

## Installation

### Step 1: Getting the DLL

1. Download the project from GitHub.
2. Open it in Visual Studio.
3. Build the application.
4. Find the DLL in the binary folder of the project.

### Step 2: Referencing the DLL

To use Axiom in your Winforms project, follow these steps:

1. Open any form in your project.
2. Right-click the Toolbox and select **Choose Items…**.
3. Click **Browse…** and navigate to the built `Axiom.dll` file.
4. Select the `Axiom.dll` file and click **OK**.

You should now see Axiom controls available in your Toolbox, ready to be dragged and dropped onto your forms.

## Usage

Here’s a simple example of how to use an Axiom control:

1. Drag an Axiom button from the Toolbox onto your form.
2. Set its properties in the Properties pane.

```csharp
// Example of setting properties in code
axiomButton.Color = AxColor.Primary;
axiomButton.Rounded = true;
axiomButton.Static = false;
axiomButton.State = AxState.Loading;
```

## Features
Axiom controls come with a variety of properties to customize their appearance and behavior:

| Property	| Description |
| --- | --- |
| Color	| The color of the control. This varies by control, with buttons having the most options. |
| Inverted	| The text color becomes the background color and vice-versa. |
| Light	| An option for a light variant of the default color scheme. |
| Outlined |	Removes the background and leaves the border and foreground text. |
| Rounded |	Adds a generous rounded border to controls. |
| Shape |	Controls the size of the control. Options are Small, Normal, Medium, and Large. |
| Static |	Converts a button to a non-interactive button. |
| State	| Tracks the state of the control (focused, hovered, etc). Setting the state to loading will display a loading symbol for some controls. |

## Inspiration
The style of Axiom controls is inspired by [Bulma.io](https://bulma.io), a fantastic CSS framework for web development. Axiom brings a similar aesthetic and usability to Winforms applications.
