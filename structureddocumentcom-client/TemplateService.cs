using com.structureddocument.api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace com.structureddocument.client
{
    // inspired by http://www.piotrwalat.net/consuming-asp-net-web-api-services-in-a-windows-8-c-xaml-app/
    public class TemplateService : ITemplateService
    {
        private string ServiceUrl;
        private readonly HttpClient _client = new HttpClient();

        public TemplateService(string serviceUrl)
        {
            ServiceUrl = serviceUrl;
        }

        public async Task<IEnumerable<Template>> GetAll()
        {
            HttpResponseMessage response = await _client.GetAsync(ServiceUrl);
            var jsonSerializer = CreateDataContractJsonSerializer(typeof(Template[]));
            var stream = await response.Content.ReadAsStreamAsync();
            return (Template[])jsonSerializer.ReadObject(stream);
        }

        public async Task Add(Template template)
        {
            var jsonString = Serialize(template);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(ServiceUrl, content);
        }

        public async Task Delete(int id)
        {
            var result = await _client.DeleteAsync(String.Format("{0}/{1}"
               , ServiceUrl, id.ToString()));
        }

        public async Task Update(Template template)
        {
            var jsonString = Serialize(template);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await _client.PutAsync(String
                .Format("{0}/{1}", ServiceUrl, template.Id), content);

        }

        private static DataContractJsonSerializer CreateDataContractJsonSerializer(Type type)
        {
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat(dateFormat)
            };
            var serializer = new DataContractJsonSerializer(type, settings);
            return serializer;
        }

        private string Serialize(Template template)
        {
            var jsonSerializer = CreateDataContractJsonSerializer(typeof(Template));
            byte[] streamArray = null;
            using (var memoryStream = new MemoryStream())
            {
                jsonSerializer.WriteObject(memoryStream, template);
                streamArray = memoryStream.ToArray();
            }
            string json = Encoding.UTF8.GetString(streamArray, 0, streamArray.Length);
            return json;
        }

    }
}
