namespace Ci.Uploads.Extensions
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;

    using Ci.Uploads.Models;
    using Ci.Uploads.Service;

    using Microsoft.WindowsAzure.Storage.Auth;

    public static class HttpPostedFileBaseExtensions
    {
        /// <summary>
        /// 儲存在本地端
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static CiFile SaveAsLocal(this HttpPostedFileBase file, string folder = "Temps", string fileName = "")
        {
            return CiFileService.SaveLocal(file, folder, fileName);
        }

        /// <summary>
        /// 儲存在 Azure Storage
        /// </summary>
        /// <param name="file"></param>
        /// <param name="container"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static CiFile SaveAsAzureStorage(this HttpPostedFileBase file, string containerName = "temps", string fileName = "", string connectionString = "")
        {
            containerName = containerName.ToLower();

            Regex regex = new Regex("^[a-z0-9_-]+$");
            Match match = regex.Match(containerName);
            if (!match.Success)
            {
                throw new ArgumentException("container naming illegal", nameof(containerName));
            }

            return CiFileService.SaveAzureStorage(file, containerName, fileName, connectionString);
        }

        /// <summary>
        /// 儲存在 Azure Storage
        /// </summary>
        /// <param name="file"></param>
        /// <param name="container"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async static Task<CiFile> SaveAsAzureStorageAsync(this HttpPostedFileBase file, string containerName = "temps", string fileName = "", string connectionString = "")
        {
            containerName = containerName.ToLower();

            Regex regex = new Regex("^[a-z0-9_-]+$");
            Match match = regex.Match(containerName);
            if (!match.Success)
            {
                throw new ArgumentException("container naming illegal", nameof(containerName));
            }

            var ciFile = await CiFileService.SaveAzureStorageAsync(file, containerName, fileName, connectionString);
            return ciFile;
        }
    }
}
