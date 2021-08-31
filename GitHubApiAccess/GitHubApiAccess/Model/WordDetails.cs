using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApiAccess
{
    ///<Summary>
    /// This class use to keep Word Details such as Word, ASCII value collection of characters belong to Word and Sum value of ASCII word wise
    ///</Summary>
    class WordDetails : IDisposable
    {
        
        string word = string.Empty;
        List<int> totalAscii = null;
        int wordWiseAsciiTotal = 0;
        
        /// <summary>
        /// This Property used to get a single Word as well as set a new single  Word.
        /// </summary>
        internal string Word
        {
            get
            {
                return word;
            }
            set
            {
                word = value;
                FetchAsciiValue(word);
            }
        }

        /// <summary>
        /// This Property used to get collection of ASCII value characters for a single Word.
        /// </summary>
        internal List<int> AsciiValues
        {
            get
            {
                return totalAscii;
            }
        }
        

        /// <summary>
        /// This Property used to get sum of ASCII value of all characters for a single Word.
        /// </summary>
        internal int WordWiseAsciiTotal
        {
            get
            {
                return wordWiseAsciiTotal;
            }
        }

        /// <summary>
        /// This method used to dispose all declared varriables once the class done it's job.
        /// </summary>
        public void Dispose()
        {
            if (totalAscii != null)
            {
                totalAscii = null;
            }

            word = null;
        }

        /// <summary>
        /// This method used to calculate all ASCII value based on character belongs to a single word. Doing a total sum of ASCII values also.
        /// </summary>
        /// <param name="singleWord">A single Word.</param>
        /// <returns>No return type</returns>
        private void FetchAsciiValue(string singleWord)
        {
            byte[] ASCIIvalues = null;
            try
            {
                if (!string.IsNullOrEmpty(singleWord))
                {
                    totalAscii = new List<int>();
                    ASCIIvalues = Encoding.ASCII.GetBytes(singleWord);
                    totalAscii.AddRange(Array.ConvertAll(ASCIIvalues, objChar => (int)objChar));
                    wordWiseAsciiTotal = totalAscii.Sum();
                }
            }
            catch (Exception FetchAsciiValueException)
            {
                GitRepoHandler.logger.Error(string.Format("Exception source : {0} Exception Details : {1}", "FetchAsciiValue", FetchAsciiValueException));
                Console.WriteLine(string.Format("Cannot generate ASCII values for : {0}", singleWord));
            }
            finally
            {
                if (ASCIIvalues != null)
                {
                    ASCIIvalues = null;
                }
            }
        }

    }
}

