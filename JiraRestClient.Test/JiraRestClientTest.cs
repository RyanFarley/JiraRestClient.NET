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
            JiraRestClient client = new JiraRestClient("http://jira.atlassian.com", "jirarestclientnet", "jirarestclientnet");

            JiraIssue issue = client.GetIssue("JRA-25592");

            Assert.AreEqual("JRA-25592", issue.Key);
            Assert.IsFalse(issue.IsSubtask);

            IEnumerable<string> expectedLabels = new List<string> { "advisory", "security" };
            IEnumerable<string> actualLabels = from label in issue.Labels orderby label select label;

            CollectionAssert.AreEqual(expectedLabels, actualLabels);
        }
    }
}
