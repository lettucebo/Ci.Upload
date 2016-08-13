namespace Ci.Uploads.Models
{
    using Ci.Uploads.Enums;

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
