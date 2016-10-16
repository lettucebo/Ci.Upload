namespace Ci.Uploads.Models
{
    using System.Drawing.Imaging;

    using Ci.Uploads.Enums;

    public class CiImageFile : CiFile
    {
        public CiImageFile()
        {
            this.StorageType = StorageType.Local;
        }

        public ImageFormat Format { get; set; }
    }
}
