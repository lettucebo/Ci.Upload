using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Ci.Upload.Extensions;
using Ci.Upload.Models;
using Creatidea.Library.Configs;

namespace Ci.Upload.Service
{
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
            if (file.IsNullOrEmpty())
            {
                throw new NullReferenceException("file is null or no content length");
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

        /// <summary>
        /// 取得新的檔案名稱
        /// </summary>
        /// <returns>System.String.</returns>
        private static string GetNewFileName()
        {
            return Ci.Sequential.Guid.Create().ToString().ToUpper();
        }
    }
}
