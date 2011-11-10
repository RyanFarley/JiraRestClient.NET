using Newtonsoft.Json.Linq;
using RestSharp;

namespace JiraRestClient
{
    /// <summary>
    /// The response from a REST request to JIRA
    /// </summary>
    public class JiraRestResponse : IJiraRestResponse
    {
        #region Public Properties

        /// <summary>
        /// The RestSharp response object
        /// </summary>
        public IRestResponse RestResponse { get; private set; }

        /// <summary>
        /// The JiraRestClient used to make the request
        /// </summary>
        public IJiraRestClient JiraRestClient { get; private set; }

        /// <summary>
        /// The JSON data from the response; this may be null.
        /// </summary>
        public JObject JObject { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a JiraRestResponse
        /// </summary>
        /// <param name="jiraRestClient">The JiraRestClient used to make the request</param>
        /// <param name="restResponse">The RestSharp response object</param>
        public JiraRestResponse(IJiraRestClient jiraRestClient, IRestResponse restResponse)
        {
            JiraRestClient = jiraRestClient;
            RestResponse = restResponse;
            JObject = restResponse.ContentType.Contains("application/json") ? JObject.Parse(restResponse.Content) : null;
        }

        #endregion
    }
}
