using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Import.DataModels;
using Xstract.Import.LAS;

namespace XODB.Import.ImportUtils
{
    public class LasImportUtils
    {
        public LasImportUtils() { }


        internal void ImportLASFile(Xstract.Import.LAS.LASFile lasFile, string origFilename, ModelImportStatus mos, Guid currentProject, Action<string, double> UpdateStatus)
        {
            int li = origFilename.LastIndexOf("\\");
            string tempHoleIDa = origFilename.Substring(li);
            li = tempHoleIDa.LastIndexOf(".");
            string tempHoleID = tempHoleIDa.Substring(1, li - 1);
            int rowCounter = 0;
            try
            {
                // here we need to create the X_Geophyiscs data row item
                XODBImportCollarEntities entityObj = new XODBImportCollarEntities();
                entityObj.Configuration.AutoDetectChangesEnabled = false;

                X_Geophysics xG = new X_Geophysics();
                xG.FileName = origFilename;
                Guid gg = Guid.NewGuid();
                xG.GeophysicsID = gg;

                Guid holeGuid = CollarQueries.FindHeaderGuid(tempHoleID, currentProject);
                Guid resHole = new Guid();
                if (!holeGuid.ToString().Equals(resHole.ToString()))
                {
                    xG.HeaderID = holeGuid;
                }
                Guid unitGuid = new Guid("2395DE56-8F6F-4B0C-806C-DD2606B9902B");
                UnitQueries uq = new UnitQueries();
                X_DictionaryUnit xu = uq.FindUnits("m");
                if (xu != null)
                {
                    unitGuid = xu.UnitID;
                }
                xG.DimensionUnitID = unitGuid;

                entityObj.X_Geophysics.Add(xG);


                Dictionary<string, Guid> metaDataIDLookup = new Dictionary<string, Guid>();
                // here we need to add a X_GeophysicsMetadata item for each column

                foreach (string s in lasFile.columnHeaders)
                {
                    X_Geophysics_Parameter xp = null;
                    xp = GetParameterIDFor(entityObj, "LAS data column", s);
                    if (xp == null)
                    {
                        xp = new X_Geophysics_Parameter();
                        Guid pg = Guid.NewGuid();
                        xp.ParameterID = pg;
                        xp.ParameterType = "LAS data column";
                        xp.ParameterName = s;

                        // TODO, add the unit as per the las file if present
                        entityObj.X_Geophysics_Parameter.Add(xp);
                    }
                    X_GeophysicsMetadata xgm = new X_GeophysicsMetadata();
                    xgm.GeophysicsID = gg;
                    Guid gmid = Guid.NewGuid();
                    xgm.GeophysicsMetadataID = gmid;
                    xgm.Mnemonic = s;
                    xgm.ParameterID = xp.ParameterID;
                    entityObj.X_GeophysicsMetadata.Add(xgm);
                    metaDataIDLookup.Add(s, gmid);
                }
                entityObj.SaveChanges();
                
                /// here we add the row data, one entry per depth & value into the X_GeophysicsData
                foreach (LASDataRow ldr in lasFile.dataRows)
                {
                    double depth = ldr.depth;

                    for (int i = 0; i < ldr.rowData.Count(); i++)
                    {
                        X_GeophysicsData xd1 = new X_GeophysicsData();
                        string s = lasFile.columnHeaders[i];
                        xd1.GeophysicsDataID = Guid.NewGuid();
                        Guid g = new Guid();
                        bool found = metaDataIDLookup.TryGetValue(s, out g);
                        if (found)
                        {
                            xd1.GeophysicsMetadataID = g;
                        }
                        xd1.Dimension = (decimal)depth;
                        xd1.MeasurementValue = (decimal)ldr.rowData[i];
                        entityObj.X_GeophysicsData.Add(xd1);
                    }
                    entityObj.SaveChanges();
                    rowCounter++;
                }
            }
            catch (Exception ex) {
                mos.errorMessages.Add("Failed to complete import of LAS file: " + origFilename);
            }
            mos.recordsAdded = rowCounter;
            
        }

        private X_Geophysics_Parameter GetParameterIDFor(XODBImportCollarEntities entityObj, string paramType, string paramName)
        {
            X_Geophysics_Parameter res = null; ;
            IQueryable<X_Geophysics_Parameter> resGP = entityObj.X_Geophysics_Parameter.Where(c => c.ParameterType.Trim().Equals(paramType) && c.ParameterName.Trim().Equals(paramName));
            foreach (X_Geophysics_Parameter xx in resGP)
            {
                res = xx;
                break;
            }
            return res;
        }
    }
}
