﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data.Linq.Mapping;
using System.Threading;
using System.Data;
using System.Data.Sql;
using System.Data.EntityClient;
using System.Data.Common;
using System.Data.SqlClient;

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

        private static int? _defaultTimeout = null;
        public static int DefaultTimeout
        {
            get
            {
                if (_defaultTimeout == null)
                {
                    try
                    {
                        Monitor.Enter(typeof(DBHelper));
                        int timeout;
                        if (!int.TryParse(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"], out timeout))
                            timeout = 600;   //10 Minutes 
                        _defaultTimeout = timeout;
                    }
                    catch
                    {
                        _defaultTimeout = 600;
                    }
                    finally
                    {
                        Monitor.Exit(typeof(DBHelper));
                    }
                   
                }
                return _defaultTimeout.Value;
            }
        }

        public static string GetEnityConnectionString(string providerConnectionString)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder scsb = new System.Data.SqlClient.SqlConnectionStringBuilder(providerConnectionString);
            EntityConnectionStringBuilder ecb = new EntityConnectionStringBuilder();
            ecb.Metadata = "res://*/";
            ecb.Provider = "System.Data.SqlClient";
            ecb.ProviderConnectionString = scsb.ConnectionString;
            return ecb.ConnectionString;
        }
    }
}