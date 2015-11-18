using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Entities;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            FileRepository frep = new FileRepository();
            //XMLRepository xrep = new XMLRepository();
            //LINQ2XMLRepository linqrep = new LINQ2XMLRepository();

            BookList list = new BookList(frep);
            Book b1 = new Book() 
            { 
                Author = "Tolstoy",
                Title = "War and peace",
                Year = 1869,
                NumberOfPages = 1274,
                Publiser = "Moskva"
            };
            Book b2 = new Book() 
            { 
                Author = "Tolstoy",
                Title = "Anna Karenina",
                Year = 1877,
                NumberOfPages = 723,
                Publiser = "Russkii vestnik"
            };
            list.AddBook(b1);
            list.AddBook(b2);

            Book b = list.FindBookByTag(x => x.Publiser == "Moskva");
            Console.WriteLine(b.ToString());

            Console.ReadLine();
        }
    }
}
