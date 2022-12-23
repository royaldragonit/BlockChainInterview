using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace BlockChainInterview.Utilities
{
    public static class Api
    {
        public const string ApiKey = "ISAXW39G1MN43Q4UUNVCZG6DFHT2JGGQFK";
        public static T Post<T>(string path, object param = null)
        {
            var client = new RestClient(path);
            client.UseNewtonsoftJson();
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(param);
            var res = client.Execute<T>(request);
            return res.Data;
        }

        public static T Get<T>(string path)
        {
            var client = new RestClient(path);
            var request = new RestRequest();
            request.Method = Method.Get;
            var res = client.Execute<T>(request);
            return res.Data;
        }
        public static T GetEther<T>(string action,string tag, params string[] otherParam)
        {
            string url = $"https://api.etherscan.io/api?module=proxy&action={action}&tag={tag}&boolean=true&apikey={ApiKey}";
            foreach (string p in otherParam)
            {
                url += p;
            }
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Get;
            var res = client.Execute<T>(request);
            return res.Data;
        }
        public static async Task<T> GetAsync<T>(string path)
        {
            var client = new RestClient(path);
            var request = new RestRequest();
            request.Method = Method.Get;
            var res = await client.ExecuteAsync<T>(request);
            return res.Data;
        }
        public static async Task<string> GetStringAsync(string path)
        {
            var client = new RestClient(path);
            var request = new RestRequest();
            request.Method = Method.Get;
            var res = await client.ExecuteAsync<string>(request);
            return res.Content;
        }
    }
}