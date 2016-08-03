using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Models
{
    using Creatidea.Library.Uploads.Enums;
    using Creatidea.Library.Uploads.Interface;

    public class CiWebFile : CiFile
    {
        public CiWebFile()
        {
            this.StorageType = StorageType.Local;
        }

        /// <summary>
        /// 檔案網址 (http://imgur.com/xxx.png)
        /// </summary>
        public string Url { get; set; }
    }
}
