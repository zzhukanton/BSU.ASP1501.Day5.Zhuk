using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Logic
{
    /// <summary>
    /// Interface for working with binary repository with list of books
    /// </summary>
    interface IBookListService
    {
        /// <summary>
        /// Adds book in repository
        /// </summary>
        /// <param name="book">Some new book</param>
        void AddBook(Book book);

        /// <summary>
        /// Removes book from repository
        /// </summary>
        /// <param name="book">Some book to remove</param>
        void RemoveBook(Book book);

        /// <summary>
        /// Method for sorting books in some way
        /// </summary>
        void SortBooksByTag();

        /// <summary>
        /// Finds some book depending on function 
        /// </summary>
        /// <param name="function">Condition to find book</param>
        /// <returns>Book</returns>
        Book FindBookByTag(Func<Book, bool> function);
    }
}
