using Autofac;
using DionysosFX.Swan.Associations;
using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Module.StaticFile
{
    /// <summary>
    /// Static file module
    /// </summary>
    internal class StaticFileModule : WebModuleBase
    {
        /// <summary>
        /// Static file options
        /// </summary>
        StaticFileOptions options = null;
        /// <summary>
        /// Files
        /// </summary>
        ConcurrentDictionary<string, StaticFileItem> _files = null;

        /// <summary>
        /// Static file module started was  and trigged this method
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override void Start(CancellationToken cancellationToken)
        {
            _files = new();
            Container.TryResolve<StaticFileOptions>(out options);
            PeriodicTask.Create(HandleExpireFiles, 1, cancellationToken);
        }

        /// <summary>
        /// Static file module handle request and trigged this method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task HandleRequestAsync(IHttpContext context)
        {
            if (context.IsHandled)
                return;
            string fileName = string.Empty;
            if (context.Request.RawUrl == "/")
            {
                fileName = "index.html";
            }
            else
            {                
                fileName = options.ExecutionDirectory;
                foreach (var dir in context.Request.RawUrl.Split('/'))
                    fileName = Path.Combine(fileName, dir.ToLowerInvariant());
            }
            while (fileName.Contains("?") || fileName.Contains("&"))
            {
                var indexOf = fileName.IndexOf('?');
                if (indexOf == -1)
                    indexOf = fileName.IndexOf('&');
                if (indexOf != -1)
                    fileName = fileName.Substring(0, indexOf);
            }
            var contentType = MimeType.Associations.GetValueOrDefault(Path.GetExtension(fileName));
            byte[] bytes = null;
            if (_files.ContainsKey(fileName))
            {
                if (_files.TryGetValue(fileName, out StaticFileItem staticFileItem))
                {
                    bytes = staticFileItem.Data;
                    if (options.CacheActive)
                    {
                        var expire = (staticFileItem.ExpireDate - DateTime.Now).TotalSeconds;
                        context.AddCacheExpire(expire);
                    }
                }
            }
            else
            {
                try
                {
                    if (File.Exists(fileName))
                    {
                        if (contentType.StartsWith("text"))
                        {
                            bytes = Encoding.UTF8.GetBytes(File.ReadAllText(fileName));
                        }
                        else
                        {
                            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read))
                            {
                                bytes = new byte[fs.Length];
                                fs.Read(bytes, 0, (int)fs.Length);
                            }
                        }

                        if (options.CacheActive)
                        {
                            _files.TryAdd(fileName, new StaticFileItem(bytes, DateTime.Now.AddSeconds(options.ExpireTime)));
                            var expire = DateTime.Now.AddMinutes(5).Second;
                            context.AddCacheExpire(expire);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }

            if (bytes != null)
            {
                if (options.AllowedMimeTypes.Any(f => f == "*" || f == contentType))
                {
                    if (!contentType.StartsWith("text"))
                        context.Response.Headers.Add("Content-disposition", "attachment; filename=" + Guid.NewGuid().ToString()+Path.GetExtension(fileName));
                    context.Response.OutputStream.Write(bytes);
                    context.Response.ContentType = contentType;
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.SetHandled();
                }
            }
        }

        /// <summary>
        ///  
        /// </summary>
        private void HandleExpireFiles()
        {
            List<KeyValuePair<string, StaticFileItem>> deleteItems = new();
            foreach (var item in _files)
            {
                if (item.Value.ExpireDate < DateTime.Now)
                    deleteItems.Add(item);
            }

            foreach (var item in deleteItems)
            {
                _files.TryRemove(item);
            }

        }

        public override void Dispose()
        {
            _files.Clear();
            _files = null;
        }
    }
}
