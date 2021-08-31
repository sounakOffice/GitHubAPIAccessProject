using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApiAccess.Model
{
   public class InputParameter
    {
        string userName = string.Empty;
        string userToken = string.Empty;
        string userRepoName = string.Empty;

        /// <summary>
        /// This property used to get and set the User Name.
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        /// <summary>
        /// This property used to get and set the User Token.
        /// </summary>
        public string UserToken
        {
            get
            {
                return userToken;
            }
            set
            {
                userToken = value;
            }
        }

        /// <summary>
        /// This property used to get and set the Repository Folder Name.
        /// </summary>
        public string RepoName
        {
            get
            {
                return userRepoName;
            }
            set
            {
                userRepoName = value;
            }
        }

        public string ValidateUserInput(ValidationType validateType)
        {
            string validationMessage = string.Empty;

            if (validateType==ValidationType.All || validateType==ValidationType.UserName)
            {
                if (string.IsNullOrEmpty(userName))
                {
                    validationMessage = "User Name cannot be blank.";

                }
            }

            if (validateType == ValidationType.All || validateType == ValidationType.Token)
            {
                if (string.IsNullOrEmpty(userToken))
                {
                    validationMessage = "Token cannot be blank.";

                }
            }

            if (validateType == ValidationType.All || validateType == ValidationType.RepoName)
            {
                if (string.IsNullOrEmpty(userRepoName))
                {
                    validationMessage = "Repository Name cannot be blank.";

                }
            }

            return validationMessage;
        }

       public enum ValidationType
        {
            UserName=1,
            Token=2,
            RepoName=3,
            All=4
        }
    }
}
