using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Extensions
{
    using System.Drawing;

    using Creatidea.Library.Uploads.Models;
    using Creatidea.Library.Uploads.Service;

    public static class ImageExtensions
    {
        public static CiImageFile SaveAsLocal(this Image image, string folder = "Temps")
        {
            return CiFileService.SaveLocal(image, image.RawFormat, folder);
        }
    }
}
