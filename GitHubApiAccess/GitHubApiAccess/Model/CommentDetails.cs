using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApiAccess
{
    ///<Summary>
    /// This class use to keep comments of a repository and there corresponding Word details.
    ///</Summary>
    class CommentDetails : IDisposable
    {

        string givenComment = string.Empty;
        List<WordDetails> wordList = null;
        char[] arrSplitChars = { ' ' };


        /// <summary>
        /// This Property used to get and set a unique ID for each Comment.
        /// </summary>
        internal string RepoCommentID { get; set; }

        /// <summary>
        /// This Property used to get and set the Comment and also calculate Words details based on a comment.
        /// </summary>
        internal string RepoComment
        {
            get
            {
                return givenComment;
            }
            set
            {
                givenComment = value;
                GetWordsFromComment(givenComment);
            }
        }

        /// <summary>
        /// This Property used to get Words details based on a comment.
        /// </summary>
        internal List<WordDetails> Words
        {
            get
            {
                return wordList;
            }
        }

        /// <summary>
        /// This method used to dispose all declared varriables once the class done it's job.
        /// </summary>
        public void Dispose()
        {
            if (wordList != null)
            {
                wordList = null;
            }

            if (arrSplitChars != null)
            {
                arrSplitChars = null;
            }
        }


        /// <summary>
        /// This method used to get Word details belong to a Comment and generate the words by searching space between a comment.
        /// </summary>
        /// <param name="singleWord">A single Word.</param>
        /// <returns>No return type</returns>
        private void GetWordsFromComment(string commentDetail)
        {
            try
            {
                if (!string.IsNullOrEmpty(commentDetail))
                {
                    wordList = new List<WordDetails>();
                    wordList.AddRange(commentDetail.Split(arrSplitChars, StringSplitOptions.RemoveEmptyEntries).Select(singleWord => new WordDetails() { Word = singleWord }));
                }
            }
            catch (Exception wordFetchException)
            {
                GitRepoHandler.logger.Error(string.Format("Exception source : {0} Exception Details : {1}", "GetWordsFromComment", wordFetchException));
                Console.WriteLine("Unable to fetch words from Commit comment.");
            }
        }
    }
}
