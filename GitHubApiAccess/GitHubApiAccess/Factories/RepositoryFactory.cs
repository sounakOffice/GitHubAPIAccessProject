using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitHubApiAccess
{
    ///<Summary>
    /// This is a Factory class use as an abstraction between the UI and the buisness Logic Layer.
    ///</Summary>
    public class RepositoryFactory
    {
        public static IRepoUtility repositoryUtility = null;


        /// <summary>
        /// This method used to map the object of the handler class to the interface.
        /// </summary>
        public static IRepoUtility GetRepoHandler()
        {
            GitRepoHandler userInputDetails = new GitRepoHandler();
            repositoryUtility = userInputDetails;
            return repositoryUtility;
        }
    }
}
