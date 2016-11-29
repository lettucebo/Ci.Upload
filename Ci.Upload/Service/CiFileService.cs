namespace Ci.Uploads.Service
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;

    using Ci.Extensions;
    using Ci.Uploads.Enums;
    using Ci.Uploads.Models;

    using Creatidea.Library.Configs;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;

    using MimeTypeMap.List;

    /// <summary>
    /// CiFileService.
    /// </summary>
    public class CiFileService
    {
        /// <summary>
        /// 檔案儲存之 root path.
        /// </summary>
        /// <value>The root path.</value>
        public static string RootPath { get; set; }

        static CiFileService()
        {
            if (CiConfig.Global.CiFile.RootPath != null
                && !string.IsNullOrWhiteSpace(CiConfig.Global.CiFile.RootPath.ToString()))
            {
                RootPath = CiConfig.Global.CiFile.RootPath.ToString();
            }
            else
            {
                RootPath = "";
            }
        }

        public CiFileService(string rootPath)
        {
            RootPath = rootPath;
        }

        /// <summary>
        /// 檔案上傳儲存於本地端
        /// </summary>
        /// <param name="file">上傳之檔案</param>
        /// <param name="folder">欲存放資料夾名稱</param>
        /// <returns>T.</returns>
        /// <exception cref="System.NullReferenceException">檔案為空或是沒有任何檔案長度</exception>
        protected internal static CiFile SaveLocal(HttpPostedFileBase file, string folder = "Temps", string fileName = "")
        {
            if (IsNullOrEmpty(file))
            {
                throw new NullReferenceException("檔案為空或是沒有任何檔案長度");
            }

            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = GetNewFileName();
            }

            CiFile model = new CiFile
            {
                Extension = ext,
                NewName = fileName,
                OriName = Path.GetFileNameWithoutExtension(file.FileName),
                Folder = folder,
                VirtualPath = Path.Combine(RootPath, folder)
            };
            model.FullPath =
                HttpContext.Current.Server.MapPath(Path.Combine(model.VirtualPath, model.NewName + model.Extension));

            // 若檔名重複則自動重新取得編號
            while (File.Exists(model.FullPath))
            {
                model.NewName = GetNewFileName();
                model.FullPath =
                    HttpContext.Current.Server.MapPath(Path.Combine(model.VirtualPath, model.NewName + model.Extension));
            }

            // 檢查資料夾是否存在，若不存在則自動建立
            if (!Directory.Exists(Path.GetDirectoryName(model.FullPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(model.FullPath));
            }

            // 存檔
            file.SaveAs(model.FullPath);

            model.VirtualPath = Path.Combine(model.VirtualPath, model.NewName + model.Extension);

            return model;
        }

        // todo 將組CIFILE抽出來

        /// <summary>
        /// 圖片儲存
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <param name="folder">欲存放資料夾名稱</param>
        /// <returns>CiResult FileUploadViewModel</returns>
        protected internal static CiImageFile SaveLocal(Image image, ImageFormat imageFormat, string folder = "Temps", string fileName = "")
        {
            if (image == null)
            {
                throw new NullReferenceException("圖片為空！");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = GetNewFileName();
            }

            CiImageFile model = new CiImageFile()
            {
                Extension = imageFormat.ToFileExtension(),
                NewName = fileName,
                Folder = folder,
                VirtualPath = Path.Combine(RootPath, folder),
                Format = imageFormat
            };
            model.FullPath =
                HttpContext.Current.Server.MapPath(Path.Combine(model.VirtualPath, model.NewName + model.Extension));

            // 若檔名重複則自動重新取得編號
            while (File.Exists(model.FullPath))
            {
                model.NewName = GetNewFileName();
                model.FullPath =
                    HttpContext.Current.Server.MapPath(Path.Combine(model.VirtualPath, model.NewName + model.Extension));
            }

            // 檢查資料夾是否存在，若不存在則自動建立
            if (!Directory.Exists(Path.GetDirectoryName(model.FullPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(model.FullPath));
            }

            // 存檔
            image.Save(model.FullPath, imageFormat);

            model.VirtualPath = Path.Combine(model.VirtualPath, model.NewName + model.Extension);

            return model;
        }

        protected internal static CiFile SaveAzureStorage(HttpPostedFileBase file, string containerName, string fileName, string connectionString)
        {
            if (IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file), "file is null or empty");
            }

            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = GetNewFileName();
            }

            var ciFile = new CiFile()
            {
                Extension = ext,
                Folder = containerName,
                NewName = fileName,
                OriName = Path.GetFileNameWithoutExtension(file.FileName),
                StorageType = StorageType.Azure
            };

            // initlize storageaccount by various setting
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                if (CiConfig.Global.CiFile.AzureStorage.ConnectionString != null
                    && !string.IsNullOrWhiteSpace(CiConfig.Global.CiFile.AzureStorage.ConnectionString.ToString()))
                {
                    connectionString = CiConfig.Global.CiFile.AzureStorage.ConnectionString.ToString();
                }
                else
                {
                    throw new ArgumentNullException(nameof(CiConfig.Global.CiFile.AzureStorage.ConnectionString), "AzureStorage ConnectionString is empty.");
                }
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container, container name must be loweer case.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });

            // Retrieve reference to a blob named "myblob", aka filename.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName + ext);

            blockBlob.UploadFromStream(file.InputStream);

            ciFile.FullPath = blockBlob.StorageUri.ToString();
            ciFile.VirtualPath = blockBlob.Uri.ToString();

            return ciFile;
        }

        protected internal async static Task<CiFile> SaveAzureStorageAsync(HttpPostedFileBase file, string containerName, string fileName, string connectionString)
        {
            if (IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file), "file is null or empty");
            }

            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = GetNewFileName();
            }

            var ciFile = new CiFile()
            {
                Extension = ext,
                Folder = containerName,
                NewName = fileName,
                OriName = Path.GetFileNameWithoutExtension(file.FileName),
                StorageType = StorageType.Azure
            };

            // initlize storageaccount by various setting
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                if (CiConfig.Global.CiFile.AzureStorage.ConnectionString != null
                    && !string.IsNullOrWhiteSpace(CiConfig.Global.CiFile.AzureStorage.ConnectionString.ToString()))
                {
                    connectionString = CiConfig.Global.CiFile.AzureStorage.ConnectionString.ToString();
                }
                else
                {
                    throw new ArgumentNullException(nameof(CiConfig.Global.CiFile.AzureStorage.ConnectionString), "AzureStorage ConnectionString is empty.");
                }
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container, container name must be loweer case.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            // Retrieve reference to a blob named "myblob", aka filename.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName + ext);

            await blockBlob.UploadFromStreamAsync(file.InputStream);

            ciFile.FullPath = blockBlob.StorageUri.ToString();
            ciFile.VirtualPath = blockBlob.Uri.ToString();

            return ciFile;
        }

        /// <summary>
        /// 檢查檔案副檔名是否符合
        /// </summary>
        /// <param name="extension">檔案之副檔名</param>
        /// <param name="mime">The MIME.</param>
        /// <param name="checkExtensionList">The need to be check extension's list.</param>
        /// <param name="checkMime">if set to <c>true</c> [check MIME].</param>
        /// <returns>bool</returns>
        public static bool CheckExtensionWithMime(string extension, string mime, List<string> checkExtensionList, bool checkMime = true)
        {
            if (!checkExtensionList.Contains(extension))
            {
                return false;
            }

            if (checkMime)
            {
                foreach (string ext in checkExtensionList)
                {
                    if (MimeTypeMap.GetMimeType(ext).Contains(mime))
                        return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// 取得新的檔案名稱
        /// </summary>
        /// <returns>System.String.</returns>
        private static string GetNewFileName()
        {
            return Ci.Sequential.Guid.Create().ToString().ToUpper();
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified file].
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns><c>true</c> if [is null or empty] [the specified file]; otherwise, <c>false</c>.</returns>
        private static bool IsNullOrEmpty(HttpPostedFileBase file)
        {
            return (file == null || file.ContentLength <= 0);
        }
    }
}
