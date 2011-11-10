using System;
namespace JiraRestClient
{
    public interface IJiraIssueType: IJiraObjectBase
    {
        string Description { get; }
        string IconUrl { get; }
        bool IsSubtask { get; }
        string Name { get; }
    }
}
