using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Axiom.Controls.Select.Designer
{
    public class SelectActionList : DesignerActionList
    {

        // Properties

        [Editor("System.Windows.Forms.Design.StringCollectionEditor", typeof(UITypeEditor))]
        public List<string> Items
        {
            get => _select.Items;
            set => GetPropertyByName("Items").SetValue(_select, value);
        }

        // Fields

        private DesignerActionUIService _service;

        private AxSelect _select;

        // Constructors

        public SelectActionList(IComponent component) : base(component)
        {
            _service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            _select = component as AxSelect;
        }

        // Methods

        private PropertyDescriptor GetPropertyByName(string name)
        {
            var property = TypeDescriptor.GetProperties(_select)[name];

            if (property == null)
            {
                throw new ArgumentException("Property not found");
            }

            return property;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            return new DesignerActionItemCollection
            {
                new DesignerActionPropertyItem("Items", "Items")
            };

            // See this for using the MethodItem approach:
            // https://stackoverflow.com/questions/43399483/display-a-multilinestringeditor-at-design-time-to-edit-the-lines-of-an-edit-cont
        }


    }
}
