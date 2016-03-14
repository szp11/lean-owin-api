using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;

namespace LeanOwinApi
{
    internal sealed class FileServerConfig
    {
        public static FileServerOptions Configure()
        {
            var fileSystem = new PhysicalFileSystem("./Content");
            var fso = new FileServerOptions();
            fso.FileSystem = fileSystem;
            fso.EnableDefaultFiles = true;
            return fso;   
        }
    }
}
