using System.Collections.Generic;

namespace JiraRestClient
{
    /// <summary>
    /// A read-only wrapper around the JSON data returned for a JIRA issue
    /// </summary>
    /// <remarks>
    /// This class can be easily expanded to provide more fields by examining the JSON results of an issue query
    /// and following the patterns in the other properties below.
    /// </remarks>
    public class JiraIssue: JsonWrapper
    {
        #region Private Fields

        /// <summary>
        /// The JiraRestClient used in the original request; used to make subsequent calls
        /// </summary>
        private readonly JiraRestClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a JiraIssue from a JiraRestResponse
        /// </summary>
        /// <param name="jiraResponse">The REST response</param>
        public JiraIssue(JiraRestResponse jiraResponse) : base(jiraResponse.JObject)
        {
            JiraResponse = jiraResponse;
            _client = JiraResponse.JiraRestClient;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The underlying REST response that makes up this issue
        /// </summary>
        public JiraRestResponse JiraResponse { get; private set; }

        /// <summary>
        /// The issue key
        /// </summary>
        public string Key { get { return Get<string>("Key", "key"); } }

        /// <summary>
        /// The issue summary
        /// </summary>
        public string Summary { get { return Get<string>("Summary", "fields", "summary", "value"); } }

        /// <summary>
        /// The list of labels assigned to this issue
        /// </summary>
        public IEnumerable<string> Labels { get { return GetList<string>("Labels", "fields", "labels", "value"); } }

        /// <summary>
        /// The issue key of the parent issue
        /// </summary>
        public string Parent { get { return Get<string>("Parent", "fields", "parent", "value", "issueKey"); } }

        /// <summary>
        /// The JiraIssue object of the parent issue
        /// </summary>
        public JiraIssue ParentObject { get { return Get("ParentObject", () => _client.GetIssue(Parent)); } }

        /// <summary>
        /// Is this issue a sub-task?
        /// </summary>
        public bool IsSubtask { get { return Get<bool>("IsSubTask", "fields", "issuetype", "value", "subtask"); } }

        #endregion
    }
}
