using log4net;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubApiAccess.Model;

namespace GitHubApiAccess
{
    ///<Summary>
    /// This class is the main GitHub repository handler class.
    ///</Summary>
    public class GitRepoHandler : IRepoUtility
    {
        private static GitHubClient clientDetail;
        private List<WordDetails> allWordsFromallCommits = null;
        private bool considerAllComments = false;
        private List<WordDetails> finalSortedResult = null;
        private StringBuilder csvOutPut;
        protected internal static ILog logger = LogManager.GetLogger("MainLog");
        private InputParameter inputDetails = null;

        /// <summary>
        /// This Property used to get a single Word as well as set a new single  Word.
        /// </summary>
        public InputParameter UserParameter
        {
            get
            {
                return inputDetails;
            }
            set
            {
                inputDetails = value;
            }
        }

        /// <summary>
        /// This Constructor initialize.
        /// </summary>
        /// <param name="wordElements">A collection of Word and there ASCII Details.</param>
        internal GitRepoHandler()
        {
            finalSortedResult = new List<WordDetails>();
            allWordsFromallCommits = new List<WordDetails>();
        }


        /// <summary>
        /// This method used to connect to the GitHub Repo using a proper User credential and Token Info.
        /// </summary>
        private bool AuthenticateTokenAndGetClient()
        {
            try
            {
                var productInformation = new ProductHeaderValue(inputDetails.UserName);
                var credentials = new Credentials(inputDetails.UserToken);
                clientDetail = new GitHubClient(productInformation) { Credentials = credentials };
                return true;
            }
            catch (Exception accessFailException)
            {
                logger.Error(string.Format("Exception source : {0} with Details : {1}", "AuthenticateTokenAndGetClient", accessFailException));
                clientDetail = null;
                return false;
            }
        }

        /// <summary>
        /// This method used to sort the words using a Binary Search Tree Algorithm.
        /// </summary>
        /// <param name="words">A collection of Words.</param>
        /// <returns>Return a sorted collection of Words.</returns>
        private List<WordDetails> SortWord(List<WordDetails> words)
        {
            List<WordDetails> sortedDestinationElements = null;
            try
            {
                if (words != null && words.Count > 0)
                {
                    using (BinarySearchTree treeSort = new BinarySearchTree(words))
                    {
                        sortedDestinationElements = treeSort.getSortedArray;
                    }
                }
            }
            catch (Exception singleCommentWordSortException)
            {
                logger.Error(string.Format("Exception source : {0} Exception Details : {1}", "SortWord", singleCommentWordSortException));
            }

            return sortedDestinationElements;
        }


        /// <summary>
        /// This method used to Search for all Commit Comments and split Comment in Words.
        /// Apply sort on single comment Word or on all comment words.
        /// Count the Occurance of Words in Comment
        /// Prepare a CSV output which will use for CSV Export as well.
        /// </summary>
        /// <returns>Return an asynchronus operation.</returns>
        private async Task FetchAllCommitComments()
        {
            if (AuthenticateTokenAndGetClient())
            {
                if (clientDetail != null)
                {
                    try
                    {
                        // Retrive all the commits comment for the GHE repo
                        IReadOnlyList<GitHubCommit> allCommits = await clientDetail.Repository.Commit.GetAll(inputDetails.UserName, inputDetails.RepoName);

                        if (allCommits != null && allCommits.Count > 0)
                        {
                            // Clear the List
                            finalSortedResult.Clear();

                            // Iterate over each commit comment
                            foreach (GitHubCommit item in allCommits)
                            {
                                CommentDetails singleComment = new CommentDetails() { RepoComment = item.Commit.Message, RepoCommentID = item.Commit.Sha };
                                if (singleComment != null)
                                {
                                    if (!considerAllComments)
                                    {
                                        // Sort the individual Word of every Comment using BST
                                        finalSortedResult.AddRange(SortWord(singleComment.Words));
                                    }

                                    // Add all comment words to a global collection
                                    allWordsFromallCommits.AddRange(singleComment.Words);
                                }
                            };

                            if (considerAllComments)
                            {
                                // Sort all Word of all Comment using BST
                                // Add all to the final Dictionary
                                finalSortedResult.AddRange(SortWord(allWordsFromallCommits));
                            }

                            // Count the Occurance of Words
                            List<FinalWordSet> wordOccurance = GetWordWiseOccurenceCount(allWordsFromallCommits);
                            csvOutPut = PrintAndOutPutWord(wordOccurance);

                        }
                        else
                        {
                            Console.WriteLine("No Commit Comments found in this Repository.");
                        }
                    }
                    
                    catch (Exception fetchCommitException)
                    {
                        if (fetchCommitException is ApiException)
                        {
                            Console.WriteLine(fetchCommitException.Message);
                        }
                        else
                        {
                            Console.WriteLine("Authentication failed. Please try again.");
                        }

                        logger.Error(string.Format("Exception source : {0} Exception Details : {1}", "FetchAllCommitComments", fetchCommitException));
                        throw;
                    }
                }
            }
            else
            {
                Console.WriteLine("Authentication failed. Please try again.");
                throw new Exception("Authentication failed. Please try again.");
            }
        }

        /// <summary>
        /// This method used to Print all Words and there occurance in UI screen.
        /// </summary>
        /// <returns>Returns a string collection which helps to create the content of the CSV File.</returns>
        private StringBuilder PrintAndOutPutWord(List<FinalWordSet> FinalResultSet)
        {
            StringBuilder exportOutput = new StringBuilder();

            try
            {
                if (FinalResultSet != null)
                {
                    exportOutput.AppendLine(string.Join(",", "Word", "OccuranceCount"));
                    foreach (WordDetails item in finalSortedResult)
                    {
                        FinalWordSet fetchedWord = FinalResultSet.Where(wo => wo.Word == item.Word).FirstOrDefault();

                        if (fetchedWord != null)
                        {
                            exportOutput.AppendLine(string.Join(",", fetchedWord.Word, fetchedWord.OccuranceCount));
                            Console.WriteLine(string.Join(Environment.NewLine, fetchedWord.WordPrint));
                        }
                        else
                        {
                            exportOutput.AppendLine(string.Join(",", item.Word, 0));
                            Console.WriteLine(string.Join(Environment.NewLine, $" Word : {item.Word}, ASCII : {item.WordWiseAsciiTotal}, Occurrence : 0"));
                        }
                    }
                }
            }
            catch (Exception printException)
            {
                logger.Error(string.Format("Exception source : {0} Exception Details : {1}", "PrintAndOutPutWord", printException));
            }

            return exportOutput;
        }

        /// <summary>
        /// This method used to calculate occurance of each word in all Commit Comments.
        /// </summary>
        /// <returns>Returns a final Wordset Collection.</returns>
        private List<FinalWordSet> GetWordWiseOccurenceCount(List<WordDetails> wordWiseData)
        {
            try
            {
                return wordWiseData.GroupBy(commitData => new { commitData.Word, commitData.WordWiseAsciiTotal })
                            .Select(wordSet => new FinalWordSet
                            {
                                Word = wordSet.Key.Word,
                                OccuranceCount = wordSet.Count(),
                                WordPrint = $" Word : {wordSet.Key.Word}, ASCII : {wordSet.Key.WordWiseAsciiTotal}, Occurrence : {wordSet.Count()}"
                            }).ToList();
            }
            catch (Exception wordCountException)
            {
                logger.Error(string.Format("Exception source : {0} Exception Details : {1}", "GetWordWiseOccurenceCount", wordCountException));
                return null;
            }
        }

        /// <summary>
        /// This method output the wordwise occurance from Commit Comments in UI.
        /// </summary>
        /// <returns>Returns True or False.</returns>
        public bool GetAllCommitComments()
        {
            try
            {
                FetchAllCommitComments().GetAwaiter().GetResult();
                return true;
            }
            catch (Exception getCommitException)
            {
                logger.Error(string.Format("Exception source : {0} Exception Details : {1}", "GetAllCommitComments", getCommitException));
                return false;
            }
        }

        /// <summary>
        /// This method used to dispose all declared varriables once the class done it's job.
        /// </summary>
        public void Dispose()
        {
            if (clientDetail != null)
            {
                clientDetail = null;
            }

            if (allWordsFromallCommits != null)
            {
                allWordsFromallCommits.Clear();
                allWordsFromallCommits = null;
            }
            if (finalSortedResult != null)
            {
                finalSortedResult.Clear();
                finalSortedResult = null;
            }

            if (csvOutPut != null)
            {
                csvOutPut.Clear();
                csvOutPut = null;
            }

            logger = null;

        }

        /// <summary>
        /// This method used to Export the Word Details to a CSV file.
        /// </summary>
        public void ExportWordToCSV(string userChoice)
        {
            try
            {
                if (userChoice.ToUpper().Trim().Equals("YES"))
                {
                    if (csvOutPut != null && csvOutPut.Length > 0)
                    {
                        // Get The Current Directory for File Path
                        string filePath = Path.Combine(Environment.CurrentDirectory, "WordDetails" + ".csv");

                        //File location, where the .csv goes and gets stored.
                        File.WriteAllText(filePath, csvOutPut.ToString());
                        Console.WriteLine(string.Format("File created and exported to path: {0}", filePath));
                    }
                }
                else
                {
                    Console.WriteLine("No Export operation executed.");
                }
            }
            catch (Exception exportException)
            {
                logger.Error(string.Format("Exception source : {0} Exception Details : {1}", "ExportWordToCSV", exportException));
            }
        }
    }
}
