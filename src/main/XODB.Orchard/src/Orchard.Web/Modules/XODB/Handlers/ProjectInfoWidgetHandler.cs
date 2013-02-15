using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using XODB.Models;
using Orchard;

namespace XODB.Handlers
{
    public class ProjectInfoWidgetHandler : ContentHandler
    {
		protected override void BuildDisplayShape(BuildDisplayContext context)
		{
			base.BuildDisplayShape(context);
			
			if (context.ContentItem.ContentType == "ProjectInfoWidget")
			{
                dynamic packageDisplay = context.New.ProjectInfo(
					ProjectCount: 10					
				);

                context.Shape.Zones["Content"].Add(packageDisplay);
			}
		}
    }
}