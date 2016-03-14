using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace LeanOwinApi.Config
{
    internal sealed class FileServerConfig
    {
        public static void Configure(IAppBuilder app)
        {
            var fileSystem = new PhysicalFileSystem("./Content");
            var fileServerOptions = new FileServerOptions();
            fileServerOptions.FileSystem = fileSystem;
            fileServerOptions.EnableDefaultFiles = true;
            app.UseFileServer(fileServerOptions);
        }
    }
}
