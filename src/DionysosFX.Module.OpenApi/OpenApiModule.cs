using DionysosFX.Module.WebApi;
using DionysosFX.Swan.Modules;
using DionysosFX.Swan.Net;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DionysosFX.Module.OpenApi
{
    public class OpenApiModule : IWebModule
    {
        public void Dispose()
        {
        }

        public async Task HandleRequestAsync(IHttpContext context)
        {
            if (context.IsHandled)
                return;
            if(context.Request.Url.LocalPath == "/openapi")
            {
                context.SetHandled();
            }
        }

        public void Start(CancellationToken cancellationToken)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            var xmlPath = string.Format("{0}.xml", Path.Combine(Path.GetDirectoryName(entryAssembly.Location),entryAssembly.GetName().Name));
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlPath);
                XmlNodeList members = xmlDocument.SelectNodes("//doc//members//member");
                foreach (XmlNode member in members)
                {
                    var memberName = member.Attributes["name"].Value;

                    if(memberName.StartsWith("T"))
                    {
                        memberName = memberName.Replace("T:", "");
                        var memberType = entryAssembly.GetType(memberName);
                        if(memberType != null)
                        {
                            var isController = memberType.IsWebApiController();
                            if (isController)
                            {
                                
                            }
                        }
                    }else if (memberName.StartsWith("M:"))
                    {
                        memberName = memberName.Replace("M:", "");
                        var memberNamePieces = memberName.Split('.').ToList();
                        var namespaces = memberNamePieces.Where(f => !f.Contains("(") && !f.Contains(")")).ToList();
                        string controllerName = string.Join(".", namespaces.Take(namespaces.Count-1));
                        var memberType = entryAssembly.GetType(controllerName);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
