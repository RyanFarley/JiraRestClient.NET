using System;
using System.Collections.Generic;
namespace JiraRestClient
{
    public interface IJiraObjectBase
    {
        IEnumerable<string> ErrorMessages { get; }
        IJiraRestResponse JiraResponse { get; }
        IJiraRestClient JiraRestClient { get; }
        string Self { get; }
    }
}
