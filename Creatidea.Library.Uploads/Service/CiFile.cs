using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Service
{
    using System.IO;
    using System.Web;

    using Creatidea.Library.Configs;
    using Creatidea.Library.Results;
    using Creatidea.Library.Uploads.Models;

    using MimeTypeMap.List;

    public class CiFile
    {
        private string RootPath { get; set; }

        public CiFile()
        {
            if (CiConfig.Global.CiFile.RootPath != null
                && string.IsNullOrWhiteSpace(CiConfig.Global.CiFile.RootPath.ToString()))
            {
                this.RootPath = CiConfig.Global.CiFile.RootPath.ToString();
            }
            else
            {
                this.RootPath = string.Empty;
            }
        }

        public CiFile(string rootPath)
        {
            this.RootPath = rootPath;
        }

        /// <summary>
        /// 檔案上傳
        /// </summary>
        /// <param name="file">上傳之檔案</param>
        /// <param name="folderName">欲存放資料夾名稱</param>
        /// <returns>CiResult FileUploadViewModel </returns>
        public CiResult<CiWebFileResult> Upload(HttpPostedFileBase file, string folderName = "Temps")
        {
            var result = new CiResult<CiWebFileResult>() { Message = "上傳檔案發生錯誤！" };

            if (file == null || file.ContentLength <= 0)
            {
                result.Message += "檔案為空或是沒有任何檔案長度！";
                return result;
            }

            var ext = Path.GetExtension(file.FileName);

            CiWebFileResult model = new CiWebFileResult()
            {
                Extension = ext,
                NewName = Guid.NewGuid().ToString().ToUpper(),
                OriName = Path.GetFileNameWithoutExtension(file.FileName),
                Folder = folderName,
                VirtualPath = Path.Combine(RootPath, folderName)
            };
            model.FullPath =
                HttpContext.Current.Server.MapPath(Path.Combine(model.VirtualPath, model.NewName + model.Extension));

            // 若檔名重複則自動重新取得編號
            while (File.Exists(model.FullPath))
            {
                model.NewName = Guid.NewGuid().ToString().ToUpper();
                model.FullPath =
                    HttpContext.Current.Server.MapPath(Path.Combine(model.VirtualPath, model.NewName + model.Extension));
            }

            // 檢查資料夾是否存在，若不存在則自動建立
            if (!Directory.Exists(model.FullPath))
            {
                Directory.CreateDirectory(model.FullPath);
            }

            // 存檔
            file.SaveAs(model.FullPath);

            result.Success = true;
            result.Message = "上傳檔案儲存成功";
            result.Data = model;

            return result;
        }

        /// <summary>
        /// 檢查檔案副檔名是否符合
        /// </summary>
        /// <param name="extension">檔案之副檔名</param>
        /// <param name="mime">The MIME.</param>
        /// <param name="checkExtensionList">The need to be check extension's list.</param>
        /// <param name="checkMime">if set to <c>true</c> [check MIME].</param>
        /// <returns>bool</returns>
        public bool CheckExtension(string extension, string mime, List<string> checkExtensionList, bool checkMime = true)
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
