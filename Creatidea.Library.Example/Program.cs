using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatidea.Library.Example
{
    using Ci.Uploads.Service;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CiFileService.RootPath);
            Console.Read();
        }
    }
}
