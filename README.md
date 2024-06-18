Axiom is a control library for Winforms applications.

### What is it
Axiom is a library for Winforms applications to help create more modern, user-friendly interfaces. 

### Compatibility
Axiom can be included in Winforms applications built on .NET Framework. 

### Step 1: Getting the DLL
Download the project, open it in Visual Studio and build the application. The DLL in the binary folder can then be referenced in your own Winforms projects.

### Step 2: Referencing the DLL
To use Axiom you need to include it in your Toolbox. To do so, open any form in your project and:
 
1. Right-click the Toolbox > Choose Items… > Browse… 
2. Navigate to the built Axiom.dll file and select it
3. Click OK to close the dialog. 

At this point you should be able to drag and drop Axiom controls onto the form.

### Features
Most Axiom controls have access to the following properties, most are visible in the Properties pane while some, like State, aren’t.

| Property | Description |
| --- | --- |
| Color | The color of the control. This varies by control, with buttons having the most options. |
| Inverted | The text color becomes the background color and vice-versa |
| Light | An option for a light variant of the default color scheme |
| Outlined | Removes the background and leaves the border and foreground text |
| Rounded | Adds a generous rounded border to controls |
| Shape | This controls the size of the control. Options are Small, Normal, Medium and Large |
| Static | Converts a button to a non-interactive button |
| State | Used to track the state of the control (focused, hovered, etc). Setting the state to loading will display a loading symbol for some controls. |

### Bulma.io
The style of Axiom controls are inspired by [Bulma.io](https://bulma.io/), which is a great CSS framework for your web work. 
