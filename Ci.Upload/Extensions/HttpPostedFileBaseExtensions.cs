using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Ci.Upload.Models;
using Ci.Upload.Service;

namespace Ci.Upload.Extensions
{
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
        /// Determines whether [is null or empty] [the specified file].
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns><c>true</c> if [is null or empty] [the specified file]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this HttpPostedFileBase file)
        {
            return (file == null || file.ContentLength <= 0);
        }
    }
}
