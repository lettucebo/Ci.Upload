using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Models
{
    using System.Drawing.Imaging;

    using Creatidea.Library.Uploads.Enums;
    using Creatidea.Library.Uploads.Interface;

    public class CiImageFile : CiFile
    {
        public CiImageFile()
        {
            this.StorageType = StorageType.Local;
        }

        public ImageFormat Format { get; set; }
    }
}
