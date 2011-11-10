using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiraRestClient
{
    public class JiraIssueType: JiraObjectBase, IJiraIssueType
    {
        #region Constructors

        /// <summary>
        /// Construct a JiraIssue from a JiraRestResponse
        /// </summary>
        /// <param name="jiraResponse">The REST response</param>
        public JiraIssueType(IJiraRestResponse jiraResponse) : base(jiraResponse) { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Issue type name
        /// </summary>
        public string Name { get { return Get<string>("Name", "name"); } }

        /// <summary>
        /// Issue type description
        /// </summary>
        public string Description { get { return Get<string>("Description", "description"); } }

        /// <summary>
        /// Issue type icon URL
        /// </summary>
        public string IconUrl { get { return Get<string>("IconUrl", "iconUrl"); } }

        /// <summary>
        /// Is this issue type a sub-task?
        /// </summary>
        public bool IsSubtask { get { return Get<bool>("IsSubtask", "subtask"); } }

        #endregion
    }
}
