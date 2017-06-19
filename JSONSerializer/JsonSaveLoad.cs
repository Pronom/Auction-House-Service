using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JSONSerializer
{
    public class JsonSaveAndLoad   
    {

        SafeHandle safe;

        public JsonSaveAndLoad()
        {
            safe = new SafeFileHandle(IntPtr.Zero, true);
        }

        public object deserializeData(object objectName, Type type, string uri, SourceType sourceType)
        {
            if (sourceType == SourceType.FromFile)
            {
                if (File.Exists(uri))
                {
                    objectName = JsonConvert.DeserializeObject(File.ReadAllText(uri), type, new JavaScriptDateTimeConverter());
                    if (objectName == null)
                    {
                        return objectName = Activator.CreateInstance(type);
                    }
                    else
                    {
                        return objectName;
                    }
                }
                else
                {
                    File.Create(uri);
                    return objectName = Activator.CreateInstance(type);
                }
                

            }
            else if (sourceType == SourceType.FromWeb)
            {
                using (WebClient webClient = new WebClient())
                {
                    objectName = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(webClient.DownloadData(uri)), type);

                    return objectName;
                }
            }

            return null;
        }

        public void serializeData(Type type, object objectName, string uri, SelectFormatting formatting)
        {
            JsonSerializer jsSerializer = new JsonSerializer();
            
            jsSerializer.Converters.Add(new JavaScriptDateTimeConverter());
            jsSerializer.NullValueHandling = NullValueHandling.Ignore;


            using (StreamWriter streamWriter = new StreamWriter(uri))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.Formatting = (Formatting)formatting;
                jsSerializer.Serialize(jsonWriter, objectName);
            }
        }



    }

    public enum SourceType
    {
        FromFile,
        FromWeb
    }

    public enum SelectFormatting
    {
        Indented = Formatting.Indented,
        None = Formatting.None
    }
}
