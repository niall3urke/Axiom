# Axiom

Axiom is a control library for Winforms applications that helps create modern, user-friendly interfaces inspired by the Bulma CSS framework.

## What is it

Axiom is a library designed to enhance Winforms applications with more modern and visually appealing controls. It aims to provide developers with tools to create interfaces that are both functional and attractive.

It allows you to take your controls from this: 

![axiom button default winforms](https://github.com/niall3urke/Axiom/assets/11950726/259ee839-7f09-4acb-a95d-62c0e4ed6634) 

To this: 

![axiom button example](https://github.com/niall3urke/Axiom/assets/11950726/de237eb3-d48f-4ac9-a442-6e178e8a9c36) 

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

## Controls
Axiom contains the following types of control:
| Control | Description |
| --- | --- |
| AxButton | The classic button, in different colors, sizes and states |
| AxCheckbox | A checkbox control with properties similar to that of the button
| AxSwitch | A variation on the checkbox with no Winforms native equivalent |
| AxRadioButton | A radio button with colors, sizes and styles | 
| AxImage | A picture box control with added facilities for rounded borders, and set aspect ratios |
| AxInput | A textbox with placeholder, colors, sizes, styles, states etc |
| AxSelect | A combobox with colors, sizes, styles and an animated dropdown arrow |
| AxBox | A panel with colors, styles, rounded borders and shadows |
| AxHoverableBox | Similar to AxBox, but has an animated hover-effect |

## Overview
Here's a quick overview of the main properties for buttons. Many of Axiom's controls share these properties, so they're a good place to start.

### Colors
Choose from plain colors:

![axoim button colors-plain](https://github.com/niall3urke/Axiom/assets/11950726/047cd744-48d4-4c78-9696-031c23b16ba4)

Opt for bright eye-catching colors:

![axiom button colors-normal](https://github.com/niall3urke/Axiom/assets/11950726/39b9fa21-5005-4419-8e25-45ee32af65ba)

Or, toggle the **IsLight** property for the light version:

![axiom buttoncolors-light](https://github.com/niall3urke/Axiom/assets/11950726/4361d986-2970-4734-b6ed-ff762e87600e)

### Sizes
There are four main sizes of control in Axiom:
- Small
- Normal
- Medium
- Large

Sizes are set using the **Shape** property.

![axoim button sizes](https://github.com/niall3urke/Axiom/assets/11950726/dc6b32bd-4893-4716-844c-fdd6cee82522)


### Styles

#### Outlined
You can create a lighter-weight button with **IsOutlined**:

![axiom button outlined](https://github.com/niall3urke/Axiom/assets/11950726/7ba656d7-cec7-4c00-82a7-206de7b1c7e1)

#### Inverted
Or invert the foreground and background colors with  **IsInverted**:

![axiom button inverted](https://github.com/niall3urke/Axiom/assets/11950726/5917062d-f228-4941-b32e-9324643e2cee)

#### Rounded
You can even create pill-style buttons with **IsRounded**:

![axoim button rounded](https://github.com/niall3urke/Axiom/assets/11950726/88e19922-a67e-486e-ace1-1e294cb73bc9)

You can also combine these properties at the same time. 

### States
The state of a control affects its style.

#### Normal
![axiom button state-normal](https://github.com/niall3urke/Axiom/assets/11950726/27f96f8f-23f7-45d1-93b9-bfaf65333d39)


#### Hover
![axiom button state-hover](https://github.com/niall3urke/Axiom/assets/11950726/b78c802c-b547-4c40-a1ea-4d0e012f71eb)


#### Focus
![axiom button state-focus](https://github.com/niall3urke/Axiom/assets/11950726/7927d98d-2fa1-4db9-afe2-15c8ff204584)


#### Active
![axiom button state-active](https://github.com/niall3urke/Axiom/assets/11950726/b6dce44d-f9b8-4c95-a014-38155bfa1855)


#### Loading
![axiom buttons loading](https://github.com/niall3urke/Axiom/assets/11950726/98389dfa-fcff-4d19-98e5-7a136ddd4769)


#### Disabled
![axiom button state-disabled](https://github.com/niall3urke/Axiom/assets/11950726/afdac4f1-0232-41a2-983f-45c02b76d7cc)


## Inspiration
The style of Axiom controls is inspired by [Bulma.io](https://bulma.io), a fantastic CSS framework for web development. Axiom brings a similar aesthetic and usability to Winforms applications.
