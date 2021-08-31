using GitHubApiAccess.Model;
using System;

namespace GitHubApiAccess
{
    class Program
    {

        static void Main(string[] args)
        {

            InputParameter givenValue = new InputParameter();
            string validationMessage = string.Empty;

            // Set User Name
            Console.WriteLine("Enter User Name:");

            do
            {
                givenValue.UserName = Console.ReadLine();
                validationMessage = givenValue.ValidateUserInput(InputParameter.ValidationType.UserName);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    Console.WriteLine(validationMessage);
                }

            } while (!string.IsNullOrEmpty(validationMessage));


            // Set Token
            Console.WriteLine("\n");
            Console.WriteLine("Enter Token Details:");

            do
            {
                givenValue.UserToken = Console.ReadLine();
                validationMessage = givenValue.ValidateUserInput(InputParameter.ValidationType.Token);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    Console.WriteLine(validationMessage);
                }

            } while (!string.IsNullOrEmpty(validationMessage));


            // Set Repository Name
            Console.WriteLine("\n");
            Console.WriteLine("Enter Repository Name:");
            
            do
            {
                givenValue.RepoName = Console.ReadLine();
                validationMessage = givenValue.ValidateUserInput(InputParameter.ValidationType.RepoName);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    Console.WriteLine(validationMessage);
                }

            } while (!string.IsNullOrEmpty(validationMessage));



            using (var repositoryUtility = RepositoryFactory.GetRepoHandler())
            {
                Console.WriteLine("\n");
                repositoryUtility.UserParameter = givenValue;

                if (repositoryUtility.GetAllCommitComments())
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Do you want to export to CSV File (Yes / No)?");
                    string userChoice = Console.ReadLine();
                    repositoryUtility.ExportWordToCSV(userChoice);
                }
            };

            givenValue = null;
            Console.WriteLine("\n");
            Console.WriteLine("Press Enter Key to exit the Application.");
            Console.ReadLine();
        }
    }
}
