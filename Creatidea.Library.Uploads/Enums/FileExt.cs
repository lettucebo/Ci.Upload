using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Uploads.Enums
{
    public class FileExtensions
    {
        /// <summary>
        /// 網路圖片支援列表
        /// </summary>
        /// <value>The image.</value>
        public List<string> WebImage => new List<string>() { ".jpg", ".jpeg", ".png", ",gif" };

        public List<string> MsWord => new List<string>() { ".doc", ".docx", ".dot", ".docm", ".dotx", ".dotm", "docb" };

        public List<string> MsPowerPoint => new List<string>() { ".ppt", ".pptx", ".pot", ".pps", ".pptm", ".potx", ".potm", ".ppam", ".ppsx", ".ppsm", ".sldx", ".sldm" };

        public List<string> MsExcel => new List<string>() { ".xls", ".xlsx", ".xlt", ".xlm", ".xlsm", ".xltx", ".xltm", ".xlsb", ".xla", ".xlam", ".xll", ".xlw" };

    }
}
