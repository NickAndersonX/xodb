using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using XODB.Models;

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

        public int UpdateFrom1()
        {
            SchemaBuilder.CreateTable(typeof(EmailPartRecord).Name, table => table
                .ContentPartRecord()
                .Column("Recipients", DbType.String, c => c.WithLength(4000))
                .Column("Subject", DbType.String)
                .Column("Body", DbType.String, c => c.WithLength(4000))
                .Column("Retry", DbType.Boolean)
                .Column("Processed", DbType.DateTime)
                .Column("Completed", DbType.DateTime)
                );


            ContentDefinitionManager.AlterTypeDefinition("Email",
                cfg => cfg
                    .WithPart("EmailPart")  
                    //.Creatable(true)
                    );

            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.CreateTable(typeof(BlockModelPartRecord).Name, table => table
                .ContentPartRecord()
                .Column("BmFileName", DbType.String)
                .Column("FormatFileName", DbType.String)
                .Column("ProjectID", DbType.Guid)
                .Column("ProjectName", DbType.String)
                .Column("Alias", DbType.String)
                .Column("UserID", DbType.Guid)
                .Column("Notes", DbType.String, c => c.WithLength(4000))
                .Column("Stage", DbType.String)
                .Column("StageMetaID", DbType.Guid)
                .Column("BmGuid", DbType.Guid)
                .Column("ColumnNameToAdd", DbType.String)
                .Column("ColumnIndexToAdd", DbType.Int32)
                .Column("Emails", DbType.String, c => c.WithLength(4000))
                .Column("Processed", DbType.DateTime)
                .Column("Completed", DbType.DateTime)
                );


            ContentDefinitionManager.AlterTypeDefinition("BlockModel",
                cfg => cfg
                    .WithPart("BlockModelPart")  
                    );

            return 3;
               
        }
    }
}