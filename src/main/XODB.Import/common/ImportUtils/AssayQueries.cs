using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Import.DataModels;

namespace XODB.Import.ImportUtils
{

    /// <summary>
    /// Helper class for assay importing.
    /// </summary>
    public class AssayQueries
    {

        public DataModels.XODBImportAssayEntities entityObj { get; set; }

        Dictionary<Guid, IQueryable<X_AssayGroupTestResult>> resultsCache1 = new Dictionary<Guid, IQueryable<X_AssayGroupTestResult>>();
        Dictionary<Guid, IQueryable<X_AssayGroupTest>> resultsCache2 = new Dictionary<Guid, IQueryable<X_AssayGroupTest>>();

        internal void SetEntityObject(DataModels.XODBImportAssayEntities _entityObj)
        {
            entityObj = _entityObj;
        }


        internal List<X_Sample> CheckForDuplicate(Guid holeID, decimal fromDepth, decimal toDepth)
        {

            List<X_Sample> resultList = new List<X_Sample>();
            bool found = false;
            IQueryable<X_Sample> res = entityObj.X_Sample.Where(c => c.HeaderID == holeID && c.FromDepth == fromDepth && c.ToDepth == toDepth);
            if (res != null && res.Count() > 0)
            {
                found = true;                
            }
            foreach (X_Sample xx in res)
            {
                found = true;
                resultList.Add(xx);
                break;
            }
            return resultList;
        }

        internal List<X_AssayGroupTestResult> GetDuplicateResult(Dictionary<Guid, X_AssayGroupTest> assayGroups, Guid sampleID, Guid assayGroupTestID)
        {
            List<X_AssayGroupTestResult> resultData = new List<X_AssayGroupTestResult>();
            
            X_AssayGroupTest xx =  assayGroups[assayGroupTestID];
            string testName = xx.AssayTestName;
            Guid testParamID = (Guid)xx.ParameterID;

                
            IQueryable<X_AssayGroupTestResult> res = entityObj.X_AssayGroupTestResult.Where(c => c.SampleID == sampleID);
            bool foundDupe = false;
            foreach (X_AssayGroupTestResult xx2 in res)
            {
                
                IQueryable<X_AssayGroupTest> res2 = entityObj.X_AssayGroupTest.Where(c => c.AssayGroupID == xx2.AssayGroupTestID);
                foreach(X_AssayGroupTest agt in res2){                   
                    if (agt.ParameterID == testParamID) {
                        foundDupe = true;
                        break;
                    }
                }
                if (foundDupe)
                {
                    resultData.Add(xx2);
                    break;
                } 
               
            }
            return resultData;
        }

        internal List<X_AssayGroupTestResult> GetDuplicateResult(Guid sampleID, string columnName)
        {
            List<X_AssayGroupTestResult> resultData = new List<X_AssayGroupTestResult>();
            bool foundDupe = false;
            IQueryable<X_AssayGroupTestResult> res = null;
            if (resultsCache1.ContainsKey(sampleID))
            {
                res = resultsCache1[sampleID];
            }
            else{
                res = entityObj.X_AssayGroupTestResult.Where(c => c.SampleID == sampleID);
                resultsCache1.Add(sampleID, res);
            }

            

            foreach (X_AssayGroupTestResult xx2 in res)
            {
                Guid assayGroupTestOfSample = xx2.AssayGroupTestID;
                // now query the assay groups tests for this sample
                IQueryable<X_AssayGroupTest> res2 = null;
                if (resultsCache2.ContainsKey(assayGroupTestOfSample))
                {
                    res2 = resultsCache2[assayGroupTestOfSample];
                }
                else
                {
                    res2 = entityObj.X_AssayGroupTest.Where(c => c.AssayGroupTestID == assayGroupTestOfSample);
                    resultsCache2.Add(assayGroupTestOfSample, res2);
                }
                foreach (X_AssayGroupTest agt in res2)
                {
                        
                    // these are teh assay test groups
                    if (agt.AssayTestName.Trim().Equals(columnName))
                    {
                        foundDupe = true;
                        break;
                    }
                }
                    
                if (foundDupe)
                {
                    resultData.Add(xx2);
                    break;
                }

            }
            
            return resultData;
        }
    }


}
