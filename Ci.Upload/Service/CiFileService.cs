namespace Ci.Uploads.Service
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;

    using Ci.Extensions;
    using Ci.Uploads.Models;

    using Creatidea.Library.Configs;

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

        public CiFileService()
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
        protected internal static CiFile SaveLocal(HttpPostedFileBase file, string folder = "Temps")
        {
            if (IsNullOrEmpty(file))
            {
                throw new NullReferenceException("檔案為空或是沒有任何檔案長度");
            }

            var ext = Path.GetExtension(file.FileName);

            CiFile model = new CiFile
            {
                Extension = ext,
                NewName = GetNewFileName(),
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

        /// <summary>
        /// 圖片儲存
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <param name="folder">欲存放資料夾名稱</param>
        /// <returns>CiResult FileUploadViewModel</returns>
        protected internal static CiImageFile SaveLocal(Image image, ImageFormat imageFormat, string folder = "Temps")
        {
            if (image == null)
            {
                throw new NullReferenceException("圖片為空！");
            }

            CiImageFile model = new CiImageFile()
            {
                Extension = imageFormat.ToFileExtension(),
                NewName = GetNewFileName(),
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

        /// <summary>
        /// Determines whether [is null or empty] [the specified file].
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns><c>true</c> if [is null or empty] [the specified file]; otherwise, <c>false</c>.</returns>
        private static bool IsNullOrEmpty(HttpPostedFileBase file)
        {
            return (file == null || file.ContentLength <= 0);
        }

        /// <summary>
        /// 取得新的檔案名稱
        /// </summary>
        /// <returns>System.String.</returns>
        private static string GetNewFileName()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        #region image

        /*
        /// <summary>
        /// 圖片儲存
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <param name="folderName">欲存放資料夾名稱</param>
        /// <returns>CiResult FileUploadViewModel</returns>
        public CiImageFile SaveAs(Image image, ImageFormat imageFormat, string folderName = "Temps")
        {
            if (image == null)
            {
                throw new NullReferenceException("圖片為空！");
            }

            CiImageFile model = new CiImageFile()
            {
                Extension = imageFormat.ToFileExtension(),
                NewName = GetNewFileName(),
                Folder = folderName,
                VirtualPath = Path.Combine(RootPath, folderName),
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
        */
        #endregion

        /// <summary>
        /// 檢查檔案副檔名是否符合
        /// </summary>
        /// <param name="extension">檔案之副檔名</param>
        /// <param name="mime">The MIME.</param>
        /// <param name="checkExtensionList">The need to be check extension's list.</param>
        /// <param name="checkMime">if set to <c>true</c> [check MIME].</param>
        /// <returns>bool</returns>
        public static bool CheckExtension(string extension, string mime, List<string> checkExtensionList, bool checkMime = true)
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
    }
}
