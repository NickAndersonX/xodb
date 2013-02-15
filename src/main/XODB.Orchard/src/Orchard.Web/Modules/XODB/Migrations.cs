using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace XODB {
    public class Migrations : DataMigrationImpl {

        public int Create() {
			// Creating table OwnerGuidPartRecord
			SchemaBuilder.CreateTable("OwnerGuidPartRecord", table => table
				.ContentPartRecord()
				.Column("OwnerGuid", DbType.Guid)
			);

			// Creating table UserSyncSettingsPartRecord
			SchemaBuilder.CreateTable("UserSyncSettingsPartRecord", table => table
				.ContentPartRecord()
				.Column("NextUserSyncUtc", DbType.DateTime)
			);

			// Creating table ProjectPartRecord
			SchemaBuilder.CreateTable("ProjectPartRecord", table => table
				.ContentPartRecord()
			);

			// Creating table ProjectSettingsPartRecord
			SchemaBuilder.CreateTable("ProjectSettingsPartRecord", table => table
				.ContentPartRecord()
				.Column("UploadAllowedFileTypeWhitelist", DbType.String)
			);

            ContentDefinitionManager.AlterTypeDefinition("ProjectInfoWidget", cfg => cfg
               .WithPart("WidgetPart")
               .WithPart("CommonPart")
               .WithSetting("Stereotype", "Widget"));

            return 1;
        }
    }
}