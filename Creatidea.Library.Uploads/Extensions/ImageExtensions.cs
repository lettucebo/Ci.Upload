namespace Ci.Uploads.Extensions
{
    using System.Drawing;
    using System.Drawing.Imaging;

    using Ci.Uploads.Models;
    using Ci.Uploads.Service;

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
