using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Models
{
    using Creatidea.Library.Uploads.Enums;
    using Creatidea.Library.Uploads.Interface;

    public class CiWebFileResult : ICiFileResult
    {
        public StorageType StorageType { get; set; }

        public string OriName { get; set; }

        public string NewName { get; set; }

        public string Extension { get; set; }

        public string FullPath { get; set; }

        /// <summary>
        /// 檔案存放資料夾位置(~/AAA/BBB/)
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// 欲放置資料夾(AAA/BBB/...)
        /// </summary>
        public string Folder { get; set; }
    }
}
