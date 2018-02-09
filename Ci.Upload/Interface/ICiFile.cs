using Ci.Upload.Enums;

namespace Ci.Upload.Interface
{
    interface ICiFile
    {
        /// <summary>
        /// Gets or sets the type of the storage.
        /// </summary>
        /// <value>The type of the storage.</value>
        StorageType StorageType { get; set; }

        /// <summary>
        /// 原檔名(不含副檔名)
        /// </summary>
        string OriName { get; set; }

        /// <summary>
        /// 更改後的檔名(不含副檔名)
        /// </summary>
        string NewName { get; set; }

        /// <summary>
        /// 副檔名(.XXX)
        /// </summary>
        string Extension { get; set; }

        /// <summary>
        /// 完整檔案存放路徑(D:/AAA/BBB/CCC.ddd) or (http://imgur.com/xxx.png)
        /// </summary>
        string FullPath { get; set; }

        /// <summary>
        /// 從根目錄的相對檔案存放資料夾位置(~/AAA/BBB/xxx.yy)
        /// </summary>
        string VirtualPath { get; set; }

        /// <summary>
        /// 欲放置資料夾(AAA/BBB/...)
        /// </summary>
        string Folder { get; set; }
    }
}
