using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom.Controls.Label
{
    public enum AxLabelSize
    {
        [Description("Size 1")]
        IsSize1,
        [Description("Size 2")]
        IsSize2,
        [Description("Size 3")]
        IsSize3,
        [Description("Size 4")]
        IsSize4,
        [Description("Size 5")]
        IsSize5,
        [Description("Size 6")]
        IsSize6,
        [Description("Size 7")]
        IsSize7,
    }

    public enum AxFontWeight
    {
        [Description("Normal")]
        Normal,
        [Description("Light")]
        SemiLight,
        [Description("Semi-bold")]
        SemiBold,
        [Description("Bold")]
        Bold
    }
}
