using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Xml;

namespace Logic
{
    public class XMLRepository: IRepository
    {
        private string filePath;

        public XMLRepository()
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.xml");
        }

        public XMLRepository(string path)
        {
            filePath = path;
        }

        public List<Book> LoadToList()
        {
            List<Book> books = new List<Book>();

            try
            {
                using (var reader = XmlReader.Create(filePath))
                {
                    while (reader.Read())
                    {

                        Book book = new Book();
                        //reader.ReadToFollowing("book");
                        if (!reader.ReadToFollowing("title"))
                            break;
                        else
                        {
                            book.Title = reader.ReadElementContentAsString();
                            Console.WriteLine(book.Title);
                            reader.ReadToFollowing("author");
                            book.Author = reader.ReadElementContentAsString();
                            Console.WriteLine(book.Author);
                            reader.ReadToFollowing("publisher");
                            book.Publiser = reader.ReadElementContentAsString();
                            Console.WriteLine(book.Publiser);
                            reader.ReadToFollowing("year");
                            book.Year = reader.ReadElementContentAsInt();
                            Console.WriteLine(book.Year);
                            reader.ReadToFollowing("numberOfPages");
                            book.NumberOfPages = reader.ReadElementContentAsInt();
                            Console.WriteLine(book.NumberOfPages);
                            books.Add(book);
                        }
                    }
                }
                return books;
            }
            catch (XmlException)
            {
                throw new InvalidDataException("Invalid xml document");
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("Error while saving");
            }
        }

        public void LoadToFile(List<Book> books)
        {
            try
            {
                using (var writer = XmlWriter.Create(filePath))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("books");
                    foreach (Book book in books)
                    {
                        writer.WriteStartElement("book");

                        writer.WriteElementString("title", book.Title);
                        writer.WriteElementString("author", book.Author);
                        writer.WriteElementString("publisher", book.Publiser);

                        writer.WriteStartElement("year");
                        writer.WriteValue(book.Year);
                        writer.WriteEndElement();

                        writer.WriteStartElement("numberOfPages");
                        writer.WriteValue(book.NumberOfPages);
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (XmlException)
            {
                throw new InvalidDataException("Invalid xml document");
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("Error while saving");
            }
        }
    }
}
