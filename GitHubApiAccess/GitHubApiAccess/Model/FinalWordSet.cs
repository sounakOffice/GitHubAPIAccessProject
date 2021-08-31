using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApiAccess
{
    ///<Summary>
    /// This class use to keep main content of the Word, the OccuranceCount of the word and the print format.
    ///</Summary>
    class FinalWordSet
    {
        string word = string.Empty;
        int wordOccurrence = 0;
        string wordPrint = string.Empty;

        /// <summary>
        /// This Property used to get a single Word as well as set a new single Word.
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
            }
        }

        /// <summary>
        /// This Property used to get and set Occurance count of a single word.
        /// </summary>
        internal int OccuranceCount
        {
            get
            {
                return wordOccurrence;
            }
            set
            {
                wordOccurrence = value;
            }
        }

        /// <summary>
        /// This Property used to print a Word with Occurance in a printable format.
        /// </summary>
        internal string WordPrint
        {
            get
            {
                return wordPrint;
            }
            set
            {
                wordPrint = value;
            }
        }
    }
}
