using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using XODB.Models;

namespace XODB.Handlers {
    [UsedImplicitly]
    public class ProjectSettingsPartHandler : ContentHandler {
        public ProjectSettingsPartHandler(IRepository<ProjectSettingsPartRecord> repository) {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<ProjectSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new TemplateFilterForRecord<ProjectSettingsPartRecord>("ProjectSettings", "Parts/XODB.ProjectSettings", "mining"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Mining")));
        }
    }
}