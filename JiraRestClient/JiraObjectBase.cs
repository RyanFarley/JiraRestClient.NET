using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiraRestClient
{
    /// <summary>
    /// Base class for JIRA objects
    /// </summary>
    abstract public class JiraObjectBase: JsonWrapper, IJiraObjectBase
    {
        #region Constructors

        /// <summary>
        /// Construct a Jira object from a JiraRestResponse
        /// </summary>
        /// <param name="jiraResponse">The REST response</param>
        protected JiraObjectBase(IJiraRestResponse jiraResponse)
            : base(jiraResponse.JObject)
        {
            JiraResponse = jiraResponse;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The underlying REST response that makes up this object
        /// </summary>
        public IJiraRestResponse JiraResponse { get; private set; }

        /// <summary>
        /// The original client object used to make this request.
        /// </summary>
        public IJiraRestClient JiraRestClient { get { return JiraResponse.JiraRestClient; } }

        /// <summary>
        /// The REST resource pointer to this object
        /// </summary>
        public string Self { get { return Get<string>("Self", "self"); } }

        /// <summary>
        /// Error messages returned from JIRA when making this request.
        /// </summary>
        public IEnumerable<string> ErrorMessages { get { return GetList<string>("ErrorMessages", "errorMessages"); } }

        #endregion

    }
}
