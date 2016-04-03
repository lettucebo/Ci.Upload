using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Interface
{
    using Creatidea.Library.Uploads.Enums;

    interface ICiFileResult
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
        /// 完整檔案存放路徑(D:/AAA/BBB/CCC.ddd)
        /// </summary>
        string FullPath { get; set; }
    }
}
