using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace Axiom.Controls.Select.Designer
{
    public class SelectDesigner : ControlDesigner
    {

        // Fields 

        private DesignerActionListCollection _actionLists;

        // Methods

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.Add(new SelectActionList(Component));
                }

                return _actionLists;
            }
        }


    }
}
