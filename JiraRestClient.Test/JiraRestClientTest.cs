using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace JiraRestClient.UnitTest
{
    [TestFixture]
    public class JiraRestClientTest
    {
        [Test]
        public void OneTest()
        {
            IJiraRestClient client = new JiraRestClient("https://jira.atlassian.com", "jirarestclientnet", "jirarestclientnet");

            IJiraIssue issue = client.GetIssue("JRA-25592");

            Assert.AreEqual("JRA-25592", issue.Key);
            Assert.IsFalse(issue.IsSubtask);

            IEnumerable<string> expectedLabels = new List<string> { "advisory", "security" };
            IEnumerable<string> actualLabels = from label in issue.Labels orderby label select label;

            CollectionAssert.AreEqual(expectedLabels, actualLabels);

            Assert.AreEqual("Bug", issue.IssueType);
            Assert.AreEqual("Bug", issue.IssueTypeObject.Name);
        }
    }
}
