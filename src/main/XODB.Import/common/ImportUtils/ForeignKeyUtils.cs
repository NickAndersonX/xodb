using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Import.ColumnSpecs;
using XODB.Import.FormatSpecification;

namespace XODB.Import.ImportUtils
{
    public static class ForeignKeyUtils
    {

        public static string FindFKValueInDictionary(string columnValue, ColumnMap cmap, SqlConnection connection, bool genNewFK)
        {

            string fkTable = cmap.fkRelationTable;
            string fkColumnKey = cmap.fkRelationColumn;
            string partA = fkTable.Substring("X_Dictionary".Length);
            string nameLookupColumnPrediction = "Custom" + partA + "Name";
            string stdLookupColumnPrediction = "Standard" + partA + "Name";

            string statement1 = "SELECT " + fkColumnKey + " FROM " + fkTable + " WHERE " + nameLookupColumnPrediction + " = \'" + columnValue + "\'";

            SqlCommand sqc = new SqlCommand(statement1, connection);
            SqlDataReader reader = sqc.ExecuteReader();
            List<string> results = new List<string>();
            while (reader.Read())
            {
                string fkName = reader[0].ToString();
                results.Add(fkName);
            }
            reader.Close();

            if (results.Count == 0 && genNewFK == true)
            {
                // there is no matching entry in this dictionary.  Make a new entry
                Guid gg = Guid.NewGuid();

                string p1 = gg.ToString();
                results.Add(p1);

                char[] splitters = { '-' };
                string[] items = p1.Split(splitters);
                string p2 = "";
                foreach (string it in items)
                {
                    p2 += it;
                }
                //string stdValMock = p2;
                p2 = columnValue + "-" + p2;
                if (p2.Length > 15)
                {
                    p2 = p2.Substring(0, 15);
                }
                string query = "INSERT INTO " + fkTable + " (" + fkColumnKey + "," + stdLookupColumnPrediction + "," + nameLookupColumnPrediction + ") VALUES " +
                                " (\'" + gg.ToString() + "\',\'" + p2 + "\',\'" + columnValue + "\' )";

                SqlCommand sqc2 = new SqlCommand(query, connection);
                sqc2.ExecuteNonQuery();

            }
            string res = null;
            if (results.Count > 0) {
                res = results.First();
            }
            return res;
        }


        public static string FindFKValueInOther(string lookupValue, ColumnMap cmap, SqlConnection connection, bool genNewFK, string lookupColName, Guid XODBProjectID)
        {

            string fkTable = cmap.fkRelationTable;
            string fkColumnKey = cmap.fkRelationColumn;

            if (lookupColName == null)
            {
                lookupColName = cmap.targetColumnName;
            }


            string statement1 = "SELECT " + fkColumnKey + " FROM " + fkTable + " WHERE " + lookupColName + " = \'" + lookupValue + "\' AND ProjectID = \'"+XODBProjectID.ToString()+"\'";

            SqlCommand sqc = new SqlCommand(statement1, connection);
            SqlDataReader reader = sqc.ExecuteReader();
            List<string> results = new List<string>();
            while (reader.Read())
            {
                string fkName = reader[0].ToString();
                results.Add(fkName);
            }
            reader.Close();

            if (results.Count == 0 && genNewFK == true)
            {
            //    // there is no matching entry in this dictionary.  Make a new entry
            //    Guid gg = Guid.NewGuid();

            //    string p1 = gg.ToString();
            //    results.Add(p1);

            //    char[] splitters = { '-' };
            //    string[] items = p1.Split(splitters);
            //    string p2 = "";
            //    foreach (string it in items)
            //    {
            //        p2 += it;
            //    }
            //    string stdValMock = p2;
            //    if (p2.Length > 15)
            //    {
            //        p2 = p2.Substring(0, 15);
            //    }
            //    string query = "INSERT INTO " + fkTable + " (" + fkColumnKey + "," + stdLookupColumnPrediction + "," + nameLookupColumnPrediction + ") VALUES " +
            //                    " (\'" + gg.ToString() + "\',\'" + p2 + "\',\'" + columnValue + "\' )";

            //    SqlCommand sqc2 = new SqlCommand(query, connection);
            //    sqc2.ExecuteNonQuery();

            }
            string res = null;
            if (results.Count > 0)
            {
                res = results.First();
            }
            return res;
        }


        public static List<FKSpecification> QueryForeignKeyRelationships(string connString, string tableToQuery)
        {

            List<FKSpecification> fkList = new List<FKSpecification>();
            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(connString);
                connection.Open();
                string statement1 = "SELECT f.name AS ForeignKey, OBJECT_NAME(f.parent_object_id) AS TableName," +
                                    "COL_NAME(fc.parent_object_id,fc.parent_column_id) AS ColumnName,OBJECT_NAME " +
                                    "(f.referenced_object_id) AS ReferenceTableName, COL_NAME(fc.referenced_object_id," +
                                    "fc.referenced_column_id) AS ReferenceColumnName FROM sys.foreign_keys AS f " +
                                    "INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id " +
                                    "Where OBJECT_NAME(f.parent_object_id) = \'" + tableToQuery + "\';";

                SqlCommand sqc = new SqlCommand(statement1, connection);
                SqlDataReader reader = sqc.ExecuteReader();
                while (reader.Read())
                {

                    string fkName = reader[0].ToString();
                    string TableName = reader[1].ToString();
                    string ColName = reader[2].ToString();
                    string ReferencedTable = reader[3].ToString();
                    string ReferencedCol = reader[4].ToString();
                    FKSpecification fks = new FKSpecification() { parentColumnName = ColName, parentTableName = TableName, childColumnName = ReferencedCol, childTableName = ReferencedTable };
                    fkList.Add(fks);

                }

                connection.Close();

            }
            catch (Exception ex)
            {

            }
            return fkList;
        }


    }
}
