using System;
using System.Collections.Generic;
namespace JiraRestClient
{
    public interface IJiraIssue: IJiraObjectBase
    {
        bool IsSubtask { get; }
        string IssueType { get; }
        IJiraIssueType IssueTypeObject { get; }
        string Key { get; }
        IEnumerable<string> Labels { get; }
        string Parent { get; }
        IJiraIssue ParentObject { get; }
        string Summary { get; }
    }
}
