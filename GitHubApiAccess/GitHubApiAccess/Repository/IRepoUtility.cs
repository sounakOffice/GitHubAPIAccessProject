using GitHubApiAccess.Model;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApiAccess
{

   public interface IRepoUtility : IDisposable
    {
        /// <summary>
        /// This Interface property get and set the User input details in the UI.
        /// </summary>
        InputParameter UserParameter { get; set; }

        /// <summary>
        /// This Interface method defination created for the purpose of Read and Print all words from Commit Comment in sorted order with the occurence count.
        /// </summary>
        /// <returns>No return type</returns>
        bool GetAllCommitComments();

        /// <summary>
        /// This Interface method defination created for the purpose to Export all Word with occurence count to CSV File in the current application Folder. 
        /// </summary>
        /// <returns>No return type</returns>
        void ExportWordToCSV(string userChoice);
    }
}
