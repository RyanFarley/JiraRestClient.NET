using RestSharp;

namespace JiraRestClient
{
    public class JiraRestClient : IJiraRestClient
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

        #region Public Methods

        /// <summary>
        /// GET a REST resource
        /// </summary>
        /// <param name="resource">The resource to GET specified as a relative URL off the REST API, e.g. "issue/JRA-10000"</param>
        /// <returns>The REST response</returns>
        public IJiraRestResponse Get(string resource)
        {
            RestRequest request = new RestRequest
                {
                    Method = Method.GET,
                    Resource = CleanResource(resource)
                };
            RestResponse response = _restClient.Execute(request);
            IJiraRestResponse jiraResponse = new JiraRestResponse(this, response);
            return jiraResponse;
        }

        /// <summary>
        /// GET a JIRA issue
        /// </summary>
        /// <param name="issueKey">The issue key, e.g. "JRA-10000"</param>
        /// <returns>A read-only JiraIssue</returns>
        public IJiraIssue GetIssue(string issueKey)
        {
            return new JiraIssue(Get("issue/" + issueKey));
        }

        /// <summary>
        /// GET a JIRA issue type
        /// </summary>
        /// <param name="id">The ID of the issue type</param>
        /// <returns>A read-only JiraIssueType</returns>
        public IJiraIssueType GetIssueType(int id)
        {
            return new JiraIssueType(Get("issueType/" + id));
        }

        /// <summary>
        /// GET a JIRA issue type by the resource URL (either relative or absolute)
        /// </summary>
        /// <param name="resource">The REST resource of the issue type</param>
        /// <returns>A read-only JiraIssueType</returns>
        public IJiraIssueType GetIssueTypeByResource(string resource)
        {
            return new JiraIssueType(Get(resource));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Cleans the REST resource to be relative to the Base URL
        /// </summary>
        /// <param name="resource">The resource to be cleaned</param>
        /// <returns>The cleaned resource</returns>
        private string CleanResource(string resource)
        {
            // TODO: Validate the resource for absolute URL that is not part of _baseUrl

            // If being past in a "self" or full reference, strip the initial part off
            if (resource.StartsWith(_baseUrl))
            {
                resource = resource.Substring(_baseUrl.Length);
            }

            return resource;
        }

        #endregion

    }
}
