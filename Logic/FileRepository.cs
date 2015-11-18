using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Entities;

namespace Logic
{
    public class FileRepository: IRepository
    {
        private string filePath;

        public FileRepository()
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.bin"); 
        }

        public FileRepository(string path)
        {
            filePath = path;
        }

        public List<Book> LoadToList()
        {
            List<Book> books = new List<Book>();

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

                return books;
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

        public void LoadToFile(List<Book> books)
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
