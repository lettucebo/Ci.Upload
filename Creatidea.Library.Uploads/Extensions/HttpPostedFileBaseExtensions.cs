using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Extensions
{
    using System.Web;

    using Creatidea.Library.Uploads.Models;
    using Creatidea.Library.Uploads.Service;

    public static class HttpPostedFileBaseExtensions
    {
        public static CiFile SaveAsLocal(this HttpPostedFileBase file, string folder = "Temps")
        {
            return CiFileService.SaveLocal(file, folder);
        }
    }
}
