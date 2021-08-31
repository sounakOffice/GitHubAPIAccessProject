using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubApiAccess;

namespace GitHubApiTest
{
    [TestClass]
    public class GitHubUnitTest
    {
        [TestMethod]
        public void ValidGetAllCommitDetails()
        {
            // Arrange
            GitHubApiAccess.Model.InputParameter inputValues = new GitHubApiAccess.Model.InputParameter { UserName = "sounakOffice"
                , RepoName = "SampleRepo", UserToken = "validGiHubAccessToken" };

            bool expectedValue = true;
            bool actualValue = false;

            // Act
            using (var repositoryUtility =  RepositoryFactory.GetRepoHandler())
            {
                repositoryUtility.UserParameter = inputValues;

                actualValue = repositoryUtility.GetAllCommitComments();
            };

            // Assert
            Assert.AreEqual(expectedValue, actualValue,"Repository connected and Commit Details with Comments fetched successful.");
        }

        [TestMethod]
        public void FailToGetCommitDetails()
        {
            // Arrange
            GitHubApiAccess.Model.InputParameter inputValues = new GitHubApiAccess.Model.InputParameter
            {
                UserName = "Test",
                RepoName = "Test",
                UserToken = "Test"
            };

            bool expectedValue = true;
            bool actualValue = false;

            // Act
            using (var repositoryUtility = RepositoryFactory.GetRepoHandler())
            {
                repositoryUtility.UserParameter = inputValues;
                actualValue = repositoryUtility.GetAllCommitComments();
            };

            // Assert
            Assert.AreEqual(expectedValue, actualValue, "Repository connected and Commit Details with Comments fetch failed.");
        }
    }
}
