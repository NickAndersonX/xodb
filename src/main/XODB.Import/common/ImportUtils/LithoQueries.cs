﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Import.DataModels;

namespace XODB.Import.ImportUtils
{
    public class LithoQueries
    {
        public XODBImportCollarEntities entityObj { get; set; }


        internal void SetEntityObject(XODBImportCollarEntities _entityObj)
        {
            entityObj = _entityObj;
        }


        //internal List<Guid> CheckForDuplicate(Guid holeID, decimal depth, XODBImportCollarEntities eo)
        //{

        //    List<Guid> results = new List<Guid>();
            
        //    IQueryable<X_Survey> res = eo.X_Survey.Where(c => c.HeaderID == holeID && c.Depth == depth);
        //    foreach (X_Survey xs in res) {

                
        //        results.Add(xs.SurveyID);
        //    }

        //    return results;
        //}

        internal List<Guid> CheckForDuplicate(Guid holeID, decimal dtFrom, decimal dtTo, System.Data.SqlClient.SqlConnection secondaryConnection)
        {
            string statement1 = "SELECT LithologyID FROM X_Lithology WHERE HeaderID=\'" + holeID.ToString() + "\' AND FromDepth = " + dtFrom + " AND ToDepth=" + dtTo + ";";
            
            SqlCommand sqc = new SqlCommand(statement1, secondaryConnection);
            SqlDataReader reader = sqc.ExecuteReader();
            List<Guid> results = new List<Guid>();
           
            while (reader.Read())
            {
                string fkName = reader[0].ToString();
                results.Add(new Guid(fkName));
                
                
            }
            reader.Close();
            sqc.Dispose();
            return results;
        }
    }
}
