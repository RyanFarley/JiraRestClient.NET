using RestSharp;

namespace JiraRestClient
{
    public class JiraRestClient
    {
        #region Private Fields

        private readonly string _serverUrl;
        private readonly string _apiVersion;
        private readonly string _username;
        private readonly string _password;
        private readonly string _baseUrl;
        private readonly RestClient _restClient;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct the JiraRestClient, specifying the API Version
        /// </summary>
        /// <param name="serverUrl">The URL of the JIRA server</param>
        /// <param name="username">The JIRA username</param>
        /// <param name="password">The JIRA password</param>
        /// <param name="apiVersion">The API version such as "1.0", "latest"</param>
        public JiraRestClient(string serverUrl, string username, string password, string apiVersion)
        {
            _serverUrl = serverUrl;
            _username = username;
            _password = password;
            _apiVersion = apiVersion ?? "latest";
            _baseUrl = string.Format("{0}/rest/api/{1}/", _serverUrl, _apiVersion);
            _restClient = new RestClient
                {
                    BaseUrl = _baseUrl,
                    Authenticator = new HttpBasicAuthenticator(_username, _password)
                };
        }

        /// <summary>
        /// Construct the JiraRestClient, with the API Version defaulting to "latest"
        /// </summary>
        /// <param name="serverUrl">The URL of the JIRA server</param>
        /// <param name="username">The JIRA username</param>
        /// <param name="password">The JIRA password</param>
        public JiraRestClient(string serverUrl, string username, string password) : this(serverUrl, username, password, null) { }

        #endregion

        /// <summary>
        /// GET a REST resource
        /// </summary>
        /// <param name="resource">The resource to GET specified as a relative URL off the REST API, e.g. "issue/JRA-10000"</param>
        /// <returns>The REST response</returns>
        public JiraRestResponse Get(string resource)
        {
            RestRequest request = new RestRequest
                {
                    Method = Method.GET,
                    Resource = resource
                };
            RestResponse response = _restClient.Execute(request);
            JiraRestResponse jiraResponse = new JiraRestResponse(this, response);
            return jiraResponse;
        }

        /// <summary>
        /// GET a JIRA issue
        /// </summary>
        /// <param name="issueKey">The issue key, e.g. "JRA-10000"</param>
        /// <returns>A read-only JiraIssue</returns>
        public JiraIssue GetIssue(string issueKey)
        {
            return new JiraIssue(Get("issue/" + issueKey));
        }
        
    }
}
