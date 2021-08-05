using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Net;
using HttpMultipartParser;
using System;
using System.Linq;
using System.Reflection;

namespace DionysosFX.Swan.HttpMultipart
{
    public static class MultipartConverter
    {
        public static object ToFormObject(this IHttpContext @this, Type destType)
        {
            var result = Activator.CreateInstance(destType);
            var properties = result.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                bool isArray = property.IsArray();
                if (isArray)
                {
                    if (property.PropertyType.GenericTypeArguments.Any(f=>f.UnderlyingSystemType != typeof(FilePart)))
                    {
                        var formDatas = @this.Request.Form.Where(f => f.Name == property.Name).ToList();
                        if (formDatas != null)
                            property.SetValue(result, Convert.ChangeType(formDatas, property.PropertyType));
                        else
                            property.SetValue(result, null);
                    }
                    else
                    {
                        var fileDatas = @this.Request.Files.Where(f => f.Name == property.Name).ToList();
                        if(fileDatas!=null)
                            property.SetValue(result, Convert.ChangeType(fileDatas, property.PropertyType));
                        else
                            property.SetValue(result, null);
                    }
                }
                else
                {
                    if (property.PropertyType != typeof(FilePart))
                    {
                        var formData = @this.Request.Form.FirstOrDefault(f => f.Name == property.Name);
                        if (formData != null)
                            property.SetValue(result, Convert.ChangeType(formData.Data, property.PropertyType));
                        else
                            property.SetValue(result, null);
                    }
                    else
                    {
                        var fileData = @this.Request.Files.FirstOrDefault(f => f.Name == property.Name);
                        if (fileData != null)
                            property.SetValue(result, Convert.ChangeType(fileData, property.PropertyType));
                        else
                            property.SetValue(result, null);
                    }
                }
            }
            return result;
        }
    }
}
