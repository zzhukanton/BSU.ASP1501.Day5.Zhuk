using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Xml.Linq;

namespace Logic
{
    public class LINQ2XMLRepository: IRepository
    {
        private string filePath;

        public LINQ2XMLRepository()
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.xml");
        }

        public LINQ2XMLRepository(string path)
        {
            filePath = path;
        }

        public List<Book> LoadToList()
        {
            List<Book> books = new List<Book>();

            try
            {
                XDocument document = XDocument.Load(filePath);
                var boooks = document.Elements("books").Elements("book");

                foreach (XElement e in boooks)
                {
                    Book b = new Book();
                    b.Title = e.Element("title").Value;
                    b.Author = e.Element("author").Value;
                    b.Publiser = e.Element("publisher").Value;
                    b.Year = int.Parse(e.Element("year").Value);
                    b.NumberOfPages = int.Parse(e.Element("numberOfPages").Value);

                    books.Add(b);
                }
                return books;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("Error while saving");
            }
        }

        public void LoadToFile(List<Book> books)
        {
            List<XElement> nodes = new List<XElement>();

            foreach (Book book in books)
            {
                nodes.Add(new XElement("book",
                    new XElement("title", book.Title),
                    new XElement("author", book.Author),
                    new XElement("publisher", book.Publiser),
                    new XElement("year", book.Year),
                    new XElement("numberOfPages", book.NumberOfPages)));
            }

            XDocument file = new XDocument(new XElement("books", nodes));
            file.Save(filePath);
        }
    }
}
