using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using System.IO;

namespace XODB.Module
{
    public static class Package
    {
        public const string FILE_COMPRESSION = "zip";
        public const string FILE_EXTENSION = "xodb";
        public const string FILE_FILTER = "XODB File (*.xodb)|*.xodb";
        public const string FILE_CONTENTS = "Model.xafml;";
        public static string[] FileContents { get { return FILE_CONTENTS.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries); } }

        public const string CONFIG_FILE = "Model.xafml";
        public const string CONFIG_USER_FILE = "Model.User.xafml";
        public const string CONFIG_FILTER = "Configuration File (*.xafml)|*.xafml";
        public static Stream ReadConfigFromPackage(this Stream packageFileStream)
        {
            using (var fileInflater = ZipFile.Read(packageFileStream))
            {
                foreach (ZipEntry entry in fileInflater)
                {
                    if (entry == null) { continue; }

                    if (!entry.IsDirectory && !string.IsNullOrEmpty(entry.FileName) && (entry.FileName.EndsWith(CONFIG_FILE) || entry.FileName.EndsWith(CONFIG_USER_FILE)))
                    {
                        using (var stream = entry.OpenReader())
                        {
                            return stream;

                        }
                    }
                }
            }

            return null;
        }


        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public static void WriteUserConfigFile(this string xodbPath, Stream configStream)
        {            
            var f = Path.Combine(xodbPath, Package.CONFIG_USER_FILE);
            File.Delete(f);
            using (Stream file = File.OpenWrite(f))
            {
                CopyStream(configStream, file);
            }

        }

        public static string CreatePackageFromUserConfigFile(this string xodbPath)
        {
            var tmpFile = Path.GetTempFileName() + ".xodb";
            using (ZipFile zip = new ZipFile())
            {
                var p = Path.Combine(xodbPath, Package.CONFIG_USER_FILE);
                if (!File.Exists(p))
                    return null;
                zip.AddFile(p);
                zip.Save(tmpFile);
            }
            return tmpFile;
        }

        public static void SendUserConfig(this string xodbPath, string[] emailRecipients)
        {
            if (emailRecipients == null || emailRecipients.Length < 1)
                return;
            MAPI mapi = new MAPI();
            var f = xodbPath.CreatePackageFromUserConfigFile();
            if (f == null)
                return;
            mapi.AddAttachment(f);
            foreach (var r in emailRecipients)
                mapi.AddRecipientTo(r);
            mapi.SendMailPopup("Updated XODB Configuration File", "Please install the attached configuration file for XODB.");

        }

    }
       

}
