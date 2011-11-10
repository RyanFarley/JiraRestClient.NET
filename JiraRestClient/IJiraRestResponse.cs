using System;
namespace JiraRestClient
{
    public interface IJiraRestResponse
    {
        IJiraRestClient JiraRestClient { get; }
        Newtonsoft.Json.Linq.JObject JObject { get; }
        RestSharp.IRestResponse RestResponse { get; }
    }
}
