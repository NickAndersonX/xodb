using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Security.Cryptography;

namespace XODB.Helpers
{
    public static class ObjectHelper
    {
        public static string CleanTokenForSQL(this string o)
        {
            if (o == null)
                return null;
            o = o.Trim();
            return o.Split(new string[] { " ", "\t", "\r", "\n", "'" }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();            
        }

        public static byte[] ComputeHashBytes(this object o)
        {

            SHA1 sha = new SHA1CryptoServiceProvider();
            var serializer = new DataContractJsonSerializer(o.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, o);
                return sha.ComputeHash(ms.ToArray());
            }
        }

        public static string ComputeHash(this object o)
        {
            return Encoding.UTF8.GetString(ComputeHashBytes(o));
        }

        public static string ToJson(this object o)
        {
            var serializer = new DataContractJsonSerializer(o.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, o);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static T Deserialize<T>(this string json)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)(serializer.ReadObject(ms));
            }
        }

        public static object ConvertType(string stringValue, Type type)
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
            return converter.IsValid(stringValue) ? converter.ConvertFrom(stringValue) : null;
        }

        public static object ConvertType(string stringValue, string type)
        {
            return ConvertType(stringValue, Type.GetType(type, true, true));
        }
    }
}