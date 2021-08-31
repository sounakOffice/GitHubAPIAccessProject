using System;
using System.Collections.Generic;

namespace GitHubApiAccess
{
    ///<Summary>
    /// This class use to create a Binary Tree and apply sorting on the Words based on there ASCII values.
    ///</Summary>
    class BinarySearchTree : IDisposable
    {
        Node root;
        List<WordDetails> arrayElements = null;
        List<WordDetails> sortedElements = null;

        /// <summary>
        /// This Property used to get the final sorted list of Words.
        /// </summary>
        public List<WordDetails> getSortedArray
        {
            get
            {
                Treeins(arrayElements);
                if (root != null)
                {
                    sortedElements = new List<WordDetails>();
                    InorderRec(root);
                }
                return sortedElements;
            }
        }

        /// <summary>
        /// This Constructor initialize Word Details to perform Sort Operation.
        /// </summary>
        /// <param name="wordElements">A collection of Word and there ASCII Details.</param>
        public BinarySearchTree(List<WordDetails> wordElements)
        {
            root = null;
            arrayElements = wordElements;
        }


        /// <summary>
        /// This method mainly calls insertRec().
        /// </summary>
        /// <param name="singleWordInfo">A single Word.</param>
        /// <returns>No return type</returns>
        void Insert(WordDetails singleWordInfo)
        {
            root = InsertRec(root, singleWordInfo);
        }

        /// <summary>
        /// This method is a recursive function to insert a new key in BST.
        /// </summary>
        /// <param name="root">A Node object consider as Root of the Tree.</param>
        /// <param name="singleWordInfo">A single Word.</param>
        /// <returns>A Node Object</returns>
        Node InsertRec(Node root, WordDetails singleWordInfo)
        {

            /* If the tree is empty,
                return a new node */
            if (root == null)
            {
                root = new Node(singleWordInfo);
                return root;
            }

            /* Otherwise, recur
                down the tree */
            if (singleWordInfo.WordWiseAsciiTotal < root.key)
                root.left = InsertRec(root.left, singleWordInfo);
            else if (singleWordInfo.WordWiseAsciiTotal > root.key)
                root.right = InsertRec(root.right, singleWordInfo);

            /* return the root */
            return root;
        }

        /// <summary>
        /// This method is a recursive to order the words based on BST.
        /// </summary>
        /// <param name="root">A Node object consider as Root of the Tree.</param>
        /// <returns>No return type</returns>
        void InorderRec(Node root)
        {
            if (root != null)
            {
                InorderRec(root.left);
                sortedElements.Add(root.wordInfo);
                InorderRec(root.right);
            }
        }

        /// <summary>
        /// This method loop on the availble word details and initiate a Binary Tree.
        /// </summary>
        /// <param name="wordElements">A collection of Word and there ASCII Details.</param>
        /// <returns>No return type</returns>
        void Treeins(List<WordDetails> wordElements)
        {
            for (int count = 0; count < wordElements.Count; count++)
            {
                Insert(wordElements[count]);
            }
        }

        /// <summary>
        /// This method used to dispose all declared varriables once the class done it's job.
        /// </summary>
        public void Dispose()
        {
            root = null;
            arrayElements = null;
        }
    }

    ///<Summary>
    /// This class use to create a Node of a BST.
    ///</Summary>
    class Node
    {
        internal int key;
        internal Node left, right;
        internal WordDetails wordInfo;

        /// <summary>
        /// This Constructor add Word Details to consider it as a Node of the BST.
        /// </summary>
        /// <param name="singleWord">A single word keeping all the additional details of word too.</param>
        public Node(WordDetails singleWord)
        {
            key = singleWord.WordWiseAsciiTotal;
            wordInfo = singleWord;
            left = right = null;
        }
    }
}
