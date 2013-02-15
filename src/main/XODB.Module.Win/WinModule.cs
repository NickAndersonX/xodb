using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using DevExpress.ExpressApp;

namespace XODB.Module.Win
{
    [ToolboxItemFilter("Xaf.Platform.Win")]
    public sealed partial class XODBWindowsFormsModule : ModuleBase
    {
        public XODBWindowsFormsModule()
        {
            InitializeComponent();
        }
    }
}
