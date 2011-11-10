using System.Collections.Generic;

namespace JiraRestClient
{
    /// <summary>
    /// A read-only wrapper around the JSON data returned for a JIRA issue
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class can be easily expanded to provide more fields by examining the JSON results of an issue query
    /// and following the patterns in the other properties below.
    /// </para>
    /// <para>
    /// To view JSON output from the JIRA REST API, you can conveniently do the following for any REST request:
    /// </para>
    /// <para>
    /// If you have an account at http://jira.atlassian.com, log in from a browser and then in the same browser use
    /// a URL similar to the following to get the JSON.
    /// </para>
    /// <para>
    /// https://jira.atlassian.com/rest/api/latest/issue/JRA-9
    /// </para>
    /// <para>
    /// If you have Chrome and the JSON Formatter extension (https://chrome.google.com/webstore/detail/bcjindcccaagfpapjjmafapmmgkkhgoa)
    /// or something similar, it makes viewing the JSON much better.
    /// </para>
    /// </remarks>
    public class JiraIssue: JiraObjectBase, IJiraIssue
    {
        #region Constructors

        /// <summary>
        /// Construct a JiraIssue from a JiraRestResponse
        /// </summary>
        /// <param name="jiraResponse">The REST response</param>
        public JiraIssue(IJiraRestResponse jiraResponse) : base(jiraResponse) { }

        #endregion

        #region Public Properties

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
        public IJiraIssue ParentObject { get { return Get("ParentObject", () => JiraRestClient.GetIssue(Parent)); } }

        /// <summary>
        /// Is this issue a sub-task?
        /// </summary>
        public bool IsSubtask { get { return Get<bool>("IsSubTask", "fields", "issuetype", "value", "subtask"); } }

        /// <summary>
        /// The issue type
        /// </summary>
        public string IssueType { get { return Get<string>("IssueType", "fields", "issuetype", "value", "name"); } }

        /// <summary>
        /// The JiraIssueType object of the issue
        /// </summary>
        public IJiraIssueType IssueTypeObject 
        {
            get 
            {
                string issueTypeSelf = Get<string>("IssueTypeSelf", "fields", "issuetype", "value", "self");
                return Get("IssueTypeObject", () => JiraRestClient.GetIssueTypeByResource(issueTypeSelf)); 
            } 
        }

        #endregion
    }
}
