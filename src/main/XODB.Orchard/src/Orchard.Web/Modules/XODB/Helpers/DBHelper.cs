using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data.Linq.Mapping;

namespace XODB.Helpers
{
    public class DBHelper
    {
        public static PropertyInfo GetPrimaryKey<T>()
        {
            PropertyInfo[] infos = typeof(T).GetProperties();
            foreach (PropertyInfo info in infos)
            {
                var column = info.GetCustomAttributes(false)
                 .Where(x => x.GetType() == typeof(ColumnAttribute))
                 .FirstOrDefault(x =>
                  ((ColumnAttribute)x).IsPrimaryKey &&
                  ((ColumnAttribute)x).DbType.Contains("NOT NULL"));
                if (column != null)
                {
                    return info;
                }
            }
            return null;
        }
    }
}