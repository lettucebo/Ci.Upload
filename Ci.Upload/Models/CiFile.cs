namespace Ci.Uploads.Models
{
    using Ci.Uploads.Enums;
    using Ci.Uploads.Interface;

    public class CiFile : ICiFile
    {
        /// <summary>
        /// Gets or sets the type of the storage.
        /// </summary>
        /// <value>The type of the storage.</value>
        public StorageType StorageType { get; set; }

        /// <summary>
        /// 原檔名(不含副檔名)
        /// </summary>
        public string OriName { get; set; }

        /// <summary>
        /// 更改後的檔名(不含副檔名)
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        /// 副檔名(.XXX)
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 完整檔案存放路徑(D:/AAA/BBB/CCC.ddd) 
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 從根目錄的相對檔案存放資料夾位置(~/AAA/BBB/xxx.yy)
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// 欲放置資料夾(AAA/BBB/...)
        /// </summary>
        public string Folder { get; set; }
    }
}
