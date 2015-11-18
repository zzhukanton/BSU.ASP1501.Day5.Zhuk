using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.IO;
using NLog;

namespace Logic
{
    /// <summary>
    /// Class witch contains methods for working with binary repositiory of list of books
    /// </summary>
    public class BookList
    {
        /// <summary>
        /// Internal storage of repostory of books
        /// </summary>
        private List<Book> books;

        /// <summary>
        /// Repository with books
        /// </summary>
        private IRepository repository;

        /// <summary>
        /// Logger object
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor with user's way keeping
        /// </summary>
        /// <param name="repository">Object describing storage kind</param>
        public BookList(IRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("Repository is undefined");
            books = new List<Book>();
            this.repository = repository;
        }

        /// <summary>
        /// Dedault constructor (uses file as a repository) 
        /// </summary>
        public BookList()
        {
            books = new List<Book>();
            this.repository = new FileRepository();
        }

        /// <summary>
        /// Adds new book in repository(if it was not there)
        /// </summary>
        /// <param name="book">Some new book</param>
        public void AddBook(Book book)
        {
            try
            {
                if (book == null)
                    throw new ArgumentNullException("Book is null");

                books = repository.LoadToList();
                if (books.Contains(book))
                    throw new ArgumentException("Book is already in booklist");
                else
                {
                    books.Add(book);
                    logger.Info("Book was added successfully");
                    repository.LoadToFile(books);
                }
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        /// <summary>
        /// Removes some book from repository (if this book was there)
        /// </summary>
        /// <param name="book">Some book to remove</param>
        public void RemoveBook(Book book)
        {
            try
            {
                if (book == null)
                    throw new ArgumentException("Book is null");

                books = repository.LoadToList();
                if (!books.Contains(book))
                    throw new ArgumentException("There is no this book in booklist");
                else
                {
                    books.Remove(book);
                    logger.Info("Book was removed successfully");
                    repository.LoadToFile(books);
                }
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        /// <summary>
        /// Finds some book depending on function 
        /// </summary>
        /// <param name="function">Condition to find book</param>
        /// <returns>Book</returns>
        public Book FindBookByTag(Func<Book, bool> function)
        {
            Book result = null;

            try
            {
                if (function == null)
                {
                    logger.Error("Error while find book by tag");
                    throw new ArgumentNullException("Tag is null");
                }
                books = repository.LoadToList();
                result = books.First(function);
                logger.Info("Book was found successfully");
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Method for sorting books in some way
        /// </summary>
        public void SortBooksByTag()
        {
            try
            {
                books = repository.LoadToList();
                books.Sort(Comparer<Book>.Default);
                logger.Info("Sorted successfully");
                repository.LoadToFile(books);
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        /// <summary>
        /// Method for sorting books in the way depending on comparer
        /// </summary>
        /// <param name="comparer">IComparer object</param>
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            try
            {
                if (comparer == null)
                {
                    logger.Error("Error while sorting with comparer");
                    throw new ArgumentNullException("Comparer is null");
                }
                books = repository.LoadToList();
                books.Sort(comparer);
                logger.Info("Sorted successfully");
                repository.LoadToFile(books);
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (Book book in books)
            {
                result.Append(book.ToString() + "\n");
            }

            return result.ToString();
        }

        /// <summary>
        /// Checks if booklist has such book
        /// </summary>
        /// <param name="book">Book to check</param>
        /// <returns>True if book is already in booklist, else false</returns>
        private bool Contains(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("Book is null");
            foreach (Book b in books)
            {
                if (b.Equals(book))
                    return true;
            }
            return false;
        }
    }
}
