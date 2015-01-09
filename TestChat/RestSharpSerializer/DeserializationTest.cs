using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSharpSerializer
{
    public static class DeserializationTest
    {
        public static void DeserializeString()
        {
            string jsonString = "{\"has_title\":true,\"title\":\"GoodLuck\",\"entries\":[[\"/gettingstarted.pdf\",{\"thumb_exists\":false,\"path\":\"/GettingStarted.pdf\",\"client_mtime\":\"Wed, 08 Jan 2014 18:00:54+0000\",\"bytes\":249159}],[\"/task.jpg\",{\"thumb_exists\":true,\"path\":\"/Task.jpg\",\"client_mtime\":\"Tue, 14 Jan 2014 05:53:57+0000\",\"bytes\":207696}]]}";

            RestResponse response = new RestResponse();
            response.Content = jsonString;
            RestSharp.Deserializers.JsonDeserializer desrial = new RestSharp.Deserializers.JsonDeserializer();

            var obj = desrial.Deserialize<RootObject>(response);

        }
    }
    
    public class RootObject
    {
        public bool has_title { get; set; }
        public string title { get; set; }
        public List<List<object>> entries { get; set; }
    }
}