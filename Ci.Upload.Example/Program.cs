using Ci.Upload.Service;
using System;

namespace Ci.Upload.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CiFileService.RootPath);
            Console.Read();
        }
    }
}
