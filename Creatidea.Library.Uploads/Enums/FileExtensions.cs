namespace Ci.Uploads.Enums
{
    using System.Collections.Generic;

    public class FileExtensions
    {
        /// <summary>
        /// 網路圖片支援列表
        /// </summary>
        /// <value>The image.</value>
        public static List<string> WebImage => new List<string>() { ".jpg", ".jpeg", ".png", ",gif" };

        public static List<string> MsWord => new List<string>() { ".doc", ".docx", ".dot", ".docm", ".dotx", ".dotm", "docb" };

        public static List<string> MsPowerPoint => new List<string>() { ".ppt", ".pptx", ".pot", ".pps", ".pptm", ".potx", ".potm", ".ppam", ".ppsx", ".ppsm", ".sldx", ".sldm" };

        public static List<string> MsExcel => new List<string>() { ".xls", ".xlsx", ".xlt", ".xlm", ".xlsm", ".xltx", ".xltm", ".xlsb", ".xla", ".xlam", ".xll", ".xlw" };

    }
}
