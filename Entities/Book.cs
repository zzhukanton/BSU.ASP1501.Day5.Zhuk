using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Book: IEquatable<Book>, IComparable<Book>
    {
        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                if (value > DateTime.Now.Year || value < 0)
                    throw new ArgumentException("Wrong year value");
                year = value;
            }
        }

        private int numberOfPages;
        public int NumberOfPages
        {
            get { return numberOfPages; }
            set
            {
                if (value < 3)
                    throw new ArgumentException("Too few pages");
                numberOfPages = value;
            }
        }

        public string Title { get; set; }
        public string Author { get; set; }

        private string publisher;
        public string Publiser
        {
            get { return publisher; }
            set
            {
                if (value == null || value == string.Empty)
                    publisher = "none";
                else
                    publisher = value;
            }
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return (Title == other.Title && Author == other.Author);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            Book b = obj as Book;
            if (b == null)
                return false;

            return this.Equals(b);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode() ^ Author.GetHashCode();
        }

        public int CompareTo(Book other)
        {
            if (other == null)
                return 1;
            if (this.Equals(other))
                return 0;

            return Title.CompareTo(other.Title);
        }

        public override string ToString()
        {
            return string.Format("Title: {0}, Author: {1}, Publishing year: {2}, Publisher: {3}, Number of pages: {4}",
                Title, Author, Year, Publiser, NumberOfPages);
        }
    }
}
