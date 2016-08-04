using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Extensions
{
    using System.Drawing;
    using System.Drawing.Imaging;

    using Creatidea.Library.Uploads.Models;
    using Creatidea.Library.Uploads.Service;

    public static class ImageExtensions
    {
        public static CiImageFile SaveAsLocal(this Image image, ImageFormat format = null, string folder = "Temps")
        {
            if (format == null)
                format = image.RawFormat;
            return CiFileService.SaveLocal(image, format, folder);
        }
    }
}
