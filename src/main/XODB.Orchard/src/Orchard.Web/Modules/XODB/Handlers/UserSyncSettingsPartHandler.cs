using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using XODB.Models;

namespace XODB.Handlers {
    [UsedImplicitly]
    public class UserSyncSettingsPartHandler : ContentHandler
    {
        public UserSyncSettingsPartHandler(
            IRepository<UserSyncSettingsPartRecord> repository)
        {
            Filters.Add(new ActivatingFilter<UserSyncSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));
        }
    }
}