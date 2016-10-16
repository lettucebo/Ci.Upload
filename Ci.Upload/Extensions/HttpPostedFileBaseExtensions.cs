namespace Ci.Uploads.Extensions
{
    using System.Web;

    using Ci.Uploads.Models;
    using Ci.Uploads.Service;

    public static class HttpPostedFileBaseExtensions
    {
        public static CiFile SaveAsLocal(this HttpPostedFileBase file, string folder = "Temps", string fileName = "")
        {
            return CiFileService.SaveLocal(file, folder, fileName);
        }
    }
}
