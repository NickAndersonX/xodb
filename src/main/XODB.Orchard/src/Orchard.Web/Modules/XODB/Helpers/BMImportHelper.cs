using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XODB.Helpers
{
    public static class BMImportHelper
    {

        /// <summary>
        /// Set meta data for domains that are cached during model import
        /// </summary>
        /// <param name="domains"></param>
        /// <param name="blockModelID"></param>
        public static void UpdateDomains(List<Tuple<string,string>> domains, Guid blockModelID) {

            if (domains != null)
            {
                string metaDataType = "Domains";
                string tableType = "X_BlockModel";
                string cont = ObjectHelper.ToJson(domains);
                SetMetaDataItem(blockModelID, metaDataType, tableType, cont);
            }
        }

        public static void UpdateDomains(List<string> domains, Guid blockModelID)
        {
            if (domains != null)
            {
                string metaDataType = "Domains";
                string tableType = "X_BlockModel";
                string cont = ObjectHelper.ToJson(domains);
                SetMetaDataItem(blockModelID, metaDataType, tableType, cont);
            }
        }


        /// <summary>
        /// Add notes into the meta data associated with a model
        /// </summary>
        /// <param name="notes"></param>
        /// <param name="blockModelID"></param>
        internal static void AddModelNotes(string notes, Guid blockModelID)
        {
            if (notes != null)
            {
                string cont = ObjectHelper.ToJson(notes);
                string metaDataType = "Note";
                string tableType = "X_BlockModel";
                SetMetaDataItem(blockModelID, metaDataType, tableType, cont);
            }

        }

        /// <summary>
        /// Set an item of meta data with the given types and cvalues
        /// </summary>
        /// <param name="blockModelID"></param>
        /// <param name="metaDataType"></param>
        /// <param name="tableType"></param>
        /// <param name="cont"></param>
        private static void SetMetaDataItem(Guid blockModelID, string metaDataType, string tableType, string cont)
        {
            try
            {
                var d = new XODB.Models.BMMetaDataDataContext(global::System.Configuration.ConfigurationManager.ConnectionStrings["XODBConnectionString"].ConnectionString);

                XODB.Models.X_MetaData dt = new Models.X_MetaData();
                dt.MetaDataID = Guid.NewGuid();
                dt.MetaDataType = metaDataType;
                dt.ContentToIndex = cont;

                XODB.Models.X_MetaDataRelation rel = new XODB.Models.X_MetaDataRelation();
                rel.MetaDataRelationID = Guid.NewGuid();
                rel.MetaDataID = dt.MetaDataID;
                rel.TableType = tableType;
                rel.ReferenceID = blockModelID;

                d.X_MetaDatas.InsertOnSubmit(dt);
                d.SubmitChanges();
                d.X_MetaDataRelations.InsertOnSubmit(rel);
                d.SubmitChanges();
            }
            catch (Exception ex) { }

        }


        internal static void AddKeyToPrivateData(string guid_bm_stage_lookup_string, Guid? guid_bm_stage_id)
        {            
            //var d = new XODB.Models.InternalsContext(global::System.Configuration.ConfigurationManager.ConnectionStrings["XODBConnectionString"].ConnectionString);
            //Models.PrivateData pd = new Models.PrivateData();
            //pd.PrivateDataID = Guid.NewGuid();
            //pd.UniqueID = guid_bm_stage_lookup_string;
            //pd.UserDataType = "Metadata";
            //pd.Value = guid_bm_stage_id.ToString();
            //pd.SystemDataType = "System.Guid";
            //d.PrivateDatas.InsertOnSubmit(pd);
            //d.SubmitChanges();
        }

        
        internal static void UpdateStage(Guid blockModelGUID, Guid stageMetaID, string stage)
        {
            if (stage != null && stage.Length > 0)
            {
                try
                {
                    var d = new XODB.Models.BMMetaDataDataContext(global::System.Configuration.ConfigurationManager.ConnectionStrings["XODBConnectionString"].ConnectionString);
                    XODB.Models.X_Parameter dt = new Models.X_Parameter();
                    dt.ParameterID = stageMetaID;
                    dt.ParameterName = "Stage";
                    dt.ParameterType = "Metadata";

                    XODB.Models.X_BlockModelMetadata rel = new XODB.Models.X_BlockModelMetadata();
                    rel.BlockModelID = blockModelGUID;
                    rel.BlockModelMetadataID = Guid.NewGuid();
                    rel.ParameterID = stageMetaID;
                    rel.IsColumnData = false;
                    rel.BlockModelMetadataText = stage;

                    d.X_Parameters.InsertOnSubmit(dt);
                    d.SubmitChanges();
                    d.X_BlockModelMetadatas.InsertOnSubmit(rel);
                    d.SubmitChanges();
                }
                catch (Exception ex) { 
                }
            }

        }
    }
}