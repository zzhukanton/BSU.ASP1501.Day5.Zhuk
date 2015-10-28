using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.IO;

namespace Logic
{
    /// <summary>
    /// Class witch contains methods for working with binary repositiory of list of books
    /// </summary>
    public class BookList: IBookListService
    {
        /// <summary>
        /// Internal storage of repostory of books
        /// </summary>
        private List<Book> books;

        /// <summary>
        /// Path to repository file
        /// </summary>
        private readonly string filePath;

        public BookList()
        {
            books = new List<Book>();
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.bin");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
            }
        }

        /// <summary>
        /// Adds new book in repository(if it was not there)
        /// </summary>
        /// <param name="book">Some new book</param>
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("Book is null");

            LoadToList();
            if (books.Contains(book))
                throw new ArgumentException("Book is already in booklist");
            else
            {
                books.Add(book);
                LoadToFile();
            }
        }

        /// <summary>
        /// Removes some book from repository (if this book was there)
        /// </summary>
        /// <param name="book">Some book to remove</param>
        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book is null");

            LoadToList();
            if (!books.Contains(book))
                throw new ArgumentException("There is no this book in booklist");
            else
            {
                books.Remove(book);
                LoadToFile();
            }
        }

        /// <summary>
        /// Finds some book depending on function 
        /// </summary>
        /// <param name="function">Condition to find book</param>
        /// <returns>Book</returns>
        public Book FindBookByTag(Func<Book, bool> function)
        {
            if (function == null)
                throw new ArgumentNullException("Tag is null");

            LoadToList();
            Book result = books.First(function);

            return result;
        }

        /// <summary>
        /// Method for sorting books in some way
        /// </summary>
        public void SortBooksByTag()
        {
            LoadToList();
            books.Sort(Comparer<Book>.Default);
            LoadToFile();
        }

        /// <summary>
        /// Method for sorting books in the way depending on comparer
        /// </summary>
        /// <param name="comparer">IComparer object</param>
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("Comparer is null");
            LoadToList();
            books.Sort(comparer);
            LoadToFile();
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

        /// <summary>
        /// Method that "synchronize" internal list of books with binary repository,
        /// reads the repository and writes values in internal list
        /// </summary>
        private void LoadToList()
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.OpenRead(filePath)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        Book b = new Book();
                        b.Author = reader.ReadString();
                        b.Title = reader.ReadString();
                        b.Publiser = reader.ReadString();
                        b.NumberOfPages = reader.ReadInt32();
                        b.Year = reader.ReadInt32();
                        books.Add(b);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("File not found");
            }
            catch (IOException)
            {
                throw new InvalidOperationException("Cannot load file");
            }
        }

        /// <summary>
        /// Method that "synchronize" internal list of books with binary repository,
        /// writes the internal list of books to binary repository
        /// </summary>
        private void LoadToFile()
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Create(filePath)))
                {
                    foreach (Book book in books)
                    {
                        writer.Write(book.Title);
                        writer.Write(book.Author);
                        writer.Write(book.Publiser);
                        writer.Write(book.NumberOfPages);
                        writer.Write(book.Year);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("File not found");
            }
            catch (IOException)
            {
                throw new InvalidOperationException("Error when saving to file");
            }
        }
    }
}
