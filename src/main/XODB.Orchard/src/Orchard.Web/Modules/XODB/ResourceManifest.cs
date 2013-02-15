using Orchard.UI.Resources;

namespace XODB {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            builder.Add().DefineStyle("XODBAdmin").SetUrl("xodb-projects-admin.css");
        }
    }
}
