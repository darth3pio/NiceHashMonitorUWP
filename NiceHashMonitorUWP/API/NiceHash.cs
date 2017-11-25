using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace API
{
    public class NiceHash
    {
        public static HttpClient http = new HttpClient();

        public async static Task<RootObject> GetApiAsync()
        {
            Uri uri = new Uri("https://api.nicehash.com/api");
            var response = await http.GetAsync(uri);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;

        }

        public async static Task<RootObject> GetAddrAsync(string addr)
        {
            Uri uri = new Uri("https://api.nicehash.com/api?method=stats.provider&addr=" + addr);
            var response = await http.GetAsync(uri);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public string error { get; set; }

        [DataMember]
        public string api_version { get; set; }

        [DataMember]
        public List<Stat> stats { get; set; }

        [DataMember]
        public List<Payment> payments { get; set; }

        [DataMember]
        public string addr { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Result result { get; set; }

        [DataMember]
        public object method { get; set; }
    }

    [DataContract]
    public class Stat
    {
        [DataMember]
        public string balance { get; set; }

        [DataMember]
        public string rejected_speed { get; set; }

        [DataMember]
        public int algo { get; set; }

        [DataMember]
        public string accepted_speed { get; set; }
    }

    [DataContract]
    public class Payment
    {
        [DataMember]
        public string amount { get; set; }

        [DataMember]
        public string fee { get; set; }

        [DataMember]
        public string TXID { get; set; }

        [DataMember]
        public string time { get; set; }
    }
}
