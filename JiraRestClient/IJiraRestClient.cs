using System;
namespace JiraRestClient
{
    public interface IJiraRestClient
    {
        IJiraRestResponse Get(string resource);
        IJiraIssue GetIssue(string issueKey);
        IJiraIssueType GetIssueType(int id);
        IJiraIssueType GetIssueTypeByResource(string resource);
    }
}
