using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public static class BookListService
    {
        public static readonly string filePath = @"D:\BookStorage.bin";

        public static void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book is null");

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(book.Author);
                    writer.Write(book.Title);
                    writer.Write(book.Publisher);

                }
            }
        }

        public static void RemoveBook()
        {

        }

        public static Book FindByTag()
        {

        }

        public static void SortBooksByTag()
        {

        }
    }
}
